using AutoMapper;
using Ecommerce.API.Service.Interface;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;

namespace Ecommerce.API.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly ILogger<CartService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IProductRepo _productRepo;
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _configuration;

        public CartService(ICartRepo cartRepo, ILogger<CartService> logger, IMapper mapper, IUserRepo userRepo, IProductRepo productRepo, IEmailServices emailServices, IConfiguration configuration)
        {
            _cartRepo = cartRepo;
            _logger = logger;
            _mapper = mapper;
            _userRepo = userRepo;
            _productRepo = productRepo;
            _emailServices = emailServices;
            _configuration = configuration;
        }
        public async Task<ResponseDto<CartItem>> AddCartItemAsync(AddCartItemDto CartItem)
        {
            var response = new ResponseDto<CartItem>();
            try
            {
                var checkCart = await _cartRepo.RetrieveUserCartAsync(CartItem.UserId);
                if (checkCart == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Cart" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkProduct = await _productRepo.GetProductbyIdAsync(CartItem.ProductId);
                if (checkProduct == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Product" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkProduct.AvailableProductNumber == 0)
                {
                    var AdminMail = new List<string>() { _configuration["BusinessEmail"] };
                    var content = "<h5>The prodict details below is out of stock</h5> <ul>"
                         + $"<li>Product Id: <strong>{checkProduct.Id}</strong> </li>" +
                         $"<li>Product Name: <strong>{checkProduct.Name}</strong> </li>" +
                         $"<li>Product Description: <strong>{checkProduct.Description}</strong> </li> </ul>";
                    var subject = "<h1>Out of Stock Notification</h1>";
                    var message = new Message(AdminMail, subject, content);
                    _emailServices.SendEmail(message);
                    response.DisplayMessage = "Product out of stock";
                    response.StatusCode = StatusCodes.Status204NoContent;
                    return response;
                }else if (checkProduct.AvailableProductNumber < CartItem.Quantity)
                {
                    response.ErrorMessages = new List<string>() { "Product quantity requested is not available" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var newItem = new CartItem();
                newItem.CartId = checkCart.Id;
                newItem.ProductId = checkProduct.Id;
                newItem.Quantity = CartItem.Quantity;
                var saveItem = await _cartRepo.AddProductToCart(newItem);
                if (saveItem == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in adding product to cart" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = saveItem;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in adding product to cart" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<DisplayCart>> CreateCartAsync(string userid)
        {
            var response = new ResponseDto<DisplayCart>();
            try
            {

                var newCart = new Cart() { UserId = userid };
                var checkUserExist = await _userRepo.FindUserByIdAsync(userid);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var addcart = await _cartRepo.CreateCartAsync(newCart);
                if (addcart == null)
                {
                    response.ErrorMessages = new List<string>() { "Cart not created successfully" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var mappResult = _mapper.Map<DisplayCart>(addcart);
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = mappResult;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating cart" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<DisplayCart>> GetCartbyUserIdAsync(string userid)
        {
            var response = new ResponseDto<DisplayCart>();
            try
            {
                var checkCart = await _cartRepo.RetrieveUserCartAsync(userid);
                if (checkCart == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Cart" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var totalPrice = checkCart.CartItems.Sum(item => item.Quantity * item.Product.Price);
                var totalWeight = checkCart.CartItems.Sum(item => item.Quantity * item.Product.Specification.Weight);
                var mappResult = _mapper.Map<DisplayCart>(checkCart);
                mappResult.TotalPrice = totalPrice;
                mappResult.TotalWeight = totalWeight;
                response.Result = mappResult;
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in retriving cart" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<DisplayCart>> UpdateUserCartItemAsync(string userid, string productid, int quantity)
        {
            var response = new ResponseDto<DisplayCart>();
            try
            {
                var checkCart = await _cartRepo.RetrieveUserCartAsync(userid);
                if (checkCart == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Cart" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var getCartItem = await _cartRepo.RetrieveUserCartItemAsync(productid, checkCart.Id);
                if (getCartItem == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid cart item" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                getCartItem.Quantity = quantity;
                var updateCartItem = await _cartRepo.UpdateCartItemAsync(getCartItem);
                if (updateCartItem == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in updating cart quantity" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var totalPrice = checkCart.CartItems.Sum(item => item.Quantity * item.Product.Price);
                var mappResult = _mapper.Map<DisplayCart>(checkCart);
                mappResult.TotalPrice = totalPrice;
                response.Result = mappResult;
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating cart" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> RemoveUserCartItemAsync(string userid, string productid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkCart = await _cartRepo.RetrieveUserCartAsync(userid);
                if (checkCart == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Cart" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var getCartItem = await _cartRepo.RetrieveUserCartItemAsync(productid, checkCart.Id);
                if (getCartItem == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid cart item" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var deleteCartitem = await _cartRepo.RemoveCartItemAsync(getCartItem);
                if (deleteCartitem == false)
                {
                    response.ErrorMessages = new List<string>() { "Unable to remove cart item" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.DisplayMessage = "Success";
                response.Result = "Cart item deleted successfully";
                response.StatusCode = 200;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in removing cart item" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}

using AutoMapper;
using Ecommerce.API.Service.Interface;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;

namespace Ecommerce.API.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepo _cartRepo;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        
        private readonly IOrderRepo _orderRepo;

        public OrderService(ICartRepo cartRepo, ILogger<OrderService> logger, IMapper mapper, IOrderRepo orderRepo)
        {
            _cartRepo = cartRepo;
            _logger = logger;
            _mapper = mapper;
            _orderRepo = orderRepo;
        }
        public async Task<ResponseDto<string>> CreateOrderAsync(AddOrderDto addOrder)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkCart = await _cartRepo.RetrieveUserCartAsync(addOrder.UserId);
                if (checkCart == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Cart" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }

                var totalPrice = checkCart.CartItems.Sum(item => item.Quantity * item.Product.Price);
                var newOrder = _mapper.Map<Order>(addOrder);
                newOrder.CartId = checkCart.Id;
                newOrder.UserId = checkCart.UserId;
                newOrder.DeliveryFee = 100; //I will connect it later
                newOrder.ItemTotalPrice = (long)totalPrice;
                newOrder.OverallPrice = newOrder.ItemTotalPrice + newOrder.DeliveryFee;
                var addNewOrder = await _orderRepo.CreateOrderAsync(newOrder);
                if (addNewOrder == null)
                {
                    response.ErrorMessages = new List<string>() { "Order not created successfully" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = "Order created sucessfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating order" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<DisplayOrderDto>> GetActiveUserOrderbyUserIdAsync(string userid)
        {
            var response = new ResponseDto<DisplayOrderDto>();
            try
            {
                var retrieveOrder = await _orderRepo.FindActiveOrderbyUserId(userid);
                if (retrieveOrder == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no active order" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var mapresult = _mapper.Map<DisplayOrderDto>(retrieveOrder);
                response.Result = mapresult;
                response.StatusCode = 200;
                response.DisplayMessage = "Successful";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in retrieving order" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> RemoveActiveUserOrderbyUserIdAsync(string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var retrieveOrder = await _orderRepo.FindActiveOrderbyUserId(userid);
                if (retrieveOrder == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no active order" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var deleteOrder = await _orderRepo.DeleteOrder(retrieveOrder);
                if (deleteOrder == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in removing active order" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Order successfully deleted";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in removing order" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}

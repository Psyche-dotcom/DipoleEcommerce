using AutoMapper;
using Ecommerce.API.Service.Interface;
using Ecommerce.Data.Repository.Implementation;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;
using System.Net.Http.Headers;

namespace Ecommerce.API.Service.Implementation
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepo _deliveryRepo;
        private readonly ILogger<DeliveryService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _configuration;
        private readonly ICartRepo _cartRepo;

        public DeliveryService(IMapper mapper, IDeliveryRepo deliveryRepo, ILogger<DeliveryService> logger, IUserRepo userRepo, IConfiguration configuration, ICartRepo cartRepo)
        {
            _mapper = mapper;
            _deliveryRepo = deliveryRepo;
            _logger = logger;
            _userRepo = userRepo;
            _configuration = configuration;
            _cartRepo = cartRepo;
        }

        public async Task<ResponseDto<string>> CreateDeliveryAddressAsync(AddDeliveryDto deliveryDto, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUserExist = await _userRepo.FindUserByIdAsync(userid);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var delivery = _mapper.Map<Delivery>(deliveryDto);
                delivery.UserId = checkUserExist.Id;
                delivery.ReceiverName = checkUserExist.FirstName + " " + checkUserExist.LastName;
                var addDelivery = await _deliveryRepo.AddDelivery(delivery);
                if (addDelivery == false)
                {
                    response.ErrorMessages = new List<string>() { "Pickup location not created successfully" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = "successfully create user location";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating user pickup location" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<DisplayDeliveryAddressDto>> GetDeliveryAddressAsync(string userid)
        {
            var response = new ResponseDto<DisplayDeliveryAddressDto>();
            try
            {
                var checkUserExist = await _userRepo.FindUserByIdAsync(userid);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkDeliveryAddress = await _deliveryRepo.GetUserDelivery(userid);
                if (checkDeliveryAddress == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Delivery address" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var mappResult = _mapper.Map<DisplayDeliveryAddressDto>(checkDeliveryAddress);
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
        public async Task<ResponseDto<DisplayDeliveryCharges>> GetShippingfeeQoute(string userid)
        {
            var response = new ResponseDto<DisplayDeliveryCharges>();
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
                
                var totalWeight = checkCart.CartItems.Sum(item => item.Quantity * item.Product.Specification.Weight);
                var checkDeliveryAddress = await _deliveryRepo.GetUserDelivery(userid);
                if (checkDeliveryAddress == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Delivery address" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var payload = _mapper.Map<ShippingRequestPayload>(checkDeliveryAddress);
                payload.weight = (float)totalWeight;
                using (var client = new HttpClient())
                {
                    var BaseAddress = _configuration["ApiUrl:GetShipQuoteBaseUrlStage"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_configuration["ApiUrl:AccessToken"]);
                    var SendRequest = await client.PostAsJsonAsync(BaseAddress, payload);
                    var result = await SendRequest.Content.ReadAsStringAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ResponseDto<string>> UpdateDeliveryAddressAsync(UpdateDeliveryDto deliveryDto, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUserExist = await _userRepo.FindUserByIdAsync(userid);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkDeliveryAddress = await _deliveryRepo.GetUserDelivery(userid);
                if (checkDeliveryAddress == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Delivery address" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var mapResult = _mapper.Map(deliveryDto, checkDeliveryAddress);
                var updateCartItem = await _deliveryRepo.UpdateDelivery(mapResult);
                if (updateCartItem == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in updating cart quantity" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }

                response.Result = "Pickup Location Successfully updated";
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
    }
}

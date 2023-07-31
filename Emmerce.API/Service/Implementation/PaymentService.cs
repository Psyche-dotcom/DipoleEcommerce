using Ecommerce.API.Service.Interface;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.DTO;
using Flutterwave.Net;
using System.Net.Http.Headers;

namespace Ecommerce.API.Service.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        private readonly ICartRepo _cartRepo;
        private readonly IUserRepo _userRepo;

        public PaymentService(IOrderRepo orderRepo, IConfiguration configuration, ILogger<PaymentService> logger, ICartRepo cartRepo, IUserRepo userRepo)
        {
            _orderRepo = orderRepo;
            _configuration = configuration;
            _logger = logger;
            _cartRepo = cartRepo;
            _userRepo = userRepo;
        }
        public async Task<ResponseDto<TransactionData>> InitializePaymentAsync(string userid)
        {
            var response = new ResponseDto<TransactionData>();
            try
            {

                var retrieveUser = await _userRepo.FindUserByIdAsync(userid);
                if (retrieveUser == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid User" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var RetrieveOrder = await _orderRepo.FindActiveOrderbyUserId(userid);
                if (RetrieveOrder == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Order" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var api = new FlutterwaveApi(_configuration["ApiUrl:FlutterwaveScretKey"]);
                var paymentdetails = new CustomerRequestPayment(RetrieveOrder.OverallPrice, new CustomerInfo() { email = retrieveUser.Email, name = retrieveUser.FirstName, phone = retrieveUser.PhoneNumber }, _configuration, RetrieveOrder.OrderTransactionReferenceId);
                InitiatePaymentResponse result = api.Payments.InitiatePayment(paymentdetails.tx_ref, paymentdetails.amount, paymentdetails.redirect_url, paymentdetails.customer.name, paymentdetails.customer.email, paymentdetails.customer.phone, "Order", "This is coming from dipole ecommerce", "incoming.com");
                if (result.Status == "success")
                {
                    string hostedLink = result.Data.Link;
                    var data = new TransactionData();
                    data.message = result.Message;
                    data.data = new DataTransaction() { link = hostedLink };
                    data.status = result.Status;
                    response.StatusCode = 200;
                    response.Result = data;
                    response.DisplayMessage = "Success";
                    return response;
                }

                response.ErrorMessages = new List<string>() { "Error in initializing Transaction checkout" };
                response.DisplayMessage = "Error";
                response.StatusCode = 400;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in initializing Transaction checkout" };
                response.DisplayMessage = "Error";
                response.StatusCode = 500;
                return response;
            }

        }
        public async Task<ResponseDto<string>> PaymentWebhookAsync(string status,string tx_ref, string transaction_id)
        {
            var response = new ResponseDto<string>();
            try
            {
                if(status != "success")
                {
                    response.ErrorMessages = new List<string>() { "Invalid Transaction" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var RetrieveOrder = await _orderRepo.FindOrderbyTransactionId(tx_ref);
                if (RetrieveOrder == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Transaction" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                using (var client = new HttpClient())
                {
                    var apiUrl = _configuration["ApiUrl:FlutterwaveBase"] + $"{transaction_id}/verify";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["ApiUrl:FlutterwaveScretKey"]);
                    var result = await client.GetFromJsonAsync<ValidationResponseData>(apiUrl);
                    if (result.data.tx_ref == RetrieveOrder.OrderTransactionReferenceId && result.data.status == "successful" && result.status == "success" && result.data.amount == RetrieveOrder.OverallPrice)
                    {


                        RetrieveOrder.IsOrderActive = false;
                        RetrieveOrder.TransactionStatus = Model.Enum.TransactionStatus.Done;
                        RetrieveOrder.OrderTransactionId = result.data.id;
                        var updateOrder = await _orderRepo.UpdateOrderAsync(RetrieveOrder);
                        if (updateOrder == false)
                        {
                            response.ErrorMessages = new List<string>() { "Error in completing transaction" };
                            response.DisplayMessage = "Error";
                            response.StatusCode = 400;
                            return response;
                        }
                        var clearcart = await _cartRepo.ClearUserAllCartItems(RetrieveOrder.CartId);
                        if (clearcart == false)
                        {
                            response.ErrorMessages = new List<string>() { "Error in clearing cart item" };
                            response.DisplayMessage = "Error";
                            response.StatusCode = 400;
                            return response;
                        }
                        response.StatusCode = 200;
                        response.Result = "Order completed successfully";
                        response.DisplayMessage = "Success";
                        return response;
                    }
                    else
                    {

                        RetrieveOrder.IsOrderActive = false;
                        RetrieveOrder.TransactionStatus = Model.Enum.TransactionStatus.Failed;
                        //RetrieveOrder.OrderTransactionId = result.data.id;
                        var updateOrder = await _orderRepo.UpdateOrderAsync(RetrieveOrder);
                        if (updateOrder == false)
                        {
                            response.ErrorMessages = new List<string>() { "Error in completing transaction" };
                            response.DisplayMessage = "Error";
                            response.StatusCode = 400;
                            return response;
                        }
                        response.ErrorMessages = new List<string>() { "Invalid Transaction" };
                        response.DisplayMessage = "Error";
                        response.StatusCode = 400;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in handling Transaction Order" };
                response.DisplayMessage = "Error";
                response.StatusCode = 400;
                return response;
            }
        }

    }
}

using Microsoft.Extensions.Configuration;

namespace Ecommerce.Model.DTO
{
    public class CustomerRequestPayment
    {
        private readonly IConfiguration _configuration;
        public string public_key { get; set; }
        public string tx_ref { get; set; }
        public long amount { get; set; }
        public string currency { get; set; }
        public string redirect_url { get; set; }
        public CustomerInfo customer { get; set; }
        public CustomerRequestPayment(long amount, CustomerInfo customer, IConfiguration configuration, string tx)
        {
            tx_ref = tx;
            this.amount = amount;
            this.customer = customer;
            _configuration = configuration;
            currency = "NGN";
            redirect_url = _configuration["ApiUrl:Redirect_Url"];
            public_key = _configuration["ApiUrl:FlutterwavePubKey"];
        }
    }
}

namespace Ecommerce.Model.DTO
{
    public class ShippingRequestPayload
    {
        public string origin_name { get; set; }
        public string origin_phone { get; set; }
        public string origin_street { get; set; }
        public string origin_city { get; set; }
        public string origin_state { get; set; }
        public string origin_country { get; set; }
        public string destination_name { get; set; }
        public string destination_phone { get; set; }
        public string destination_street { get; set; }
        public string destination_city { get; set; }
        public string destination_state { get; set; }
        public string destination_country { get; set;}
        public float weight { get; set; }
        public string currency { get; set; } = "NGN";

    }
}

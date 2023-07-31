using Ecommerce.Model.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model.Entities
{
    public class Order : BaseEntity
    {
        [ForeignKey("Cart")]
        public string CartId { get; set; }
        public Cart Cart { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long DeliveryFee { get; set; }
        public long ItemTotalPrice { get; set; }
        public string OrderTransactionReferenceId { get; set; } = Guid.NewGuid().ToString();
        public int OrderTransactionId { get; set; }
        public bool IsOrderActive { get; set; } = true;
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;
        public long OverallPrice { get; set; }
    }
}

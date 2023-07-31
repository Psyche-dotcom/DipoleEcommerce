using Ecommerce.Model.Entities;

namespace Ecommerce.Data.Repository.Interface
{
    public interface IOrderRepo
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> FindOrderbyTransactionId(string transaction_id);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrder(Order order);
        Task<Order> FindActiveOrderbyUserId(string user_id);
    }
}

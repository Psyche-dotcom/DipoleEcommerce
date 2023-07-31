using Ecommerce.Data.Context;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repository.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly DipoleEcommerceContext _context;

        public OrderRepo(DipoleEcommerceContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            var NewOrder = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return NewOrder.Entity;
        }
        public async Task<Order> FindOrderbyTransactionId(string transaction_id)
        {
            var findOrder = await _context.Orders.FirstOrDefaultAsync(O => O.OrderTransactionReferenceId == transaction_id);
            return findOrder;
        }
        public async Task<Order> FindActiveOrderbyUserId(string user_id)
        {
            var findOrder = await _context.Orders.FirstOrDefaultAsync(O => O.UserId == user_id && O.IsOrderActive == true && O.TransactionStatus == Model.Enum.TransactionStatus.Pending);
            return findOrder;
        }

        public async Task<bool> UpdateOrderAsync (Order order)
        {
            _context.Orders.Update(order);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}

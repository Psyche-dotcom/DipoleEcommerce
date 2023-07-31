using Ecommerce.Data.Context;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repository.Implementation
{
    public class DeliveryRepo : IDeliveryRepo
    {
        private readonly DipoleEcommerceContext _context;

        public DeliveryRepo(DipoleEcommerceContext context)
        {
            _context = context;
        }

        public async Task<bool> AddDelivery(Delivery delivery)
        {
            await _context.Deliverys.AddAsync(delivery);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Delivery> GetUserDelivery(string userid)
        {
            var getUserDelivery = await _context.Deliverys.FirstOrDefaultAsync(d => d.UserId == userid);
            return getUserDelivery;
        }

        public async Task<bool> UpdateDelivery(Delivery delivery)
        {
            _context.Deliverys.Update(delivery);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}

using Ecommerce.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Repository.Interface
{
    public interface IDeliveryRepo
    {
        Task<bool> AddDelivery(Delivery delivery);
        Task<bool> UpdateDelivery(Delivery delivery);
        Task<Delivery> GetUserDelivery(string userid);
    }
}

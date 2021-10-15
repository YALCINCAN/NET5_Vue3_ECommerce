
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        Task<IEnumerable<UserOrderDTO>> GetOrdersByUserIdAsync(string userid);
        Task<OrderDetail> GetOrderDetailByOrderNumberAsync(string ordernumber);
        Task<IEnumerable<UserOrderDTO>> GetAdminOrdersAsync();
        Task<AdminOrderDetailDTO> GetAdminOrderDetailAsync(string ordernumber);
    }
}

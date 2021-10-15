
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task<IResponse> AddAsync(OrderDTO model);
        Task<IResponse> GetUserOrdersAsync();
        Task<IResponse> GetOrderDetailByOrderNumber(string ordernumber);
        Task<IResponse> GetAdminOrderDetailAsync(string ordernumber);
        Task<IResponse> GetAdminOrdersAsync();
        Task<IResponse> UpdateAsync(AdminOrderDetailDTO model);
       
    }
}


using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;

namespace Business.Concrete
{
    public class OrderStatusManager : IOrderStatusService
    {
        private IOrderStatusRepository _orderStatusRepository;
        public OrderStatusManager(IOrderStatusRepository orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<IResponse> GetAllAsync()
        {
            var orderstatuses = await _orderStatusRepository.GetAllAsync();
            return new DataResponse<IEnumerable<OrderStatus>>(orderstatuses, 200);
        }
    }

}
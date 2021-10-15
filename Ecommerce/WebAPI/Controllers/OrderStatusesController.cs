using Business.Abstract;
using Core.Utilities.Responses.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase
    {
        private IOrderStatusService _orderStatusService;
        public OrderStatusesController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<IResponse> GetOrderStatuses()
        {
            return await _orderStatusService.GetAllAsync();
        }
    }
}

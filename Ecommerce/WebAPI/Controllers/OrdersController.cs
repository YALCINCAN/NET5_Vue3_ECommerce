using Business.Abstract;
using Core.ActionFilters;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
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
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("getadminorders")]
        public async Task<IResponse> GetAdminOrdersAsync()
        {
            return await _orderService.GetAdminOrdersAsync();
        }

        [Authorize]
        [HttpPost]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> AddOrder(OrderDTO model)
        {
            return await _orderService.AddAsync(model);
        }

        [Authorize]
        [HttpGet("getadminorderdetail/{ordernumber}")]
        public async Task<IResponse> GetAdminOrderDetailAsync(string ordernumber)
        {
            return await _orderService.GetAdminOrderDetailAsync(ordernumber);
        }
        [Authorize]
        [HttpGet]
        public async Task<IResponse> GetUserOrdersAsync()
        {
            return await _orderService.GetUserOrdersAsync();
        }

        [Authorize]
        [HttpGet("{ordernumber}")]
        public async Task<IResponse> GetOrderDetailByOrderNumber(string ordernumber)
        {
            return await _orderService.GetOrderDetailByOrderNumber(ordernumber);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateOrder(AdminOrderDetailDTO model)
        {
            return await _orderService.UpdateAsync(model);
        }
    }
}

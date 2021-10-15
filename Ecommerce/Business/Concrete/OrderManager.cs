
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Core.Exceptions;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IBasketService _basketService;
        private IHttpContextAccessor _httpContextAccessor;
        public OrderManager(IOrderRepository orderRepository,IBasketService basketService,IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }

        [ValidationAspect(typeof(AddOrderValidator))]
        public async Task<IResponse> AddAsync(OrderDTO model)
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            string cardname = "John Doe";
            var cardnumber = "5528790000000008";
            var expirationMonth = "12";
            var expirationYear = "2030";
            var cvc = "123";
            if (model.CardName== cardname && model.CardNumber==cardnumber&& model.ExpirationMonth==expirationMonth && model.ExpirationYear==expirationYear  && model.Cvc == cvc)
            {
                var basket = await _basketService.GetBasketWithTotalPriceAsync();
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderStatusId = 1,
                    UserId = userid,
                    OrderNumber = new Random().Next(111111, 999999).ToString(),
                    AddressId = model.AddressId,
                    TotalPrice = Convert.ToDecimal(basket.Data.TotalPrice),
                    OrderItems = basket.Data.BasketItems.Select(basketitem => new OrderItem()
                    {
                        ProductId = basketitem.ProductId,
                        Quantity = basketitem.Quantity,
                    }).ToList()
                };
                await _orderRepository.AddAsync(order);
                await _basketService.ClearBasket();
                return new SuccessResponse(200, Messages.YourOrderIsBeingProcessed);
            }
            throw new ApiException(400, Messages.PaymentFailed);
        }

        [ValidationAspect(typeof(UpdateOrderValidator))]
        public async Task<IResponse> UpdateAsync(AdminOrderDetailDTO model)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == model.Id);
            if (order != null)
            {
                order.Id = model.Id;
                order.OrderStatusId = model.OrderStatusId;
                await _orderRepository.UpdateAsync(order);
                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
            throw new ApiException(404, Messages.NotFound);
        }
           
        public async Task<IResponse> GetUserOrdersAsync()
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userorders = await _orderRepository.GetOrdersByUserIdAsync(userid);
            return new DataResponse<IEnumerable<UserOrderDTO>>(userorders, 200);
        }

        public async Task<IResponse> GetOrderDetailByOrderNumber(string ordernumber)
        {
            var orderdetail = await _orderRepository.GetOrderDetailByOrderNumberAsync(ordernumber);
            if (orderdetail != null)
            {
                return new DataResponse<OrderDetail>(orderdetail, 200);
            }
            throw new ApiException(404, Messages.NotFound);
        }

        public async Task<IResponse> GetAdminOrdersAsync()
        {
            var adminorders = await _orderRepository.GetAdminOrdersAsync();
            return new DataResponse<IEnumerable<UserOrderDTO>>(adminorders, 200);
        }

        public async Task<IResponse> GetAdminOrderDetailAsync(string ordernumber)
        {
            var adminorder = await _orderRepository.GetAdminOrderDetailAsync(ordernumber);
            if (adminorder != null)
            {
                return new DataResponse<AdminOrderDetailDTO>(adminorder, 200);
            }
            throw new ApiException(404, Messages.NotFound);
        }
    }
}
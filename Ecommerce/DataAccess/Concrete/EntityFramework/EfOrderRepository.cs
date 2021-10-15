
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderRepository : EfEntityRepositoryBase<Order, ECommerceContext>, IOrderRepository
    {
        public EfOrderRepository(ECommerceContext context) : base(context)
        {

        }

        public async Task<AdminOrderDetailDTO> GetAdminOrderDetailAsync(string ordernumber)
        {
            return await _context.Orders.Where(x => x.OrderNumber == ordernumber).Select(order => new AdminOrderDetailDTO()
            {
                Id = order.Id,
                OrderStatusId = order.OrderStatusId
            }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserOrderDTO>> GetAdminOrdersAsync()
        {
            return await _context.Orders.Select(order => new UserOrderDTO()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderNumber = order.OrderNumber,
                OrderStatus = order.OrderStatus.Status,
                TotalPrice = order.TotalPrice.ToString("N"),
            }).ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailByOrderNumberAsync(string ordernumber)
        {
            return await _context.Orders.Select(order => new OrderDetail
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                TotalPrice=order.TotalPrice.ToString("N"),
                OrderStatus=order.OrderStatus.Status,
                User=new UserDetail
                {
                    Id=order.User.Id,
                    FirstName=order.User.FirstName,
                    LastName=order.User.LastName
                },
                Address = new AddressDetail
                {
                    Description = order.Address.Description,
                    City = order.Address.City,
                    Country = order.Address.Country,
                    Phone=order.Address.Phone
                },
                OrderItems= order.OrderItems.Select(orderitem=> new OrderItemDetail
                {
                    Id = orderitem.Id,
                    ProductId = orderitem.ProductId,
                    Quantity = orderitem.Quantity,
                    Price = orderitem.Product.Price.ToString("N"),
                    MainImage = orderitem.Product.MainImage,
                    Name = orderitem.Product.Name,
                })
            }).FirstOrDefaultAsync(x => x.OrderNumber == ordernumber);
        }

        public async Task<IEnumerable<UserOrderDTO>> GetOrdersByUserIdAsync(string userid)
        {
            return await _context.Orders.Where(x=>x.UserId==userid).Select(order => new UserOrderDTO()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderNumber = order.OrderNumber,
                OrderStatus=order.OrderStatus.Status,
                TotalPrice = order.TotalPrice.ToString("N"),
            }).ToListAsync();
        }
    }
}

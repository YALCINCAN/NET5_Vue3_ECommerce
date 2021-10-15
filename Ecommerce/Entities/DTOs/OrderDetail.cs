using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string TotalPrice { get; set; }
        public UserDetail User { get; set; }
        public AddressDetail Address { get; set; }
        public string OrderStatus { get; set; }
        public IEnumerable<OrderItemDetail> OrderItems { get; set; }
    }

    public class OrderItemDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
    }
}

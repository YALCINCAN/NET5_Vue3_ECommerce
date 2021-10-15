using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class BasketDetail
    {
        public int Id { get; set; }
        public List<BasketItemDetail> BasketItems { get; set; }
        public string TotalPrice { get; set; }
    }

    public class BasketItemDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string MainImage { get; set; }
        public int Quantity { get; set; }
    }
}

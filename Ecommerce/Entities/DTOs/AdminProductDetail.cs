using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AdminProductDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string MainImage { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<ProductImageDTO> ProductImages { get; set; }
        public IEnumerable<AdminProductOptionValueDetailDTO> ProductOptionValues { get; set; }
    }
}

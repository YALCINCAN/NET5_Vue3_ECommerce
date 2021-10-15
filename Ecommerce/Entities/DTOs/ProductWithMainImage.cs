using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductWithMainImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string Slug { get; set; }
        public string Price { get; set; }
        public string MainImage { get; set; }
        public decimal? RatingAverage { get; set; }
    }
}

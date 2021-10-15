using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Filters
{
    public class ProductFilter : OrderableFilterBase
    {
        public Range<decimal> Price { get; set; }
        public CategoryFilter Category { get; set; }
        public BrandFilter Brand { get; set; }
        public ProductOptionValueFilter ProductOptionValues { get; set; }
        [IgnoreFilter]
        public string Search { get; set; }

        [IgnoreFilter]
        public int Page { get; set; }
        [IgnoreFilter]
        public int PageSize { get; set; }
    }
}

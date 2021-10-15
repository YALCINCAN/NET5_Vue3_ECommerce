using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Filters
{
    public class BrandFilter: FilterBase
    {
        public string[] Name { get; set; }
    }
}

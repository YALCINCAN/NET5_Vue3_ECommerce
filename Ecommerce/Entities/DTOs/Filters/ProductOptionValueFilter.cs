
using AutoFilterer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Filters
{
    public class ProductOptionValueFilter:FilterBase
    {
        public OptionValueFilter OptionValue { get; set; }
    }
}

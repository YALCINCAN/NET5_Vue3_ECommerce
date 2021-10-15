using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductOptionValueDTO
    {
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public int OptionValueId { get; set; }
    }
}

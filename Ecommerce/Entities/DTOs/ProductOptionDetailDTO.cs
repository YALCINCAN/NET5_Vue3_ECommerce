using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductOptionDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OptionValueDetailDTO OptionValue { get; set; }
    }
}

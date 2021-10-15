using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AdminProductOptionValueDetailDTO : IDTO
    {
        public int OptionId { get; set; }
        public int OptionValueId { get; set; }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AddUpdateBasketDTO : IDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

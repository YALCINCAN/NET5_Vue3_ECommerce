using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductOptionValue:IEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
        public int OptionValueId { get; set; }
        public OptionValue OptionValue { get; set; }
    }
}

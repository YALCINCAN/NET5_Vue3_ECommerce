using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class CategoryOption:IEntity
    {
        public int OptionId { get; set; }
        public Option Option { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

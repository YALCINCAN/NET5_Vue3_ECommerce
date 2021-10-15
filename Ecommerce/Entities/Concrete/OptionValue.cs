using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class OptionValue:IEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}

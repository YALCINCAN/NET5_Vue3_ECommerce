using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Option:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OptionValue> OptionValues { get; set; }
        public ICollection<CategoryOption> CategoryOptions { get; set; }
    }
}

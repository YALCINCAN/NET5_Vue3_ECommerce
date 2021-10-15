using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CategoryDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] OptionIds { get; set; }
    }
}

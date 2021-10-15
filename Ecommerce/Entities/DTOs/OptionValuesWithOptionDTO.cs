using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OptionValuesWithOptionDTO : IDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int OptionId { get; set; }
        public OptionDTO Option { get; set; }
    }
}

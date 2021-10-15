using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OptionsWithValuesDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<OptionValueDTO> OptionValues { get; set; }
    }
}

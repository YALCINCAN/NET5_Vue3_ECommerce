using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class SliderDTO : IDTO
    {
        public int Id { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
    }
}

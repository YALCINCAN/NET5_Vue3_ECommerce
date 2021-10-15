using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ConfirmEmailDTO : IDTO
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}

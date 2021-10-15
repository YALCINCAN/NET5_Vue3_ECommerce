using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RoleDTO:IDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

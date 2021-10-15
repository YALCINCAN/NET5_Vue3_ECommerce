using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserWithRolesDTO
    {
        public UserDTO User { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}

using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        private const string AdminRoleId = "2301D884-221A-4E7D-B509-0113DCC043E1";
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            var role = new Role
            {
                Id = AdminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            builder.HasData(role);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        private const string AdminUserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7";
        private const string AdminRoleId = "2301D884-221A-4E7D-B509-0113DCC043E1";
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            IdentityUserRole<string> iur = new IdentityUserRole<string>
            {
                UserId = AdminUserId,
                RoleId = AdminRoleId
            };
            builder.HasData(iur);
        }
    }

}

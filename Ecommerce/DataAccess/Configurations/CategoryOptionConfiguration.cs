using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class CategoryOptionConfiguration : IEntityTypeConfiguration<CategoryOption>
    {
        public void Configure(EntityTypeBuilder<CategoryOption> builder)
        {
            builder.Property(x => x.OptionId).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
        }
    }
}

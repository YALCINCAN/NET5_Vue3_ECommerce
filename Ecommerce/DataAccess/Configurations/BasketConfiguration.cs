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
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            var basket = new Basket()
            {
                Id = 1,
                UserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
            };
            builder.HasData(basket);
        }
    }
}

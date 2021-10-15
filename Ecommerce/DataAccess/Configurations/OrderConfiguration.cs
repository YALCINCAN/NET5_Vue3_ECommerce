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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.AddressId).IsRequired();
            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.OrderStatusId).IsRequired();
            builder.Property(x => x.OrderNumber).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}

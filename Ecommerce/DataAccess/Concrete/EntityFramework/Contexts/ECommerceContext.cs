using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class ECommerceContext : IdentityDbContext<User, Role, string>
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOptionValue>().HasKey(pov => new { pov.ProductId, pov.OptionId, pov.OptionValueId });
            modelBuilder.Entity<CategoryOption>().HasKey(co => new { co.CategoryId, co.OptionId });
            modelBuilder.Entity<ProductOptionValue>().HasOne(p => p.OptionValue).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>().HasOne(o => o.User).WithMany(u => u.Orders).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<ProductOptionValue> ProductOptionValues { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CategoryOption> CategoryOptions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Slider> Sliders { get; set; }
    }
}

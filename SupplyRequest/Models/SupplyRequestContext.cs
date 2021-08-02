using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace SupplyRequestAPI.Models {
	public class SupplyRequestContext : DbContext {
		public SupplyRequestContext (DbContextOptions<SupplyRequestContext> options)
			: base(options) {
			Database.EnsureCreated();
		}

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }

		public DbSet<OrderVendor> OrderVendors { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<ProductType> ProductType { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<Vendor> Vendors { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Product>()
				.HasOne(c => c.ProductType);

			modelBuilder.Entity<OrderItem>()
				.HasOne(c => c.Product);

			modelBuilder.Entity<Order>()
				.HasMany(oi => oi.OrderItems);
			modelBuilder.Entity<Order>()
				.HasOne(c => c.User);
			modelBuilder.Entity<Order>()
				.HasMany<Vendor>(o => o.Vendors)
				.WithMany(v => v.Orders);
		}
	}
}

using SupplyRequestAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories {
	public class ProductRepository : IProductRepository {

		private readonly SupplyRequestContext _context;
		public ProductRepository(SupplyRequestContext context) {
			_context = context;
		}
		public async Task<Product> Create(Product product) {
			ProductType existingProductType = _context.ProductType.Find(product.TypeId);

			Product newProduct = new() {
				ID = product.ID,
				Name = product.Name,
				Description = product.Description,
				SKU = product.SKU,
				Active = product.Active || true,
				TypeId = product.TypeId,
				ProductType = existingProductType ?? product.ProductType
			};

			_context.Product.Add(newProduct);
	
			await _context.SaveChangesAsync();

			return newProduct;
		}

		public async Task<IEnumerable<Product>> Create(IEnumerable<Product> productList) {
			_context.Product.AddRange(productList);
			await _context.SaveChangesAsync();

			return productList;
		}

		public async Task Delete(int ID) {
			var product = await _context.Product.FindAsync(ID);
			_context.Product.Remove(product);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Product>> Get() {
			return await _context.Product
				.Include(t => t.ProductType)
				.Where(p => p.Active.Equals(true))				
				.ToListAsync();
		}

		public async Task<Product> Get(int ID) {
			return await _context.Product.FindAsync(ID);
		}
	}
}

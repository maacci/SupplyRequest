using SupplyRequestAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SupplyRequestAPI.Repositories {
	public class ProductTypeRepository : IProductTypeRepository {
		private readonly SupplyRequestContext _context;
		public ProductTypeRepository(SupplyRequestContext context) {
			_context = context;
		}
		public async Task<ProductType> Create(ProductType productType) {
			try
			{
				_context.ProductType.Add(productType);
				await _context.SaveChangesAsync();
			} catch (Exception)
			{

			}

			return productType;
		}

		public async Task<IEnumerable<ProductType>> Create(IEnumerable<ProductType> productTypes) {
			_context.ProductType.AddRange(productTypes);
			await _context.SaveChangesAsync();

			return productTypes;
		}

		public async Task Delete(int ID) {
			var productType = await _context.ProductType.FindAsync(ID);
			_context.ProductType.Remove(productType);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ProductType>> Get() {
			return await _context.ProductType.ToListAsync();
		}

		public async Task<ProductType> Get(int ID) {
			return await _context.ProductType.FindAsync(ID);
		}
	}
} 

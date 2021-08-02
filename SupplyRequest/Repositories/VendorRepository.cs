using Microsoft.EntityFrameworkCore;
using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories
{
	public class VendorRepository : IVendorRepository
	{
		private readonly SupplyRequestContext _context;
		public VendorRepository(SupplyRequestContext context)
		{
			_context = context;
		}
		public async Task<Vendor> Create(Vendor vendor)
		{
			_context.Vendors.Add(vendor);
			await _context.SaveChangesAsync();
			
			return vendor;
		}

		public async Task<IEnumerable<Vendor>> Create(IEnumerable<Vendor> vendor)
		{
			_context.Vendors.AddRange(vendor);
			await _context.SaveChangesAsync();

			return vendor;
		}

		public async Task Delete(int ID)
		{
			var vendor = await _context.Vendors.FindAsync(ID);
			_context.Vendors.Remove(vendor);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Vendor>> Get()
		{
			return await _context.Vendors.ToListAsync();
		}

		public async Task<Vendor> Get(int ID)
		{
			return await _context.Vendors.FindAsync(ID);
		}
	}
}

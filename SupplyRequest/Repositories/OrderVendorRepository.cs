using Microsoft.EntityFrameworkCore;
using SupplyRequestAPI.Models;
using System;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories
{
	public class OrderVendorRepository : IOrderVendorRepository
	{
		private readonly SupplyRequestContext _context;

		public OrderVendorRepository(SupplyRequestContext context)
		{
			_context = context;
		}

		public async Task<OrderVendor> Create(OrderVendor orderVendor)
		{
			Order order = _context.Orders.Find(orderVendor.OrderID);

			if (order == null)
			{
				throw new Exception();
			}
			Vendor vendor = _context.Vendors.Find(orderVendor.VendorID);

			if (vendor == null)
			{
				throw new Exception();
			}

			_context.OrderVendors.Add(orderVendor);
			await _context.SaveChangesAsync();

			return orderVendor;
		}

		public async Task<OrderVendor> Get(int orderID, int vendorID)
		{
			OrderVendor orderVendor = await _context.OrderVendors.FirstOrDefaultAsync(ov => ov.VendorID == vendorID && ov.OrderID == orderID);

			return orderVendor;
		}
	}
}

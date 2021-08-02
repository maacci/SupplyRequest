using SupplyRequestAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EFCore.BulkExtensions;

namespace SupplyRequestAPI.Repositories {
	public class OrderRepository : IOrderRepository {

		private readonly SupplyRequestContext _context;

		public OrderRepository (SupplyRequestContext context) {
			_context = context;
		}

		public async Task<Order> Create(Order order) {
			User user = _context.User.Find(order.User.ID);

			if (user == null)
			{
				throw new Exception();
			}
			
			List<OrderItem> newItems = new();
			foreach (OrderItem item in order.OrderItems)
			{
				Product existingProduct = _context.Product.Find(item.Product.ID);
				if (existingProduct != null)
				{
					newItems.Add(new OrderItem()
					{
						Quantity = item.Quantity,
						Product = existingProduct
					});
				}
			}

			List<Vendor> vendors = order.Vendors
				.Where(i => _context.Vendors.Any(p => p.ID == i.ID))
				.Select(i => _context.Vendors.Find(i.ID))
				.ToList();

			Order newOrder = _context.Orders.Add(new()
			{
				Created = DateTime.Now,
				User = user,
				Vendors = vendors,
				StatusID = Helpers.OrderStatus.Created,
				OrderItems = newItems
			}).Entity;


			foreach(Vendor v in newOrder.Vendors)
			{
				_context.OrderVendors.Add(new()
				{
					OrderID = newOrder.ID,
					VendorID = v.ID,
					Requested = false
				});
			}

			await _context.SaveChangesAsync();

			return newOrder;
		}

		public async Task Delete(int ID) {
			var order = await _context.Orders.FindAsync();
			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Order>> Get() {
			return await _context.Orders.ToListAsync();
		}

		public async Task<Order> Get(int ID) {
			var orders = await _context.Orders
				.FindAsync(ID);

			return orders;
		}

		public async Task<IEnumerable<Order>> GetByVendor(int vendorID)
		{

			var result = await _context.Orders
				.Where(o => o.Vendors.Any(i => i.ID == vendorID))
				.ToListAsync();

			return result;
		}

		public async Task<Order> GetByVendor(int vendorID, int orderID)
		{
			var orders = await _context.Orders
				.FirstOrDefaultAsync(o => o.ID == orderID && o.Vendors.Any(i => i.ID == vendorID));

			if (orders != null)
			{
				_context.OrderVendors.Add(new()
				{
					OrderID = orderID,
					VendorID = vendorID,
					Requested = true
				});
				await _context.SaveChangesAsync();
			}
			return orders;
		}

		public async Task<OrderVendor> GetByVendorStatus(int vendorID, int orderID)
		{
			return await _context.OrderVendors.FirstOrDefaultAsync(ov => ov.VendorID == vendorID && ov.OrderID == orderID);
		}
	}
}

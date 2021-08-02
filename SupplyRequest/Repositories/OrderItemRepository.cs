using Microsoft.EntityFrameworkCore;
using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories
{
	public class OrderItemRepository : IOrderItemRepository
	{
		private readonly SupplyRequestContext _context;
		public OrderItemRepository(SupplyRequestContext context)
		{
			_context = context;
		}
		public async Task<OrderItem> Create(OrderItem orderItem)
		{
			Product existingProduct = _context.Product.Find(orderItem.Product);

			if (existingProduct != null)
			{
				OrderItem newOrderitem = new OrderItem()
				{
					ID = orderItem.ID,
					Quantity = orderItem.Quantity,

				};
				_context.OrderItem.Add(newOrderitem);

				await _context.SaveChangesAsync();
			}
			return orderItem;

		}

		public async Task<IEnumerable<OrderItem>> Create(IEnumerable<OrderItem> orderItem)
		{
			_context.OrderItem.AddRange(orderItem);
			await _context.SaveChangesAsync();

			return orderItem;
		}

		public async Task Delete(int ID)
		{
			var orderItem = await _context.OrderItem.FindAsync(ID);
			_context.OrderItem.Remove(orderItem);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<OrderItem>> Get()
		{
			return await _context.OrderItem.ToListAsync();
		}

		public async Task<OrderItem> Get(int ID)
		{
			return await _context.OrderItem.FindAsync(ID);
		}
	}
}

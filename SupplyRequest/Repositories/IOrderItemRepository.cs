using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories
{
	interface IOrderItemRepository
	{
		Task<IEnumerable<OrderItem>> Get();

		Task<OrderItem> Get(int ID);

		Task<OrderItem> Create(OrderItem order);

		Task Delete(int ID);
	}
}

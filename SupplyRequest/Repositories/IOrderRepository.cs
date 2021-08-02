using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories {
	public interface IOrderRepository {

		Task<IEnumerable<Order>> Get();

		Task<Order> Get(int ID);

		Task<IEnumerable<Order>> GetByVendor(int vendorID);

		Task<Order> GetByVendor(int vendorID, int orderID);

		Task<OrderVendor> GetByVendorStatus(int vendorID, int orderID);

		Task<Order> Create(Order order);

		Task Delete(int ID);

	}
}

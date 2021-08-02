
using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories
{
	public interface IOrderVendorRepository
	{
		Task<OrderVendor> Get(int orderID, int vendorID);

		Task<OrderVendor> Create(OrderVendor orderVendo);
	}
}

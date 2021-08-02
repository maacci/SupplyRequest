using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories
{
	public interface IVendorRepository
	{
		Task<IEnumerable<Vendor>> Get();

		Task<Vendor> Get(int ID);

		Task<Vendor> Create(Vendor vendor);

		Task<IEnumerable<Vendor>> Create(IEnumerable<Vendor> vendor);

		Task Delete(int ID);
	}
}

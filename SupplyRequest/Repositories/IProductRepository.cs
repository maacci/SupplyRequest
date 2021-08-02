using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories {
	public interface IProductRepository {
		Task<IEnumerable<Product>> Get();

		Task<Product> Get(int ID);

		Task<Product> Create(Product product);

		Task<IEnumerable<Product>> Create(IEnumerable<Product> product);

		Task Delete(int ID);
	}
}

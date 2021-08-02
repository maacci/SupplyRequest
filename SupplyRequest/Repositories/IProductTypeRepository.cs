using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories {
	public interface IProductTypeRepository {
		Task<IEnumerable<ProductType>> Get();

		Task<ProductType> Get(int ID);

		Task<ProductType> Create(ProductType productType);

		Task<IEnumerable<ProductType>> Create(IEnumerable<ProductType> productType);

		Task Delete(int ID);
	}
}

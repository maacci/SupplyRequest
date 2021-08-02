using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> Get();

		Task<User> Get(int ID);

		Task<User> Create(User user);

		Task<IEnumerable<User>> Create(IEnumerable<User> users);

		Task Delete(int ID);
	}
}

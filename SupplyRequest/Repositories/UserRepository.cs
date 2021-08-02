using Microsoft.EntityFrameworkCore;
using SupplyRequestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly SupplyRequestContext _context;
		public UserRepository(SupplyRequestContext context)
		{
			_context = context;
		}
		public async Task<User> Create(User user)
		{
			_context.User.Add(user);
			await _context.SaveChangesAsync();

			return user;
		}

		public async Task<IEnumerable<User>> Create(IEnumerable<User> user)
		{
			_context.User.AddRange(user);
			await _context.SaveChangesAsync();

			return user;
		}

		public async Task Delete(int ID)
		{
			var user = await _context.User.FindAsync(ID);
			_context.User.Remove(user);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<User>> Get()
		{
			return await _context.User.ToListAsync();
		}

		public async Task<User> Get(int ID)
		{
			return await _context.User.FindAsync(ID);
		}
	}
}

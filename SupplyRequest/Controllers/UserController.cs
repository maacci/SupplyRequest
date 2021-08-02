using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplyRequestAPI.Models;
using SupplyRequestAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _repository;

		public UserController(IUserRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<UserDto>> GetUsers()
		{
			IEnumerable<User> users = await _repository.Get();

			return users.Select(b => new UserDto()
			{
				ID = b.ID,
				Name = b.Name,
				Active = b.Active
			}).ToList();
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserDto>> GetUsers(int id)
		{
			User user = await _repository.Get(id);
			if (user == null)
			{
				return NotFound();
			}
			UserDto userDtoNew = new()
			{
				ID = user.ID,
				Active = user.Active,
				Name = user.Name
			};

			return Ok(userDtoNew);
		}
		
		
		[HttpPost]
		public async Task<ActionResult<UserDto>> PostUsers([FromBody] User user) {
			if (ModelState.IsValid)
			{
				try
				{
					var newUser = await _repository.Create(user);

					UserDto userDtoNew = new()
					{
						ID = newUser.ID,
						Active = newUser.Active,
						Name = newUser.Name
					};

					return CreatedAtAction(nameof(GetUsers), new { id = userDtoNew.ID }, userDtoNew);
				} catch (Exception)
				{
					ModelState.AddModelError("Error", "Error saving data in the database.");
					return BadRequest(ModelState);
				}

			} else
			{
				return BadRequest();
			}

		}
	}
}

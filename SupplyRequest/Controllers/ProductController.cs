using SupplyRequestAPI.Models;
using SupplyRequestAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace SupplyRequestAPI.Controllers {
	[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
		private readonly IProductRepository _repository;

		public ProductController(IProductRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<Product>> GetProducts() {
			return await _repository.Get();
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Product>> GetProducts(int id) {
			var product = await _repository.Get(id);
			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		
		[HttpPost]
		public async Task<ActionResult<Product>> PostProducts([FromBody] Product product) {
			if (ModelState.IsValid)
			{
				try
				{
					var newProduct = await _repository.Create(product);
					return CreatedAtAction(nameof(GetProducts), new { id = newProduct.ID }, newProduct);
				}
				catch (Exception)
				{
					ModelState.AddModelError("Error", "Error saving data in the database.");
					return BadRequest(ModelState);
				}
			}
			else
			{
				return BadRequest();
			}
		}
		
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var toDelete = await _repository.Get(id);
			if (toDelete == null)
				return NotFound();

			await _repository.Delete(toDelete.ID);
			return NoContent();
		}
	}
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplyRequestAPI.Models;
using SupplyRequestAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
		private readonly IProductTypeRepository _repository;

		public ProductTypeController(IProductTypeRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<ProductType>> GetProductTypes() {
			return await _repository.Get();
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductType>> GetProductTypes(int id) {
			var productType = await _repository.Get(id);
			if (productType == null)
			{
				return NotFound();
			}

			return Ok(productType);
		}

		
		[HttpPost]
		public async Task<ActionResult<ProductType>> PostProductTypes([FromBody] ProductType productType) {
			if (ModelState.IsValid)
			{
				try
				{
					var newProductType = await _repository.Create(productType);
					return CreatedAtAction(nameof(GetProductTypes), new { id = newProductType.ID }, newProductType);
				}
				catch (Exception)
				{
					ModelState.AddModelError("Error", "Error saving data in the database.");
					return BadRequest(ModelState);
				}
			} else
			{
				return BadRequest();
			}

		}

		/*
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var toDelete = await _repository.Get(id);
			if (toDelete == null)
				return NotFound();

			await _repository.Delete(toDelete.ID);
			return NoContent();
		}*/
	}
}
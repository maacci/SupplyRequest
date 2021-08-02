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
	public class VendorController : ControllerBase
	{
		private readonly IVendorRepository _repository;

		public VendorController(IVendorRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<VendorDto>> GetVendors()
		{
			IEnumerable<Vendor> vendors = await _repository.Get();

			return vendors.Select(b => new VendorDto()
			{
				ID = b.ID,
				Name = b.Name,
				Active = b.Active
			}).ToList();
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<VendorDto>> GetVendors(int id)
		{
			Vendor vendor = await _repository.Get(id);
			if (vendor == null)
			{
				return NotFound();
			}
			VendorDto vendorDtoNew = new()
			{
				ID = vendor.ID,
				Active = vendor.Active,
				Name = vendor.Name
			};

			return Ok(vendorDtoNew);
		}

		
		[HttpPost]
		public async Task<ActionResult<VendorDto>> PostVendors([FromBody] VendorDto vendorDto) {
			if (ModelState.IsValid)
			{
				try
				{
					Vendor vTemp = new()
					{
						Active = vendorDto.Active,
						Name = vendorDto.Name
					};
					Vendor vendor = await _repository.Create(vTemp);

					VendorDto vendorDtoNew = new()
					{
						ID = vendor.ID,
						Active = vendor.Active,
						Name = vendor.Name
					};

					return CreatedAtAction(nameof(GetVendors), new { id = vendorDtoNew.ID }, vendorDtoNew);
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

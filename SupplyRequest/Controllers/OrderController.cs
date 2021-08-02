using SupplyRequestAPI.Models;
using SupplyRequestAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace SupplyRequestAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
    public class OrderController : ControllerBase
    {
		private readonly IOrderRepository _repository;

		public OrderController(IOrderRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<Order>> GetOrders() {
			return await _repository.Get();
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Order>> GetOrders(int id) {
			var order = await _repository.Get(id);
			if (order == null)
			{
				return NotFound();
			}

			return Ok(order);
		}

		[HttpGet("vendorConsumer/{vendorid}")]
		public async Task<IEnumerable<OrderDto>> GetOrdersByVendor(int vendorid)
		{
			IEnumerable<Order> orders = await _repository.GetByVendor(vendorid);

			return orders.Select(b => new OrderDto()
			{
				ID = b.ID,
				Created = b.Created,
				OrderItems = b.OrderItems,
				StatusID = b.StatusID
			}).ToList();
		}

		[HttpGet("vendorConsumer/{vendorid}/{orderid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<OrderDto>> GetOrdersByVendor(int vendorid, int orderid)
		{
			var order = await _repository.GetByVendor(vendorid, orderid);
			if (order == null)
			{
				return NotFound();
			}

			OrderDto orderDtoNew = new()
			{
				ID = order.ID,
				Created = order.Created,
				OrderItems = order.OrderItems,
				StatusID = order.StatusID
			};

			return Ok(orderDtoNew);
		}

		[HttpGet("vendorConsumer/{vendorid}/{orderid}/Status")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<OrderVendor>> GetOrdersByVendorStatus(int vendorid, int orderid)
		{
			var orderVendor = await _repository.GetByVendorStatus(vendorid, orderid);
			if (orderVendor == null)
			{
				orderVendor = new()
				{
					OrderID = orderid,
					VendorID = vendorid,
					Requested = false
				};
			}
			return Ok(orderVendor);
		}

		[HttpPost]
		public async Task<ActionResult<Order>> PostOrder([FromBody] Order order) {
			if (ModelState.IsValid)
			{
				try
				{
					var newOrder = await _repository.Create(order);
					return CreatedAtAction(nameof(GetOrders), new { id = newOrder.ID }, newOrder);
				}
				catch (Exception ex)
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
	}
}
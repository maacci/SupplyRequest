using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Models
{
	public class OrderDto
	{
		public int ID { get; set; }
		public DateTime Created { get; set; }
		public virtual List<OrderItem> OrderItems { get; set; }
		public virtual Helpers.OrderStatus StatusID { get; set; }
	}
}

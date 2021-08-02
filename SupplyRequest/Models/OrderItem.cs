using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Models {
	/// <summary>
	/// Represents each individual product requested in a given order.
	/// </summary>
	public class OrderItem {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public virtual Product Product { get; set; }
		public int Quantity { get; set; }

	}
}

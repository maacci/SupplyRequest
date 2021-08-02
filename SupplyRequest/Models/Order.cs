using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Models {
	public class Order {
		/// <summary>
		/// TODO (doc): Documenting...
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public DateTime Created { get; set; }
		public virtual List<OrderItem> OrderItems { get; set; }
		public virtual List<Vendor> Vendors { get; set; }

		[DefaultValue(Helpers.OrderStatus.Created)]
		public virtual Helpers.OrderStatus StatusID { get; set;}
		public virtual User User { get; set; }
	}
}

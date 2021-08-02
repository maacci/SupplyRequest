using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Models
{
	public class OrderVendor
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public int OrderID { get; set; }
		public int VendorID { get; set; }
		[DefaultValue(false)]
		public bool Requested { get; set; }
	}
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyRequestAPI.Models
{
	public class ProductType {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string Description { get; set; }
		[DefaultValue(true)]
		public bool Active { get; set; }
	}
}

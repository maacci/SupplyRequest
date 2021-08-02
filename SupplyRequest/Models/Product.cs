using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyRequestAPI.Models
{
	public class Product {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public int TypeId { get; set; }
		public virtual ProductType ProductType { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string SKU { get; set; }
		public bool Active { get; set; }
	}
}

using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyRequestAPI.Models
{
	public class User {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string UserKey { get; set; }
		public string UserSecret { get; set; }
		public string Name { get; set; }
		public bool Active { get; set; }
	}
}

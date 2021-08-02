using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SupplyRequestAPI.Models
{
	public class VendorDto
	{
		public int ID { get; set; }
		[StringLength(50, MinimumLength = 2)]
		public string Name { get; set; }
		[DefaultValue(true)]
		public bool Active { get; set; }

	}
}

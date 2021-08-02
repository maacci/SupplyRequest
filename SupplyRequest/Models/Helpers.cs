using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyRequestAPI.Models
{
	public static class Helpers
	{
		public enum OrderStatus
		{
			Created = 0,
			PartiallySent = 1,
			Sent = 2
		}
	}
}

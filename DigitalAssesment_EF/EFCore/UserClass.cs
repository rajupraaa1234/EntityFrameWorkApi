using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalAssesment_EF.Models
{
	[Table("user")]
	public class UserClass
	{
		[Key ,Required]
		public int id { get; set; }
		public string username { get; set; }
		public string password { get; set; }

	}
}


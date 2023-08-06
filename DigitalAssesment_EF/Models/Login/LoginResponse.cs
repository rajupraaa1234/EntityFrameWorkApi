using System;
namespace DigitalAssesment_EF.Models.Login
{
	public class LoginResponse
	{
		public Guid id { get; set; }
		public string username { get; set; }
		public string Jwt { get; set; }
	}
}


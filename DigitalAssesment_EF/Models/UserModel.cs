using System;
namespace DigitalAssesment_EF.Models
{
	public class UserModel
	{
        public Guid id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}


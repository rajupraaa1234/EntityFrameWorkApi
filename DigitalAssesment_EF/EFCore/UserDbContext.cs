using System;
using Microsoft.EntityFrameworkCore;

namespace DigitalAssesment_EF.Models
{
	public class UserDbContext : DbContext
	{
		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
		public DbSet<UserClass> Users { get; set; }
	}
}


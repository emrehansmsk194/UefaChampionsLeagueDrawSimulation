using Microsoft.EntityFrameworkCore;
using UclDrawAPI.Models;

namespace UclDrawAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Team> Teams { get; set; }


	}
}

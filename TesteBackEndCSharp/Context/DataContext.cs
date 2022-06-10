using Microsoft.EntityFrameworkCore;
using TesteBackEndCSharp.Models;

namespace TesteBackEndCSharp.Context
{
    public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		public DbSet<Money>? Money { get; set; }

	}
}


using System;
using Microsoft.EntityFrameworkCore;
using TesteBackEndCSharp.Models;

namespace TesteBackEndCSharp.Context
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		public DbSet<TesteBackEndCSharp.Models.Money>? Money { get; set; }
	}
}


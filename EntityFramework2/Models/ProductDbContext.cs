using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework2.Models
{
	public class ProductDbContext : DbContext
	{
		//Tạo logger
		public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
		{
			builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
			builder.AddConsole();
		});

		//Tạo bảng trong DbContext
		public DbSet<Product> products { get; set; }

		public DbSet<Category> categories { get; set; }

		private const string connectionString = @"
					Data Source=localhost;
					Initial Catalog=shopdata;
					Integrated Security=True;
					TrustServerCertificate=True;";

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLoggerFactory(loggerFactory);
			optionsBuilder.UseSqlServer(connectionString);
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
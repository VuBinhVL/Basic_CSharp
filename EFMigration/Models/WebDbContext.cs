using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMigration.Models
{
	public class WebDbContext : DbContext
	{
		//Tạo logger
		public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
		{
			builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
			builder.AddConsole();
		});

		//Tạo các bảng
		public DbSet<Article> articles { get; set; }

		public DbSet<Tag> tags { get; set; }

		public DbSet<ArticleTag> articleTags { get; set; }

		//Tạo chuỗi kết nối
		private const string connectionString = @"
					Data Source=localhost;
					Initial Catalog=webdb;
					Integrated Security=True;
					TrustServerCertificate=True;";

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLoggerFactory(loggerFactory);
			optionsBuilder.UseSqlServer(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<ArticleTag>(e =>
			{
				e.HasIndex(articleTag => new { articleTag.ArticleId, articleTag.TagId })
					.IsUnique();
			});
		}
	}
}
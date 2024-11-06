using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Models
{
	public class ShopContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }

		// Hàm giúp hiển thị câu truy vấn trong console
		public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
		{
			builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
			builder.AddConsole();
		});

		private string connectString = @"
                Data Source=LAPTOP-J7OECDJF\SQLEXPRESS;
                Initial Catalog=datashop;
                Integrated Security=True;
                Encrypt=True;
                TrustServerCertificate=True";

		// giúp cấu hình DbContext bằng cách chỉ định chuỗi kết nối
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(connectString);
		}
	}
}
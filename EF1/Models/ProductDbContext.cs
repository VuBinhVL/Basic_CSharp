using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF1.Models
{
	public class ProductDbContext : DbContext
	{
		public DbSet<Product> products { get; set; }

		private string connectString = @"
                Data Source=LAPTOP-J7OECDJF\SQLEXPRESS;
                Initial Catalog=data01;
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
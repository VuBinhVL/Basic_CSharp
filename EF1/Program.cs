using EF1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EF1
{
	public class Program
	{
		private static void CreateDB()
		{
			using var dbContext = new ProductDbContext();
			string dbname = dbContext.Database.GetDbConnection().Database; //lấy tên db
			bool kq = dbContext.Database.EnsureCreated();
			if (kq)
			{
				Console.WriteLine($"Tạo {dbname} thành công");
			}
			else
			{
				Console.WriteLine("Ko thành công");
			}
		}

		private static void DropDB()
		{
			using var dbContext = new ProductDbContext();
			string dbname = dbContext.Database.GetDbConnection().Database; //lấy tên db
			bool kq = dbContext.Database.EnsureDeleted();
			if (kq)
			{
				Console.WriteLine("Xóa thành công database " + dbname);
			}
			else
			{
				Console.WriteLine("Ko xóa đc");
			}
		}

		private static void Insert()
		{
			using var dbContext = new ProductDbContext();
			var products = new object[]
			{
				new Product {ProductName = "Laptop Acer", Provider = "Acer"},
				new Product {ProductName = "Laptop Asus", Provider = "Asus"},
				new Product {ProductName = "Laptop Dell", Provider = "Dell"},
				new Product {ProductName = "Laptop Lenovo", Provider = "Lenovo"}
			};
			dbContext.AddRange(products);
			int row_insert = dbContext.SaveChanges();
			Console.WriteLine("Số lượng cột đã chèn là: " + row_insert);
		}

		private static void ReadProduct()
		{
			using var dbContext = new ProductDbContext();
			var products = (from p in dbContext.products
							where p.ProductName.Contains("Asus")
							select p).FirstOrDefault();
			if (products != null)
			{
				products.PrintInfor();
			}
			else
			{
				Console.WriteLine("Ko tìm thấy");
			}
		}

		private static void UpdateProduct(int id, string newName, string newProvider)
		{
			using var dbContext = new ProductDbContext();
			Product product = (from p in dbContext.products
							   where p.ProductId == id
							   select p).FirstOrDefault();
			if (product != null)
			{
				product.Provider = newProvider;
				product.ProductName = newName;
				int row_up = dbContext.SaveChanges();
				Console.WriteLine("Đã cập nhật sản phẩm");
			}
			else
			{
				Console.WriteLine("Ko cập nhật");
			}
		}

		private static void DeleteProduct(int id)
		{
			using var dbContext = new ProductDbContext();
			Product product = (from p in dbContext.products
							   where p.ProductId == id
							   select p).FirstOrDefault();
			if (product != null)
			{
				dbContext.Remove(product);
				dbContext.SaveChanges();
				Console.WriteLine("Đã xóa sản phẩm");
			}
			else
			{
				Console.WriteLine("Ko xóa đc");
			}
		}

		private static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			/*CreateDB();*/
			/*DropDB();*/
			/*Insert();*/
			ReadProduct();
			/*UpdateProduct(1, "Laptop XS", "XS");*/
			DeleteProduct(8);
		}
	}
}
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
	internal class Program
	{
		private static void CreateDatabase()
		{
			using var dbContext = new ProductDbContext();   //Sử dụng database DBContext
			string databaseName = dbContext.Database.GetDbConnection().Database;  //Lấy tên database

			var kq = dbContext.Database.EnsureCreated(); //tạp database
			if (kq)
			{
				Console.WriteLine($"Tạo database {databaseName} thành công");
			}
			else
				Console.WriteLine($"Tạo database {databaseName} thất bại");
		}

		private static void DropDatabase()
		{
			using var dbContext = new ProductDbContext();   //Sử dụng database DBContext
			string databaseName = dbContext.Database.GetDbConnection().Database;  //Lấy tên database

			var kq = dbContext.Database.EnsureDeleted(); //xóa database
			if (kq)
			{
				Console.WriteLine($"Xóa database {databaseName} thành công");
			}
			else
				Console.WriteLine($"Xóa database {databaseName} thất bại");
		}

		private static void Insert()
		{
			using var dbContext = new ProductDbContext();

			//1. Tạo model
			var products = new List<Product>()
			{
				new Product () {ProductName = "Sản phẩm 1", Provider = "Công ty 1"},
				new Product () {ProductName = "Sản phẩm 2", Provider= "Công ty 2"}
			};

			//2. Insert vào Db bằng Add, AddSync ...
			dbContext.AddRange(products);

			//3. Lưu lại
			int kq = dbContext.SaveChanges();
			Console.WriteLine($"Đã chèn {kq} dòng dữ liệu vào DB");
		}

		private static void ReadProduct()
		{
			using var dbContext = new ProductDbContext();

			//1. Lấy danh sách product
			Console.WriteLine("\n1. Lấy danh sách product");
			var listProduct = dbContext.products.ToList();
			listProduct.ForEach(p => Console.WriteLine(p));

			//2. Lấy sản phẩm đầu tiên có Id = 2
			Console.WriteLine("\n2. Lấy sản phẩm đầu tiên có Id = 2");
			var product = (from p in dbContext.products
						   where p.ProductId == 2
						   select p).FirstOrDefault();
			if (product != null)
			{
				Console.WriteLine(product);
			}
			else
				Console.WriteLine("Không tìm thấy sản phẩm");

			//3. Sắp xếp sản phẩm theo id giảm dần
			Console.WriteLine("\n3. Sắp xếp sản phẩm theo id giảm dần");
			var products = from p in dbContext.products
						   orderby p.ProductId descending
						   select p;
			products.ToList().ForEach(p => Console.WriteLine(p));
		}

		private static void Remove(int id)
		{
			//4. Xóa sản phẩm theo ID
			Console.WriteLine("\n4. Xóa sản phẩm theo ID");
			using var dbContext = new ProductDbContext();
			var product = (from p in dbContext.products
						   where p.ProductId == id
						   select p).FirstOrDefault();
			if (product != null)
			{
				dbContext.products.Remove(product);
				int kq = dbContext.SaveChanges();
				Console.WriteLine($"Đã xóa {kq} dữ liệu!");
			}
		}

		private static void ChangeName(int id, string name)
		{
			//5.Thay đổi tên sản phẩm theo id
			Console.WriteLine("\n5. Thay đổi tên sản phẩm theo ID");
			using var dbContext = new ProductDbContext();
			var product = (from p in dbContext.products
						   where p.ProductId == id
						   select p).FirstOrDefault();
			if (product != null)
			{
				product.ProductName = name;
				int kq = dbContext.SaveChanges();
				Console.WriteLine($"Đã update {kq} dữ liệu!");
			}
		}

		private static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			DropDatabase();
			CreateDatabase();
			Insert();
			ReadProduct();
			ChangeName(1, "Laptop Asus");
			Remove(2);
		}
	}
}
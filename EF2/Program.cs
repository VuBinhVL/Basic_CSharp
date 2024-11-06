using EF2.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EF2
{
	public class Program
	{
		private static void CreateDB()
		{
			using var dbContext = new ShopContext();
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
			using var dbContext = new ShopContext();
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

		private static void InsertData()
		{
			using ShopContext dbContext = new ShopContext();
			Category category1 = new Category() { Name = "Điện thoại", Description = "Các loại điện thoại" };
			Category category2 = new Category() { Name = "Nước uống", Description = "Các loại đồ uống" };
			dbContext.Categories.Add(category1);
			dbContext.Categories.Add(category2);
			dbContext.SaveChanges();

			dbContext.Add(new Product() { Name = "Iphone 8", Price = 12000, Cateid = 1 });
			dbContext.Add(new Product() { Name = "Iphone XS", Price = 1232000, Cateid = 1 });
			dbContext.Add(new Product() { Name = "Iphone 15", Price = 92000, Cateid = 1 });
			dbContext.Add(new Product() { Name = "Bia", Price = 18000, Cateid = 2 });
			dbContext.Add(new Product() { Name = "Wine", Price = 90000, Cateid = 2 });
			dbContext.SaveChanges();
		}

		private static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			/*DropDB();
			CreateDB();
			InsertData();*/
			using var dbContext = new ShopContext();
			var product = (from p in dbContext.Products
						   where p.Cateid == 1
						   select p).FirstOrDefault();

			//Giúp truy vấn đến khóa ngoại
			var e = dbContext.Entry(product);
			e.Reference(p => p.Category).Load();
			if (product.Category == null)
			{
				Console.WriteLine("Ko truy vấn đc");
			}
			else
			{
				product.PrintInfor();
			}

			//truy vấn collect nav của category
			var category = (from p in dbContext.Categories
							where p.CategoryId == 1
							select p).FirstOrDefault();
			var e1 = dbContext.Entry(category);
			e1.Collection(c => c.Products).Load();
			if (category.Products == null)
			{
				Console.WriteLine("Ko truy vấn dc collect");
			}
			else
			{
				Console.WriteLine($"Số sản phẩm là: {category.Products.Count}");
				category.Products.ForEach(p => p.PrintInfor());
			}
		}
	}
}
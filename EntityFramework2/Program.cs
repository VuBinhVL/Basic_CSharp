using EntityFramework2.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework2
{
	//Muốn tự động load danh sách sản phẩm trong danh mục hoặc danh mục của 1 sản phẩm
	// Dùng thư viện proxies hổ trợ cho việc này(lazy load) hoặc dùng Entry của DbContext
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

			var categoryList = new List<Category>()
			{
				new Category() {Name = "Điện thoại", Description = "Danh sách các điện thoại"},
				new Category() {Name = "Đồ uống", Description = "Danh sách các đồ uống"}
			};

			var productList = new List<Product>()
			{
				new Product() {Name = "IphoneX" , Price = 15000000, CateId = 1},
				new Product() {Name = "SamSung 12 Ultra" , Price = 12000000, CateId = 1},
				new Product() {Name = "Xiaomi RedNote 15" , Price = 9000000, CateId = 1},
				new Product() {Name = "Bia" , Price = 8000, CateId = 2},
				new Product() {Name = "Nước ngọt" , Price = 9000, CateId = 2},
			};

			dbContext.AddRange(productList);
			dbContext.AddRange(categoryList);
			int kq = dbContext.SaveChanges();
			Console.WriteLine($"Thêm {kq} hàng dữ liệu thành công");
		}

		private static void GetProductWithLazyLoad()
		{
			using var dbContext = new ProductDbContext();
			//Lấy danh sách tất cả sản phẩm trong danh mục 1
			var qr = (from c in dbContext.categories
					  where c.CategoryId == 1
					  select c).FirstOrDefault();
			if (qr.ListProducts != null)
				qr.ListProducts.ToList().ForEach(x => Console.WriteLine(x));
			else
				Console.WriteLine("Không có sản phẩm");
		}

		private static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			DropDatabase();
			CreateDatabase();
			Insert();
		}
	}
}
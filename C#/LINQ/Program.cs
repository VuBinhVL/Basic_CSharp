namespace LINQ
{
	public class Product
	{
		public int ID { set; get; }
		public string Name { set; get; }         // tên
		public double Price { set; get; }        // giá
		public string[] Colors { set; get; }     // các màu sắc
		public int Brand { set; get; }           // ID Nhãn hiệu, hãng

		public Product(int id, string name, double price, string[] colors, int brand)
		{
			ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
		}

		// Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
		public override string ToString()
		   => $"{ID,3} {Name,12} {Price,5} {Brand,2} {string.Join(",", Colors)}";
	}

	public class Brand
	{
		public string Name { set; get; }
		public int ID { set; get; }
	}

	public class Program
	{
		private static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			var brands = new List<Brand>() {
				new Brand{ID = 1, Name = "Công ty AAA"},
				new Brand{ID = 2, Name = "Công ty BBB"},
				new Brand{ID = 4, Name = "Công ty CCC"},
			};

			var products = new List<Product>()
			{
				new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
				new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
				new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
				new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
				new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
				new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
				new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
			};

			//1. In ra các sản phẩm có giá = 400
			var product = from p in products
						  where p.Price == 400
						  select p;
			foreach (var item in product)
				Console.WriteLine(item);

			//2. Select
			Console.WriteLine("=============== 2. Select =========================");
			var slect = products.Select((p) =>
			{
				return new
				{
					Ten = p.Name,
					Gia = p.Price
				};
			});
			foreach (var item in slect)
				Console.WriteLine(item);

			//3. SelectMany
			Console.WriteLine("================ 3. SelectMany ========================");
			var selectMany = products.SelectMany(p =>
			{
				return p.Colors;
			});
			foreach (var item in selectMany)
				Console.WriteLine(item);

			//4. Join - Lấy thông tin tên sản phẩm và brand tương ứng
			Console.WriteLine("========================================");
			var join = products.Join(brands, p => p.Brand, b => b.ID, (p, b) =>
			{
				return new
				{
					Ten = p.Name,
					ThuongHieu = b.Name,
				};
			});
			foreach (var item in join)
				Console.WriteLine(item);

			//5. GroupJoin - Nhóm các sản phẩm cùng 1 thương hiệu
			Console.WriteLine("===================== 5. GroupJoin - Nhóm các sản phẩm cùng 1 thương hiệu ===================");
			var groupJoin = brands.GroupJoin(products, b => b.ID, p => p.Brand, (brand, pros)
				=>
			{
				return new
				{
					TenThuongHieu = brand.Name,
					CacSanPham = pros
				};
			});
			foreach (var item in groupJoin)
			{
				Console.WriteLine($"Tên thương hiệu: {item.TenThuongHieu}");
				foreach (var sp in item.CacSanPham)
					Console.WriteLine(sp);
			}

			//6. Take - Lấy các sp đầu tiên
			Console.WriteLine("==================== 6. Take - Lấy các sp đầu tiên ====================");
			products.Take(2).ToList().ForEach(p => Console.WriteLine(p));

			//7. Skip - Bỏ các sp đầu tiên
			Console.WriteLine("================= 7. Skip - Bỏ các sp đầu tiên =======================");
			products.Skip(2).ToList().ForEach(p => Console.WriteLine(p));

			//8. Order - Sắp xếp tăng dần
			Console.WriteLine("================== 8. Order - Sắp xếp tăng dần ======================");
			products.OrderBy(p => p.Price).ToList().ForEach(p => Console.WriteLine(p));

			//9. OrderByDescending - Sắp xếp giảm dần
			Console.WriteLine("================ 9. OrderByDescending - Sắp xếp giảm dần ========================");
			products.OrderByDescending(p => p.Price).ToList().ForEach(p => Console.WriteLine(p));

			//10. GroupBy - Nhóm trong 1 tập hợp
			Console.WriteLine("================ 10. GroupBy - Nhóm trong 1 tập hợp ========================");
			var groupBy = products.GroupBy(p => p.Price);
			foreach (var group in groupBy)
			{
				Console.WriteLine(group.Key);
				foreach (var item in group)
					Console.WriteLine(item);
			}

			//11. Distinct - Loại bỏ các giá trị trùng nhau
			Console.WriteLine("================ 11. Distinct - Loại bỏ các giá trị trùng nhau ========================");
			products.SelectMany(p => p.Colors).Distinct().ToList().ForEach(p => Console.WriteLine(p));

			//12. Single - Lấy 1 phần tử thỏa mãn. Nếu ko có hoặc > 1 thì lỗi
			Console.WriteLine("================ 12. Single - Lấy 1 phần tử thỏa mãn. Nếu ko có hoặc > 1 thì lỗi ========================");
			var proSingle = products.Single(p => p.Price == 600);
			Console.WriteLine(proSingle);

			//13. SingleOrDefault - Lấy 1 phần tử thỏa mãn. Nếu ko có thì return null hoặc > 1 thì lỗi
			Console.WriteLine("================ 13. SingleOrDefault - Lấy 1 phần tử thỏa mãn. Nếu ko có thì return null hoặc > 1 thì lỗi ========================");
			var proSingleOrDef = products.SingleOrDefault(p => p.Price == 100);
			if (proSingleOrDef != null)
				Console.WriteLine(proSingle);

			//14. Any - Kiểm tra xem có 1 phần tử nào thỏa mãn điều kiện đã cho ko
			Console.WriteLine("================ 14. Any - Kiểm tra xem có 1 phần tử nào thỏa mãn điều kiện đã cho ko ========================");
			var any = products.Any(p => p.Price == 600);
			Console.WriteLine(any);

			//15. All - Kiểm tra xem tất cả phần tử nào thỏa mãn điều kiện đã cho ko
			Console.WriteLine("================ 15. All - Kiểm tra xem tất cả phần tử nào thỏa mãn điều kiện đã cho ko ========================");
			var all = products.All(p => p.Price >= 100);
			Console.WriteLine(all);

			//16. Count - Đếm
			Console.WriteLine("================ 16. Count - Đếm ========================");
			var count = products.Count(p => p.Price == 400);
			Console.WriteLine(count);

			//17. In ra các sản phẩm với tên sp, tên thương hiệu và giá (300-400) và giảm dần
			Console.WriteLine("================ 17. In ra các sản phẩm với tên sp, tên thương hiệu và giá (300-400) và giảm dần ========================");
			products.Where(p => p.Price >= 300 && p.Price <= 400)
								.OrderByDescending(p => p.Price)
								.Join(brands, p => p.Brand, b => b.ID,
								(p, b) =>
								{
									return new
									{
										TenSP = p.Name,
										TenTT = b.Name,
										Gia = p.Price,
									};
								}).ToList().ForEach(pros => Console.WriteLine($"{pros.TenSP,15} {pros.TenTT,10} {pros.Gia,7}"));
		}
	}
}
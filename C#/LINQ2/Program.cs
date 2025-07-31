namespace LINQ2
{
	#region	   Cách viết LINQ
	//1. Xác định nguồn : from tenphantu in IEnumberables
	//	 ...join, where, orderby, let tenbien = ??
	//2. Lấy dữ liệu select, group by...

	#endregion

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

			//1. In ra các sản phẩm với thông tin gồm tên sp và giá sp
			Console.WriteLine("=============== 1. In ra các sản phẩm với thông tin gồm tên sp và giá sp =========================");
			var kq1 = from p in products
					  select new
					  {
						  TenSp = p.Name,
						  Gia = p.Price
					  };
			kq1.ToList().ForEach(x => Console.WriteLine(x));

			//2. In ra các sản phẩm với thông tin gồm tên sp và giá sp. Với giá < 400
			Console.WriteLine("=============== 2. In ra các sản phẩm với thông tin gồm tên sp và giá sp (<400) =========================");
			var kq2 = from p in products
					  where p.Price < 400
					  select new
					  {
						  TenSp = p.Name,
						  Gia = p.Price
					  };
			kq2.ToList().ForEach(x => Console.WriteLine(x));

			//3. In ra các sản phẩm với thông tin gồm tên sp, các màu và giá sp. Với giá <= 500 và có màu xanh
			Console.WriteLine("===============3. In ra các sản phẩm với thông tin gồm tên sp, các màu và giá sp. Với giá < 400 và có màu xanh =========================");
			var kq3 = from p in products
					  from color in p.Colors
					  where p.Price < 500 && color == "Xanh"
					  // orderby p.Price
					  select new
					  {
						  TenSp = p.Name,
						  Gia = p.Price,
						  CacMau = p.Colors
					  };
			kq3.ToList().ForEach(x => Console.WriteLine($"{x.TenSp}, {x.Gia}, {string.Join(",", x.CacMau)}"));

			//4. Nhóm các sản phẩm theo giá tăng dần
			Console.WriteLine("=============== 4. Nhóm các sản phẩm theo giá tăng dần =========================");
			var kq4 = from p in products
					  group p by p.Price into gr
					  orderby gr.Key
					  select gr;
			kq4.ToList().ForEach(gr =>
			{
				Console.WriteLine(gr.Key);
				gr.ToList().ForEach(p => Console.WriteLine(p));
			});

			//5. Nhóm các sản phẩm theo giá tăng dần và trả về các đối tượng (tensp,giasp va sl)
			Console.WriteLine("=============== 5. Nhóm các sản phẩm theo giá tăng dần và trả về các đối tượng (tensp,giasp va sl)  =========================");
			var kq5 = from p in products
					  group p by p.Price into gr
					  orderby gr.Key
					  let sl = "Số lượng: " + gr.Count()
					  select new
					  {
						  Gia = gr.Key,
						  CacSanPham = gr.ToList(),
						  SoLuong = sl
					  };
			kq5.ToList().ForEach(x =>
			{
				Console.WriteLine(x.Gia);
				Console.WriteLine(x.SoLuong);
				x.CacSanPham.ForEach(a => Console.WriteLine(a));
			});

			//6. Lấy thông tin tensp, giasp và tentt
			Console.WriteLine("=============== 6. Lấy thông tin tensp, giasp và tentt  =========================");
			var kq6 = from p in products
					  join b in brands on p.Brand equals b.ID
					  select new
					  {
						  TenSP = p.Name,
						  GiaSP = p.Price,
						  TenTT = b.Name,
					  };
			kq6.ToList().ForEach(x => Console.WriteLine(x));

			//7. Lấy thông tin tensp, giasp và tentt. Nếu sản phẩm ko có thương hiệu thì in No brand
			Console.WriteLine("=============== 7. Lấy thông tin tensp, giasp và tentt. Nếu sản phẩm ko có thương hiệu thì in No brand  =========================");
			var kq7 = from pro in products
					  join brand in brands on pro.Brand equals brand.ID into t
					  from b in t.DefaultIfEmpty()
					  select new
					  {
						  TenSP = pro.Name,
						  GiaSP = pro.Price,
						  TenTT = (b != null) ? b.Name : "No Brand"
					  };
			kq7.ToList().ForEach(x => Console.WriteLine(x));
		}
	}
}
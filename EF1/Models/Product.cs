using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF1.Models
{
	[Table("product")] //Tạo bảng
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		[Required] //Ko đc null
		[StringLength(50)] //Độ dài kí tự
		public string ProductName { get; set; }

		[StringLength(50)]
		public string Provider { get; set; }

		public void PrintInfor()
		{
			Console.WriteLine(ProductId + " " + ProductName + " " + Provider);
		}
	}
}
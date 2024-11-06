using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Models
{
	[Table("Category")]
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Required]
		[StringLength(100)]
		public string Description { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		//Collect Navigation
		public List<Product> Products { get; set; }

		public void PrintInfor()
		{
			Console.WriteLine(CategoryId + " " + Name + " " + Description);
		}
	}
}
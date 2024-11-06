using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Models
{
	[Table("Product")]
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[Column("Tensnpham", TypeName = "ntext")]
		public string Name { get; set; }

		[Required]
		[Column(TypeName = "money")]
		public decimal Price { get; set; }

		public int? Cateid { get; set; }

		// Foreign key
		[ForeignKey("Cateid")]
		public Category Category { get; set; } // Fk => Pk (CategoryID)

		public void PrintInfor()
		{
			Console.WriteLine(Id + " " + Name + " " + Price + Cateid);
		}
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework2.Models
{
	[Table("Product")]
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		[Required]
		[Column("TenSanPham", TypeName = "ntext")]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		[Column(TypeName = "money")]
		public decimal Price { get; set; }

		public int CateId { get; set; }

		//Reference Navigation -> FK (1-n)
		[ForeignKey("CateId")]
		public virtual Category CategoryId { get; set; }

		public override string ToString()
		{
			return $"{ProductId} - {Name} - {Price} {CateId} ";
		}
	}
}
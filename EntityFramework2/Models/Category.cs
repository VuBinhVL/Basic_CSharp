using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework2.Models
{
	[Table("Category")]
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Column(TypeName = "ntext")]
		public string Description { get; set; }

		//Collect Navigation
		public virtual List<Product> ListProducts { get; set; }

		public override string ToString()
		{
			return $"{CategoryId} - {Name} - {Description} ";
		}
	}
}
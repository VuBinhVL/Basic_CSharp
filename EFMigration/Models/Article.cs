using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMigration.Models
{
	[Table("Article")]
	public class Article
	{
		[Key]
		public int ArticleId { set; get; }

		[StringLength(100)]
		public string? Name { set; get; }

		[Column(TypeName = "ntext")]
		public string Content { set; get; }
	}
}
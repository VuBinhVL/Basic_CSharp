using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMigration.Models
{
	[Table("ArticleTag")]
	public class ArticleTag
	{
		[Key]
		public int ArticleTagId { get; set; }

		public int TagId { get; set; } //FK

		public int ArticleId { get; set; }  //FK

		[ForeignKey("TagId")]
		public Tag Tag { get; set; }

		[ForeignKey("ArticleId")]
		public Article Article { get; set; }
	}
}
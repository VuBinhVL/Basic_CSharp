using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMigration.Models
{
	[Table("Tag")]
	public class Tag
	{
		[Column(TypeName = "ntext")]
		public string Content { set; get; }

		[Key]
		public int TagId { set; get; }
	}
}
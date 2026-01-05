using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;
public partial class SavedJob
	{
	
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		[Required]
		[ForeignKey(nameof(JobPost))]
		public Guid Job { get; set; }
		[Required]
		[ForeignKey(nameof(JobSeeker))]
		public Guid SavedBy { get; set; }
		public DateTime DateSaved { get; set; }
		public virtual JobPost JobPost { get; set; }
		public virtual JobSeeker JobSeeker { get; set; }
	
}



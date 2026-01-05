using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MessageGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int newCount { get; set; }
        public bool IsNewMessages { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Members { get; set; }
        public virtual IList<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
        public virtual IList<Message> Messages { get; set; } = new List<Message>();
    }
}

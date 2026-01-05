using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_js.Models;

[Table("AuthUser")]
public partial class AuthUser: SystemUser
{
    //public Guid Id { get; set; }

    //public Guid SystemUserId { get; set; }

    //public string Password { get; set; } = null!;

    //public virtual SystemUser IdNavigation { get; set; } = null!;

    //public virtual SystemUser SystemUser { get; set; } = null!;

    public string? Password { get; set; }
    //for chat
    public string? ConnectionId { get; set; }
    public bool? OnlineStatus { get; set; } = false;

}

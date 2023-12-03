using System;
using System.Collections.Generic;

namespace ScheduleService.Models;

public partial class Role
{
    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

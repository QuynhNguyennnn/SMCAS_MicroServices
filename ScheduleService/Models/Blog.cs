using System;
using System.Collections.Generic;

namespace ScheduleService.Models;

public partial class Blog
{
    public string BlogId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Context { get; set; } = null!;

    public DateTime WritingDate { get; set; }

    public DateTime PushlishedDate { get; set; }

    public bool IsDraft { get; set; }

    public bool IsActive { get; set; }

    public virtual User User { get; set; } = null!;
}

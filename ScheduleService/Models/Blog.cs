using System;
using System.Collections.Generic;

namespace ScheduleService.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Title { get; set; } = null!;


    public string Context { get; set; }

    public DateTime WritingDate { get; set; }

    public DateTime PublishedDate { get; set; }

    public bool IsDraft { get; set; }

    public bool IsActive { get; set; }

    public virtual User User { get; set; }
}

﻿using System;
using System.Collections.Generic;

namespace UserService.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public int UserId { get; set; }

    public string Context { get; set; } = null!;

    public DateTime WritingDate { get; set; }

    public DateTime PublishedDate { get; set; }

    public bool IsDraft { get; set; }

    public bool IsActive { get; set; }

    public virtual User User { get; set; } = null!;
}
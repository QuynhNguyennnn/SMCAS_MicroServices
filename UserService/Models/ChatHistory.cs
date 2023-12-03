using System;
using System.Collections.Generic;

namespace BlogService.Models;

public partial class ChatHistory
{
    public string ChatHistoryId { get; set; } = null!;

    public string ChatId { get; set; } = null!;

    public string ReceiverId { get; set; } = null!;

    public string SenderId { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime SendingTime { get; set; }

    public bool IsActive { get; set; }

    public virtual Chat Chat { get; set; } = null!;
}

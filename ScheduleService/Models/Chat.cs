using System;
using System.Collections.Generic;

namespace ScheduleService.Models;

public partial class Chat
{
    public string ChatId { get; set; } = null!;

    public string DoctorId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public DateTime ChatDate { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string TotalTime { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<ChatHistory> ChatHistories { get; set; } = new List<ChatHistory>();

    public virtual User Doctor { get; set; } = null!;

    public virtual User Patient { get; set; } = null!;
}

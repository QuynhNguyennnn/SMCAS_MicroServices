using System;
using System.Collections.Generic;

namespace BlogService.Models;

public partial class Feedback
{
    public string FeedbackId { get; set; } = null!;

    public string DoctorId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public DateTime FeedbackDate { get; set; }

    public string Message { get; set; } = null!;

    public int Rating { get; set; }

    public bool IsActive { get; set; }

    public virtual User Doctor { get; set; } = null!;

    public virtual User Patient { get; set; } = null!;
}

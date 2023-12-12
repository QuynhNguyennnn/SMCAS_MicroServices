using System;
using System.Collections.Generic;

namespace BlogService.Models;

public partial class MedicalExaminationSchedule
{
    public int ScheduleId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan StartShift { get; set; }

    public TimeSpan EndShift { get; set; }

    public bool IsActive { get; set; }

    public virtual User Doctor { get; set; }

    public virtual User Patient { get; set; }
}

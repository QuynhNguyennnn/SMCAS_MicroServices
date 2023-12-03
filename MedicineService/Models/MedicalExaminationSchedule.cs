using System;
using System.Collections.Generic;

namespace MedicineService.Models;

public partial class MedicalExaminationSchedule
{
    public string ScheduleId { get; set; } = null!;

    public string DoctorId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public DateTime Date { get; set; }

    public TimeSpan StartShift { get; set; }

    public TimeSpan EndShift { get; set; }

    public bool IsActive { get; set; }

    public virtual User Doctor { get; set; } = null!;

    public virtual User Patient { get; set; } = null!;
}

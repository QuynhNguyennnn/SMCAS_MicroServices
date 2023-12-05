using System;
using System.Collections.Generic;

namespace ScheduleService.Models;

public partial class ExaminatedRecord
{
    public int RecordId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public int? StaffId { get; set; }

    public decimal RespirationRate { get; set; }

    public decimal Temperature { get; set; }

    public decimal BloodPressure { get; set; }

    public string Note { get; set; } = null!;

    public decimal SpO2 { get; set; }

    public bool IsActive { get; set; }

    public virtual User Doctor { get; set; } = null!;

    public virtual User Patient { get; set; } = null!;

    public virtual User? Staff { get; set; }
}

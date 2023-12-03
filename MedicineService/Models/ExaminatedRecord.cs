using System;
using System.Collections.Generic;

namespace MedicineService.Models;

public partial class ExaminatedRecord
{
    public string RecordId { get; set; } = null!;

    public string DoctorId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public string? StaffId { get; set; }

    public decimal RespirationRate { get; set; }

    public decimal Temperature { get; set; }

    public decimal BloodPressure { get; set; }

    public string Note { get; set; } = null!;

    public decimal SpO2 { get; set; }

    public bool IsActive { get; set; }

    public virtual User Doctor { get; set; } = null!;

    public virtual ICollection<MedicineExaminatedRecord> MedicineExaminatedRecords { get; set; } = new List<MedicineExaminatedRecord>();

    public virtual User Patient { get; set; } = null!;

    public virtual User? Staff { get; set; }
}

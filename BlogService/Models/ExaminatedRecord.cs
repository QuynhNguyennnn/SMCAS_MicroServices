﻿using System;
using System.Collections.Generic;

namespace BlogService.Models;

public partial class ExaminatedRecord
{
    public int RecordId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public decimal? RespirationRate { get; set; }

    public decimal? Temperature { get; set; }

    public decimal? BloodPressure { get; set; }

    public decimal? SpO2 { get; set; }

    public string? Note { get; set; }

    public bool IsActive { get; set; }

    public virtual User Doctor { get; set; } = null!;

    public virtual ICollection<MedicineExaminatedRecord> MedicineExaminatedRecords { get; set; } = new List<MedicineExaminatedRecord>();

    public virtual User Patient { get; set; } = null!;
}

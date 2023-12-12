﻿using System;
using System.Collections.Generic;

namespace BlogService.Models;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public int UserId { get; set; }

    public int UnitId { get; set; }

    public int CodeId { get; set; }

    public string MedicineName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal PricePerUnit { get; set; }

    public bool IsActive { get; set; }

    public virtual MedicineCode Code { get; set; } = null!;

    public virtual ICollection<MedicineExaminatedRecord> MedicineExaminatedRecords { get; set; } = new List<MedicineExaminatedRecord>();

    public virtual Unit User { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace MedicineService.Models;

public partial class Medicine
{
    public string MedicineId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string UnitId { get; set; } = null!;

    public string CodeId { get; set; } = null!;

    public string MedicineName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal PricePerUnit { get; set; }

    public bool IsActive { get; set; }

    public virtual MedicineCode Code { get; set; } = null!;

    public virtual ICollection<MedicineExaminatedRecord> MedicineExaminatedRecords { get; set; } = new List<MedicineExaminatedRecord>();

    public virtual Unit Unit { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

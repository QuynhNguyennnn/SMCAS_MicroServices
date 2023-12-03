using System;
using System.Collections.Generic;

namespace ScheduleService.Models;

public partial class MedicineExaminatedRecord
{
    public string Meid { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string MedicineId { get; set; } = null!;

    public int Quantity { get; set; }

    public bool IsActive { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual ExaminatedRecord Record { get; set; } = null!;
}

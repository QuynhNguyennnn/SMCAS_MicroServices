using System;
using System.Collections.Generic;

namespace BlogService.Models;

public partial class MedicineExaminatedRecord
{
    public int Meid { get; set; }

    public int RecordId { get; set; }

    public int MedicineId { get; set; }

    public int Quantity { get; set; }

    public bool IsActive { get; set; }
}

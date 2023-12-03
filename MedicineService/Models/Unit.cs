using System;
using System.Collections.Generic;

namespace MedicineService.Models;

public partial class Unit
{
    public string UnitId { get; set; } = null!;

    public string UnitName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}

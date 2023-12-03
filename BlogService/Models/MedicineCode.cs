using System;
using System.Collections.Generic;

namespace BlogService.Models;

public partial class MedicineCode
{
    public string CodeId { get; set; } = null!;

    public string CodeName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}

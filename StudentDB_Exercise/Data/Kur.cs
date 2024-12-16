using System;
using System.Collections.Generic;

namespace StudentDB_Exercise.Data;

public partial class Kur
{
    public int Id { get; set; }

    public string? Namn { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

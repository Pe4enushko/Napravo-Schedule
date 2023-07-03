using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScheduleAPI.Models;

public partial class Grade
{
    public int IdGrade { get; set; }

    public int IdStudent { get; set; }

    public int IdClass { get; set; }

    public int Value { get; set; }

    public DateTime Date { get; set; }
    [JsonIgnore]
    public virtual Class IdClassNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Student IdStudentNavigation { get; set; } = null!;
}

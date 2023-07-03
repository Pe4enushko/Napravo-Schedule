using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScheduleAPI.Models;

public partial class Teacher
{
    public int IdTeacher { get; set; }

    public string Fullname { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}

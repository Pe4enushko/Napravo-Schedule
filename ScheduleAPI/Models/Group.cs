using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScheduleAPI.Models;

public partial class Group
{
    public int IdGroup { get; set; }

    public string Title { get; set; } = null!;

    public int? Course { get; set; }
    [JsonIgnore]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
    [JsonIgnore]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

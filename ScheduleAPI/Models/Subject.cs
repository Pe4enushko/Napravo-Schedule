using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScheduleAPI.Models;

public partial class Subject
{
    public int IdSubject { get; set; }

    public string Title { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}

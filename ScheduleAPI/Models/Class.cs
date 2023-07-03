using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScheduleAPI.Models;

public partial class Class
{
    public int IdClass { get; set; }

    public int IdSubject { get; set; }

    public int IdTeacher { get; set; }

    public int IdGroup { get; set; }

    /// <summary>
    /// Time from 0:00 in minutes
    /// </summary>
    public int Time { get; set; }

    /// <summary>
    /// index of day of week
    /// </summary>
    public int DayOfWeek { get; set; }
    [JsonIgnore]
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    [JsonIgnore]
    public virtual Group IdGroupNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Subject IdSubjectNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Teacher IdTeacherNavigation { get; set; } = null!;
}

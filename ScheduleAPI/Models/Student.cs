using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScheduleAPI.Models;

public partial class Student
{
    public int IdStudent { get; set; }

    public int IdGroup { get; set; }

    public string Fullname { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? CurrentToken { get; set; }

    public string? TokenGenDate { get; set; }
    [JsonIgnore]
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    [JsonIgnore]
    public virtual Group IdGroupNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

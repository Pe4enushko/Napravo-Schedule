using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace napravo_schedule.MVVM.Models;

public partial class Notification
{
    public int IdNotification { get; set; }

    public int? IdStudent { get; set; }

    public string? Header { get; set; }

    public string? Message { get; set; }

    public DateTime? Moment { get; set; }

    public bool? IsChecked { get; set; }
}

namespace ScheduleAdminApp.MVVM.Models;
public class Group
{
    public int IdGroup { get; set; }

    public string Title { get; set; } = null!;

    public int? Course { get; set; }
}

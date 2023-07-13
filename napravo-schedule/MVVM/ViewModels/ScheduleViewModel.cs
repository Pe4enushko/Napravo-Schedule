using CommunityToolkit.Mvvm.ComponentModel;
using napravo_schedule.API;
using napravo_schedule.MVVM.Models;

namespace napravo_schedule.MVVM.ViewModels
{
    public partial class ScheduleViewModel : BaseViewModel
    {
        [ObservableProperty]
        ClassReadable[] lessons;
        [ObservableProperty]
        string groupTitle = "Вп-31";
        [ObservableProperty]
        Group group;
        public ScheduleViewModel() 
        {
            SetLessons();
            SetGroupInfo();
        }

        async void SetGroupInfo()
        {
            Group = await ResponseFactory.GetGroup(GroupTitle);
        }

        async void SetLessons()
        {
            Lessons = await ResponseFactory.GetClassesReadable("Вп-31");
        }
    }
}

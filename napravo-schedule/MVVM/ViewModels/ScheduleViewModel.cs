using CommunityToolkit.Mvvm.ComponentModel;
using napravo_schedule.API;
using napravo_schedule.MVVM.Models;
using napravo_schedule.UserMessaging;
using System.Collections.ObjectModel;
using System.Data;

namespace napravo_schedule.MVVM.ViewModels
{
    public partial class ScheduleViewModel : BaseViewModel
    {
        delegate void FillInfo();

        public ObservableCollection<ClassReadable> Lessons { get; set; } = new();

        [ObservableProperty]
        string groupTitle = "Вп-31";

        [ObservableProperty]
        Group group;


        public ScheduleViewModel() 
        {
            var fillers = new FillInfo(SetGroupInfo);
            fillers += SetLessons;

            try
            {
                fillers.Invoke(); //TODO: resolve cast problem
            }
            catch (HttpRequestException ex)
            {
                AlertDisplayer.DisplayError(ex.Message);
            }
        }

        async void SetGroupInfo() => 
            Group = await ResponseFactory.GetGroup("Вп-31");
        async void SetLessons()
        {
            var arr = await ResponseFactory.GetClassesReadable("Вп-31");
            App.Current.Dispatcher.Dispatch(() =>
            {
                foreach (var item in arr)
                    Lessons.Add(item);
            });
        }
        
    }
}

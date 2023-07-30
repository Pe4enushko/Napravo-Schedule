using napravo_schedule.API;
using napravo_schedule.MVVM.Models;
using System.Collections.ObjectModel;

namespace napravo_schedule.MVVM.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        int testStudentId = 1;
        public ObservableCollection<Notification> Notifications { get; set; } = new();

        public NotificationViewModel()
        {
            FillData();    
        }

        async void FillData()
        {
            IsBusy = true;
            await ResponseFactory.GetAllNotifications(testStudentId)
                .ContinueWith((data) =>
            {
                if (data.IsCompleted && data.Result != null)
                {
                    App.Current.Dispatcher.Dispatch(() =>
                    {
                        foreach (var item in data.Result)
                        {
                            Notifications.Add(item);
                        }
                        IsBusy = false;
                    });
                }
            });
        }
    }
}

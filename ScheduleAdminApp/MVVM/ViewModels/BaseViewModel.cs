
using CommunityToolkit.Mvvm.ComponentModel;

namespace ScheduleAdminApp.MVVM.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy;
    }
}

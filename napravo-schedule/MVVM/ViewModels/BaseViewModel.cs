
using CommunityToolkit.Mvvm.ComponentModel;

namespace napravo_schedule.MVVM.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy;
    }
}

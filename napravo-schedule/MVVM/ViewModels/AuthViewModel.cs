using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace napravo_schedule.MVVM.ViewModels
{
    public partial class AuthViewModel : BaseViewModel
    {
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;

        [RelayCommand]
        void TryAuth()
        {

        }
    }
}

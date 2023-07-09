using CommunityToolkit.Mvvm.ComponentModel;
using napravo_schedule.API;
using napravo_schedule.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace napravo_schedule.MVVM.ViewModels
{
    public partial class ScheduleViewModel : BaseViewModel
    {
        [ObservableProperty]
        ClassReadable[] lessons;
        public ScheduleViewModel() 
        {
            SetLessons();
        }
        async void SetLessons()
        {
            Lessons = await ResponseFactory.GetClassReadable("Вп-31");
        }
    }
}

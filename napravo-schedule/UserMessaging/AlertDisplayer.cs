using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace napravo_schedule.UserMessaging
{
    public static class AlertDisplayer
    {
        public async static void DisplayError(string text)
        {
            await App.Current.MainPage.DisplayAlert("Error!", text, "OK");
        }
    }
}

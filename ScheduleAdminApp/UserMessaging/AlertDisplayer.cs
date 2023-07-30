namespace ScheduleAdminApp.UserMessaging
{
    public static class AlertDisplayer
    {
        public async static void DisplayError(string text)
        {
            await App.Current.MainPage.DisplayAlert("Error!", text, "OK");
        }
    }
}

using napravo_schedule.MVVM.Models;

namespace napravo_schedule.MVVM.Views.DataTemplates
{
    public class NotificationDataTemlpateSelector : DataTemplateSelector
    {
        public DataTemplate RegularNotification { get; set; }
        public DataTemplate UnreadNotification { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var notification = ((Models.Notification)item);
            if (notification.IsChecked == null)
                return UnreadNotification;
            else
                return (bool)notification.IsChecked ? RegularNotification : UnreadNotification;
        }
    }
}

using napravo_schedule.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace napravo_schedule.MVVM.Views.DataTemplates.Calendar
{
    public class CalendarDataTemplateSelector : DataTemplateSelector
    {
        DateTime now = DateTime.Now;

        public DataTemplate DayOff { get; set; }
        public DataTemplate Weekday { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var cell = item as CalendarCell;
            int dow = (int)cell.Date.DayOfWeek;
            return dow == 0 || dow == 6 ? DayOff : Weekday;
        }
    }
}

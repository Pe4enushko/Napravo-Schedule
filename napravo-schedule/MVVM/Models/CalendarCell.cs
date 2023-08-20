using napravo_schedule.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace napravo_schedule.MVVM.Models
{
    public class CalendarCell
    {
        public CalendarCell(DateTime date)
        {
            this.Date = date;
        }
        public DateTime Date{ get; set; }
        public string DateString { get => Date.ToString("M"); }
        public string DayOfWeekString { get => Date.DayOfWeek.ToString(); }
    }
}

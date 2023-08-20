using napravo_schedule.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace napravo_schedule.Testing
{
    public static class MockDataProvider
    {
        public static Notification[] GetNotifications()
            => new[]
            {
                new Notification()
                {
                    Header = "Заголовок",
                    Message = "Сообщение о чём то",
                    Moment = DateTime.Now,
                    IdNotification = 1,
                    IdStudent = 1,
                    IsChecked = true
                },
                new Notification()
                {
                    Header = "Заголовок",
                    Message = "Сообщение о чём то невиданном",
                    Moment = DateTime.Now,
                    IdNotification = 1,
                    IdStudent = 1,
                    IsChecked = false
                }
            };
        public static ClassReadable[] GetClasses()
            => new[]
            {
                new ClassReadable()
                {
                    DayOfWeek = (int)DayOfWeek.Monday,
                    GroupTitle = "Вп-31",
                    IdClass = 1,
                    SubjectTitle = "Математика",
                    TeacherName = "Васильев В.В.",
                    Time = 540
                },
                new ClassReadable()
                {
                    DayOfWeek = (int)DayOfWeek.Tuesday,
                    GroupTitle = "Вп-31",
                    IdClass = 2,
                    SubjectTitle = "Математика",
                    TeacherName = "Васильев В.В.",
                    Time = 540
                },
                new ClassReadable()
                {
                    DayOfWeek = (int)DayOfWeek.Wednesday,
                    GroupTitle = "Вп-31",
                    IdClass = 3,
                    SubjectTitle = "Python",
                    TeacherName = "Васильев В.В.",
                    Time = 540
                },
                new ClassReadable()
                {
                    DayOfWeek = (int)DayOfWeek.Thursday,
                    GroupTitle = "Вп-31",
                    IdClass = 4,
                    SubjectTitle = "Unity",
                    TeacherName = "Васильев В.В.",
                    Time = 540
                },
                new ClassReadable()
                {
                    DayOfWeek = (int)DayOfWeek.Friday,
                    GroupTitle = "Вп-31",
                    IdClass = 5,
                    SubjectTitle = "Физика",
                    TeacherName = "Васильев В.В.",
                    Time = 540
                }
            };
        public static string[] GetGroupTitles()
            => new[] { "Вп-31" };
    }
}

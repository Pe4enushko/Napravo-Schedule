using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using napravo_schedule.API;
using napravo_schedule.MVVM.Models;
using napravo_schedule.UserMessaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Text.Json;

namespace napravo_schedule.MVVM.ViewModels
{
    public partial class ScheduleViewModel : BaseViewModel
    {
        static object locker = new();
        /// <summary>
        /// 2 августа 2023 - воскресенье. Переменную не трогать, т.к. с её
        /// помощью коротко считается день недели и переводится в строку 
        /// (не через switch же это делать)
        /// </summary>
        DateTime countPoint = new DateTime(2023, 7, 30);

        [NotifyPropertyChangedFor(nameof(DayString))]
        [ObservableProperty]
        DateTime chosenDate;
        ClassReadable[] AllLessons { get; set; }
        public ObservableCollection<ClassReadable> Lessons { get; set; } = new();

        string _groupTitle; 
        public string GroupTitle 
        {
            get => string.IsNullOrEmpty(_groupTitle) ? "Вп-31" : _groupTitle;
            set 
            {
                _groupTitle = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        Group group;

        public string DayString => ChosenDate.DayOfWeek.ToString();

        [RelayCommand]
        void IncrementDay()
        {
            ChosenDate = ChosenDate.AddDays(1);

            ChangeLessonsByDate(ChosenDate);
        }
        [RelayCommand]
        void DecrementDay()
        {
            ChosenDate = ChosenDate.AddDays(-1);

            ChangeLessonsByDate(ChosenDate);
        }
        public ScheduleViewModel(DateTime date) 
        {
            ChosenDate = date;

            try
            {
                SetGroupInfo();
                SetLessons();
            }
            catch (HttpRequestException ex)
            {
                AlertDisplayer.DisplayError(ex.Message);
            }
        }
        /// <summary>
        /// TODO: bind to auth
        /// </summary>
        void SetGroupInfo() =>
            Group = new Group() { Course = 3, IdGroup = 1, Title = "Вп-31" };
        async void SetLessons()
        {
            ClassReadable[] arr = await ResponseFactory.GetClassesReadable(GroupTitle);

            if (arr != null)
            {
                AllLessons = arr;
                FillChosenLessons(l => l.DayOfWeek == (int)ChosenDate.DayOfWeek);
            }  
        }
        
        void FillChosenLessons(Func<ClassReadable, bool> predicate)
        {
            if (AllLessons != null && Lessons != null)
            {
                Lessons.Clear();
                var items = AllLessons.Where(predicate);

                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (var item in items)
                        Lessons.Add(item);
                });
            }
        }

        public void ChangeLessonsByDate(DateTime date)
        {
            Lessons.Clear();
            FillChosenLessons(l => l.DayOfWeek == (int)date.DayOfWeek
                                    && l.GroupTitle == GroupTitle);
        }
    }
}

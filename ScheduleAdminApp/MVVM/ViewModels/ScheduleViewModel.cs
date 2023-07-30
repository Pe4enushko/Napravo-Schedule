using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScheduleAdminApp.API;
using ScheduleAdminApp.MVVM.Models;
using ScheduleAdminApp.UserMessaging;
using System.Collections.ObjectModel;
using System.Data;

namespace ScheduleAdminApp.MVVM.ViewModels
{
    public partial class ScheduleViewModel : BaseViewModel
    {
        delegate void FillInfo();
        FillInfo _fillers;

        /// <summary>
        /// 2 августа 2023 - воскресенье. Переменную не трогать, т.к. с её
        /// помощью коротко считается день недели и переводится в строку 
        /// (не через switch же это делать)
        /// </summary>
        DateTime countPoint = new DateTime(2023, 8, 2);
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
                _fillers.Invoke();
            }
        }


        public ObservableCollection<string> AllGroupTitles { get; set; } = new ObservableCollection<string>();

        [ObservableProperty]
        Group group;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DayString))]
        int currentDay = (int)DateTime.Now.DayOfWeek;
        public string DayString => countPoint.AddDays(CurrentDay).DayOfWeek.ToString();


        [RelayCommand]
        void IncrementDay()
        {
            if (CurrentDay == 6)
                CurrentDay = 0;
            else
                CurrentDay++;

            ChangeLessonsByDay(CurrentDay);
        }
        [RelayCommand]
        void DecrementDay()
        {
            if (CurrentDay == 0)
                CurrentDay = 6;
            else
                CurrentDay--;

            ChangeLessonsByDay(CurrentDay);
        }
        public ScheduleViewModel() 
        {
            GetGroupTitles();
            _fillers = new FillInfo(SetGroupInfo);
            _fillers += SetLessons;

            try
            {
                _fillers.Invoke(); //TODO: resolve cast problem
            }
            catch (HttpRequestException ex)
            {
                AlertDisplayer.DisplayError(ex.Message);
            }
        }
        /// <summary>
        /// Is synchronous because result will be used in other methods
        /// </summary>
        void GetGroupTitles()
        {
            var data = ResponseFactory.GetGroupTitles().Result;
            var count = data.Length;

            for (int i = 0; i < count; i++)
            {
                AllGroupTitles.Add(data[i]);
            }

            if (count > 0)
                GroupTitle = AllGroupTitles[0];
        }
        async void SetGroupInfo() => 
            Group = await ResponseFactory.GetGroup(GroupTitle);
        async void SetLessons()
        {
            var arr = await ResponseFactory.GetClassesReadable(GroupTitle);
            if (arr != null)
            {
                AllLessons = arr;
                FillChosenLessons(l => l.DayOfWeek == CurrentDay);
            }
        }
        
        void FillChosenLessons(Func<ClassReadable, bool> predicate)
        {
            if (AllLessons != null && Lessons != null)
            {
                Lessons.Clear();
                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (var item in AllLessons
                                         .Where(predicate))
                        Lessons.Add(item);
                });
            }
        }

        void ChangeLessonsByDay(int dayOfWeek)
        {
            Lessons.Clear();
            FillChosenLessons(l => l.DayOfWeek == dayOfWeek
                                    && l.GroupTitle == GroupTitle);
        }
    }
}

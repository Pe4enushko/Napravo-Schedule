using napravo_schedule.MVVM.Models;
using napravo_schedule.MVVM.ViewModels;

namespace napravo_schedule.MVVM.Views;

public partial class CalendarPage : ContentPage
{
	List<CalendarCell> cells = new();
	public CalendarPage()
	{
		InitializeComponent();

		FillCells();
		Calendar.ItemsSource = cells;
	}
	
	void FillCells()
	{
		int i = 1;

        for (int j = i; j < 31; j++)
		{
			cells.Add(new CalendarCell(new DateTime(DateTime.Now.Year, DateTime.Now.Month, j)));
		}
	}

    private void Calendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		var date = ((CalendarCell)e.CurrentSelection[0]).Date;

        Navigation.PushAsync(new SchedulePage(date));
    }
}
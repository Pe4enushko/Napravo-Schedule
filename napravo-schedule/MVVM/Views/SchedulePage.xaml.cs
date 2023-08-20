using napravo_schedule.MVVM.Models;
using napravo_schedule.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace napravo_schedule.MVVM.Views;

public partial class SchedulePage : ContentPage
{
	public SchedulePage(DateTime chosenDate)
	{
		InitializeComponent();

		BindingContext = new ScheduleViewModel(chosenDate);
	}
}
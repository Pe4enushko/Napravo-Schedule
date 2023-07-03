using napravo_schedule.MVVM.Views;

namespace napravo_schedule;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}
}

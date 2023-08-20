using napravo_schedule.MVVM.Views;
using System.Globalization;

namespace napravo_schedule;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
        CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");
        MainPage = new AppShell();
	}
}

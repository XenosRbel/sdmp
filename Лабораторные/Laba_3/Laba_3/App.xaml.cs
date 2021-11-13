using Laba_3.Services;
using Xamarin.Forms;

namespace Laba_3
{
	public partial class App : Application
	{

		public new static App Current => (App)Application.Current;
		public IContainer Container { get; private set; }

		private readonly IConfig _config;

		public App()
		{
			InitializeComponent();

			_config = ApplicationConfig.Configuration;
			Container = new Container(_config);

			MainPage = new AppShell();
			Shell.Current.GoToAsync("//MainPage");
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}

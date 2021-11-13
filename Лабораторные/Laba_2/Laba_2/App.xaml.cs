using Laba_2.Services;
using Xamarin.Forms;

namespace Laba_2
{
	public partial class App : Application
	{
		public new static App Current => (App)Application.Current;
		public IContainer Container { get; private set; }

		private readonly IConfig _config;

		public App()
		{
			_config = ApplicationConfig.Configuration;
			Container = new Container(_config);

			InitializeComponent();

			MainPage = new AppShell();
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

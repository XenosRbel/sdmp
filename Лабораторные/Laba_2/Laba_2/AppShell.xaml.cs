using Laba_2.Views.Home;
using Laba_2.Views.Profile;
using Laba_2.Views.About;
using Xamarin.Forms;

namespace Laba_2
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
			Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
			Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
		}

	}
}

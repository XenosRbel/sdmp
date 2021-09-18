using Laba_2.Views.Home;
using Laba_2.Views.Profile;
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
		}

	}
}

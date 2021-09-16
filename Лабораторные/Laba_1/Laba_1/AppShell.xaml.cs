using Laba_1.Views.CatClicker;
using System;
using Xamarin.Forms;

namespace Laba_1
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(CatClickerPage), typeof(CatClickerPage));
		}

		private async void OnMenuItemClicked(object sender, EventArgs e)
		{
			await Current.GoToAsync("//LoginPage");
		}
	}
}

using Laba_2.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_2.Views.Home
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
			this.BindingContext = App.Current.Container.ServiceProvider.GetService<HomeViewModel>();
		}
	}
}
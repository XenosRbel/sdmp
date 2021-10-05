
using Laba_2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_2.Views.About
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
			InitializeComponent();
			this.BindingContext = App.Current.Container.ServiceProvider.GetService<AboutViewModel>();
		}
	}
}
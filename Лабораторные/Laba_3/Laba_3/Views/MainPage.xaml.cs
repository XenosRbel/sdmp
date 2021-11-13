using Laba_3.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_3.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			this.BindingContext = App.Current.Container.ServiceProvider.GetService<MainPageViewModel>();
		}
	}
}
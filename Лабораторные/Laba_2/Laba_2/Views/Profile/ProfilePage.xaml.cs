using Laba_2.Services.Helpers;
using Laba_2.ViewModels;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_2.Views.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		private readonly ProfileViewModel _viewModel;
		public ProfilePage()
		{
			InitializeComponent();
			_viewModel = App.Current.Container.ServiceProvider.GetService<ProfileViewModel>();
			this.BindingContext = _viewModel;

			_viewModel.Profile.Property.AvatarUrl.PropertyChanged += AvatarUrl_PropertyChanged;

			var stream = File.OpenRead(_viewModel.Profile.Property.AvatarUrl.Property);
			AvatarImage.Source = ImageSource.FromStream(() => stream);
		}

		private void AvatarUrl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var profile = (ObservableObject<string>)sender;
			var stream = File.OpenRead(profile.Property);
			AvatarImage.Source = ImageSource.FromStream(() => stream);
		}
	}
}
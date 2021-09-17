using Laba_1.Helpers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Laba_1.ViewModels
{
	class CatClickerViewModel : BaseViewModel
	{
		private readonly Image _catImage;
		public CatClickerViewModel(Image catImage)
		{
			Title = "Задание на 1-4";
			FeedClickCommand = new Command(() => OnFeedClickCommand());
			ShareResultCommand = new Command(async () => await OnShareResultCommand());
			Satiety = new ObservableObject<int> { Property = 0 };

			_catImage = catImage ?? throw new ArgumentNullException();
		}

		public ObservableObject<int> Satiety { get; }
		public ICommand FeedClickCommand { get; }
		public ICommand ShareResultCommand { get; }

		private async void OnFeedClickCommand()
		{
			if (IsBusy) return;

			IsBusy = true;

			try
			{
				this.Satiety.Property++;

				await _catImage.RotateTo(360, 200, Easing.CubicInOut);
				await _catImage.ScaleTo(2, 100, Easing.CubicInOut);
				await _catImage.ScaleTo(1, 100, Easing.CubicInOut);
				_catImage.Rotation = 0;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private async Task OnShareResultCommand()
		{
			await Share.RequestAsync(new ShareTextRequest
			{
				Text = $"Мой результат: {this.Satiety.Property}!",
				Title = "Поделиться результатом"
			});
		}
	}
}

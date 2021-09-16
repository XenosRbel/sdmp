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
		public CatClickerViewModel()
		{
			Title = "Задание на 1-4";
			FeedClickCommand = new Command(() => OnFeedClickCommand());
			ShareResultCommand = new Command(async() => await OnShareResultCommand());
			Satiety = new ObservableObject<int> { Property = 0 };
		}

		public ObservableObject<int> Satiety { get; }
		public ICommand FeedClickCommand { get; }
		public ICommand ShareResultCommand { get; }

		private void OnFeedClickCommand()
		{
			if (IsBusy) return;

			IsBusy = true;

			try
			{
				this.Satiety.Property++;
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

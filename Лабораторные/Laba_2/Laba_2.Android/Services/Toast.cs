using Android.Widget;
using Laba_2.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Toast))]
namespace Laba_2.Droid.Services
{
	internal class Toast : IToast
	{
		public void Show(string message)
		{
			Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
		}
	}
}
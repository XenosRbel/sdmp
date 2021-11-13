using Laba_3.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_3.Views
{
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
			InitializeComponent();
			this.BindingContext = App.Current.Container.ServiceProvider.GetService<AboutViewModel>();
		}
	}
}
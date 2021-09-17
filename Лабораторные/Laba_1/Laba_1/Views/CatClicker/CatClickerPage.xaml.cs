using Laba_1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_1.Views.CatClicker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CatClickerPage : ContentPage
	{
		private readonly CatClickerViewModel _viewModel; 
		public CatClickerPage()
		{
			InitializeComponent();

			_viewModel = new CatClickerViewModel(CatImage);
			this.BindingContext = _viewModel;
		}
	}
}
using System;
using Laba_4.DataCore;
using Laba_4.Models;
using Laba_4.Presentation.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_4.Presentation.Views.EditContact
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditContactPage : ContentPage
	{
		public EditContactPage(EditContactViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
		}
	}
}
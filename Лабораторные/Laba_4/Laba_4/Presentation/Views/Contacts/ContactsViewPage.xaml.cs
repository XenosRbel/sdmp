using System;
using Laba_4.DataCore;
using Laba_4.Models;
using Laba_4.Presentation.ViewModels;
using Laba_4.Presentation.Views.ContactDetails;
using Laba_4.Presentation.Views.EditContact;
using Laba_4.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_4.Presentation.Views.Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsViewPage : ContentPage
    {
        private readonly ContactsViewModel _viewModel;

		public ContactsViewPage(ContactsViewModel viewModel)
		{
			InitializeComponent();

			_viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
			BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadCommand.Execute(null);
        }
    }
}
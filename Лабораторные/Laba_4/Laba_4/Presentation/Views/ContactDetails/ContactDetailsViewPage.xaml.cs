using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Laba_4.DataCore;
using Laba_4.Presentation.ViewModels;
using Laba_4.Presentation.Views.EditContact;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laba_4.Presentation.Views.ContactDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailsViewPage : ContentPage
    {
	    public ContactDetailsViewPage(ContactDetailsViewModel viewModel)
        {
	        InitializeComponent();

	        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
		}
    }
}
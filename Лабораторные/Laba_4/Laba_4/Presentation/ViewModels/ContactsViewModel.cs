using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using LiteDB;
using Laba_4.DataCore;
using Laba_4.Helpers;
using Laba_4.Models;
using Laba_4.Services;
using Xamarin.Forms;

namespace Laba_4.Presentation.ViewModels
{
	public class ContactsViewModel : BaseViewModel
    {
	    private readonly IRepository _repository;
	    private readonly INavigationService _navigationService;

	    public ContactsViewModel(IRepository repository, INavigationService navigationService)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
			_navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService)); ;

            LoadCommand = new Command(async () => await OnExecuteLoadCommand());
			TappedCommand = new Command(OnItemTapped);
			AddContactCommand = new Command(OnAddContact);

            ContactItems = new ObservableRangeCollection<Contact>();
        }

	    public ObservableRangeCollection<Contact> ContactItems { get; set; }
	    public ICommand LoadCommand { get; private set; }
	    public ICommand TappedCommand { get; private set; }
	    public ICommand AddContactCommand { get; private set; }

		private void OnItemTapped(object obj)
		{
			var contact = (Contact) obj;
			Device.BeginInvokeOnMainThread(async () =>  await _navigationService.PushContactDetailsPageAsync(contact));
		}

		private void OnAddContact()
		{
			Device.BeginInvokeOnMainThread(async () => await _navigationService.PushEditContactAsync());
		}

		private async Task OnExecuteLoadCommand()
        {
            //if (IsBusy) return;

            IsBusy = true;

            try
            {
                ContactItems.Clear();

                var contacts = await _repository.GetAllAsync<Contact>();

                ContactItems.AddRange(contacts);
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
    }
}

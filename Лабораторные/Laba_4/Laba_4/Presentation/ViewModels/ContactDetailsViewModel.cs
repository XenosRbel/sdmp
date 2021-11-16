using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Laba_4.DataCore;
using Laba_4.Helpers;
using Laba_4.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Laba_4.Presentation.ViewModels
{
	public class ContactDetailsViewModel
	{
		private readonly INavigationService _navigationService;
		private readonly IRepository _repository;

		public ContactDetailsViewModel(INavigationService navigationService, IRepository repository, Models.Contact contact)
		{
			_navigationService = navigationService ?? throw  new ArgumentNullException(nameof(navigationService));
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));

			Contact = new ObservableObject<Models.Contact> { Property = contact };

			CallCommand = new Command(OnCall);
			EditContactCommand = new Command(OnEditContact);
			RemoveContactCommand = new Command(OnRemoved);
			BackCommand = new Command(OnBack);
		}

		private async void OnRemoved()
		{
			var contacts = await _repository.GetAllAsync<Models.Contact>();
			var contact = contacts.First(item => item.Equals(Contact.Property));
			contacts.Remove(contact);

			await _repository.RemoveAllAsync<Models.Contact>();

			await _repository.AddOrUpdateAllAsync(contacts);

			await _navigationService.PopAsync();
		}

		public ObservableObject<Models.Contact> Contact { get; set; }
		public ICommand CallCommand { get; set; }
		public ICommand EditContactCommand { get; set; }
		public ICommand RemoveContactCommand { get; set; }
		public ICommand BackCommand { get; set; }

		private void OnEditContact()
		{
			var contact = Contact.Property;
			_navigationService.PushEditContactAsync(contact);
		}

		private void OnBack()
		{
			_navigationService.PopAsync();
		}

		private void OnCall()
		{
			var phoneNumber = Contact.Property.PhoneNumber;
			Device.BeginInvokeOnMainThread(() => Call(phoneNumber));
		}

		private void Call(string phoneNumber)
		{
			try
			{
				if (Device.Idiom == TargetIdiom.Desktop)
				{
					Device.OpenUri(new Uri("tel:" + phoneNumber));
				}
				else
				{
					PhoneDialer.Open(phoneNumber);
				}

			}
			catch (FeatureNotSupportedException ex)
			{
				DependencyService.Get<IToast>().Show("Phone Dialer is not supported on this device!");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}

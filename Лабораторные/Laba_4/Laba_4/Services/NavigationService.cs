using System;
using System.Threading.Tasks;
using Laba_4.Models;
using Laba_4.Presentation.ViewModels;
using Laba_4.Presentation.Views.ContactDetails;
using Laba_4.Presentation.Views.Contacts;
using Laba_4.Presentation.Views.EditContact;
using Xamarin.Forms;

namespace Laba_4.Services
{
	public class NavigationService : INavigationService
	{
		private readonly Application _application;
		private readonly IContainer _container;
		private INavigation Navigation => _application.MainPage.Navigation;

		public NavigationService(Application application, IContainer container)
		{
			_application = application ?? throw new ArgumentNullException(nameof(application));
			_container = container ?? throw new ArgumentNullException(nameof(container));

			var page = new MainPage();
			CreateNewNavigation(page);
		}

		public Task PopAsync()
		{
			return Navigation.PopModalAsync();
		}

		public Task PushContactDetailsPageAsync(Contact contact)
		{
			var viewModel = new ContactDetailsViewModel(_container.GetNavigationService(), 
				_container.GetRepository(),
				contact);
			var page = new ContactDetailsViewPage(viewModel);

			return Navigation.PushModalAsync(page);
		}

		public Task PushContactsPageAsync()
		{
			var viewModel = new ContactsViewModel(_container.GetRepository(), 
				_container.GetNavigationService());
			var page = new ContactsViewPage(viewModel);

			return Navigation.PushModalAsync(page);
		}

		public Task PushEditContactAsync()
		{
			var viewModel = new EditContactViewModel(_container.GetRepository(), 
				_container.GetNavigationService());
			var page = new EditContactPage(viewModel);

			return Navigation.PushModalAsync(page);
		}

		public Task PushEditContactAsync(Contact contact)
		{
			var viewModel = new EditContactViewModel(_container.GetRepository(), 
				_container.GetNavigationService(), 
				contact);
			var page = new EditContactPage(viewModel);

			return Navigation.PushModalAsync(page);
		}

		private void CreateNewNavigation(Page page)
		{
			var navigationPage = new NavigationPage(page);

			_application.MainPage = navigationPage.RootPage;
		}
	}
}
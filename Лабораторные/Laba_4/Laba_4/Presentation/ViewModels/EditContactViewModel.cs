using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AngleSharp;
using AngleSharp.XPath;
using CsQuery;
using Laba_4.DataCore;
using Laba_4.Models;
using Laba_4.Services;
using Xamarin.Forms;
using static System.String;

namespace Laba_4.Presentation.ViewModels
{
	public class EditContactViewModel : BaseViewModel
	{
		private readonly IRepository _repository;
		private readonly INavigationService _navigationService;
		private string _name;
		private string _phoneNumber;
		private string _phoneType;
		private string _imageUrl;
		private string _region;
		private string[] _phoneTypes;
		private bool _isValid;
		private int _contactId;
		private string _vkUrl;
		
		public EditContactViewModel(IRepository repository, INavigationService navigationService, Contact contact = null)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

			SubmitCommand = new Command(OnSubmit);
			BackCommand = new Command(OnBack);
			ImportCommand = new Command(async() => await OnImportAsync());

			Name = contact?.Name;
			PhoneNumber = contact?.PhoneNumber;
			PhoneType = contact?.PhoneType.ToString();
			PhoneTypes = Enum.GetNames(typeof(ContactType));
			Region = contact?.Region;
			ImageUrl = contact?.ImageUrl;
			_contactId = contact?.Id ?? -1;

			IsValid = true;
		}

		private async Task OnImportAsync()
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(VkUrl))
				{
					var config = Configuration.Default.WithDefaultLoader().WithXPath();
					var address = $"https://vkfaces.com/vk/user/{VkUrl}";
					var context = BrowsingContext.New(config);
					var document = await context.OpenAsync(address);

					var name = document.Body.QuerySelectorAll("div[class=\"list-info\"]>div[class=\"list-info__title\"]")?.First().TextContent;
					var region = document.Body.QuerySelectorAll("div[class=\"list-info\"]>div[class=\"list-info__subtitle\"]")?.First().TextContent.Replace("\n", "");
					var img = document.Body.QuerySelectorAll("div[class=\"avatar _elastic_down js-popup-gallery-trigger _pointer\"]>img[class=\"avatar__img\"]")?.First().GetAttribute("src");

					Name = name;
					Region = region;
					ImageUrl = img;
				}
			}
			catch (Exception)
			{

	
			}
		}

		public ICommand SubmitCommand { get; set; }
		public ICommand BackCommand { get; set; }
		public ICommand ImportCommand { get; set; }
		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public string VkUrl
		{
			get => _vkUrl;
			set => SetProperty(ref _vkUrl, value);
		}

		public string Region
		{
			get => _region;
			set => SetProperty(ref _region, value);
		}

		public string ImageUrl
		{
			get => _imageUrl;
			set => SetProperty(ref _imageUrl, value);
		}

		public string PhoneNumber
		{
			get => _phoneNumber;
			set => SetProperty(ref _phoneNumber, value);
		}

		public string PhoneType
		{
			get => _phoneType;
			set => SetProperty(ref _phoneType, value);
		}

		public string[] PhoneTypes
		{
			get => _phoneTypes;
			set => SetProperty(ref _phoneTypes, value);
		}

		public bool IsValid
		{
			get => _isValid;
			set => SetProperty(ref _isValid, value);
		}

		private async void OnSubmit()
		{
			Enum.TryParse(PhoneType, out ContactType contactType);

			var contact = new Contact
			{
				PhoneType = contactType,
				Name = Name,
				PhoneNumber = PhoneNumber,
				PhotoPath = string.Empty,
				ImageUrl = ImageUrl,
				Region = Region,
				Id = _contactId
			};

			var contacts = await _repository.GetAllAsync<Contact>();
			if (contacts.Contains(contact, new ContactIdComparer()))
			{
				var entity = contacts.First(item => item.Id == contact.Id);
				entity.Id = contact.Id;
				entity.Name = contact.Name;
				entity.PhoneNumber = contact.PhoneNumber;
				entity.PhoneType = contact.PhoneType;
				entity.PhotoPath = contact.PhotoPath;
				entity.ImageUrl = contact.ImageUrl;
				entity.Region = contact.Region;
			}
			else
			{
				contacts.Add(contact);
			}

			await _repository.AddOrUpdateAllAsync(contacts);
		}

		private async void OnBack()
		{
			await _navigationService.PopAsync();
		}
	}
}

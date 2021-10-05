using Laba_2.Models;
using Laba_2.Models.OnlinerProduct;
using Laba_2.Services;
using Laba_2.Services.Helpers;
using Laba_2.Services.Network;
using Laba_2.Services.Repositories;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace Laba_2.ViewModels
{
	internal class HomeViewModel : BaseViewModel
	{
		public ObservableRangeCollection<IProductItem> Items { get; set; }
		public ObservableRangeCollection<IProductItem> AllItems { get; set; }
		public ObservableRangeCollection<string> FilterOptions { get; }


		public ICommand LoadCommand { get; set; }

		private readonly IRepository _repository;
		private readonly PhoneService _phonesService;
		private readonly TelescopeService _telescopeService;
		private readonly CancellationTokenSource _cancellationToken;

		string selectedFilter = "Все";
		public string SelectedFilter
		{
			get => selectedFilter;
			set
			{
				if (SetProperty(ref selectedFilter, value))
					FilterItems();
			}
		}

		public ObservableObject<string> SearchFilter { get; set; }

		public HomeViewModel(PhoneService phonesService, TelescopeService telescopeService, IRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_phonesService = phonesService ?? throw new ArgumentNullException(nameof(phonesService));
			_telescopeService = telescopeService ?? throw new ArgumentNullException(nameof(telescopeService));

			LoadCommand = new Command(() => OnExecuteLoadCommand());
			SearchFilter = new ObservableObject<string> { Property = string.Empty };

			Items = new ObservableRangeCollection<IProductItem>();
			AllItems = new ObservableRangeCollection<IProductItem>();
			_cancellationToken = new CancellationTokenSource();

			FilterOptions = new ObservableRangeCollection<string>
			{
				"Все",
				"Телескоп",
				"Смартфон"
			};

			_repository.InsertCompleted += _repository_InsertCompleted;
			SearchFilter.PropertyChanged += SearchFilter_PropertyChanged;

			LoadPhonesItems();
			LoadTelescopesItems();

			_repository.Add(_repository.Load<ProductItem>().ToList());
		}

		private void SearchFilter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			FilterItems(SearchFilter.Property);
		}

		private void FilterItems()
		{
			Items.ReplaceRange(AllItems.Where(a => a.NamePrefix == SelectedFilter || SelectedFilter == "Все"));
		}

		private void FilterItems(string searchPattern)
		{
			Items.ReplaceRange(AllItems.Where(a =>a.NamePrefix == SelectedFilter || SelectedFilter == "Все" && (
			a.ExtendedName.ToLower().Contains(searchPattern.ToLower()) || 
			a.Name.ToLower().Contains(searchPattern.ToLower())
			)));
		}

		private void OnExecuteLoadCommand()
		{
			if (IsBusy) return;

			IsBusy = true;

			try
			{
				var items = _repository.GetAll<IProductItem>();
				AllItems.ReplaceRange(items);
				FilterItems();
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

		private async void LoadPhonesItems()
		{
			var progressHandler = CreateProgressHandler();
			try
			{
				var phones = await _phonesService.FetchAsync(progressHandler, _cancellationToken.Token);

				BuildAndSaveProductItems(phones.Products);
			}
			catch (HttpRequestException ex)
			{
				Debug.WriteLine(ex);

				_cancellationToken.Cancel();

				DependencyService.Get<IToast>().Show("Ошибка подключения!");
			}
		}

		private async void LoadTelescopesItems()
		{
			var progressHandler = CreateProgressHandler();
			try
			{
				var telescopes = await _telescopeService.FetchAsync(progressHandler, _cancellationToken.Token);

				BuildAndSaveProductItems(telescopes.Products);
			}
			catch (HttpRequestException ex)
			{
				Debug.WriteLine(ex);

				_cancellationToken.Cancel();

				DependencyService.Get<IToast>().Show("Ошибка подключения!");
			}
		}

		private void _repository_InsertCompleted(object sender)
		{
			OnExecuteLoadCommand();
		}

		private void BuildAndSaveProductItems(List<Product> products)
		{
			List<IProductItem> productItems = new List<IProductItem>();

			try
			{
				products.ForEach(item =>
				{
					productItems.Add(new ProductItem
					{
						ExtendedName = item.ExtendedName,
						Id = item.Id,
						ImageUrl = new UriBuilder("https", item.Images.Header.Remove(0, 2)).Uri.OriginalString.TrimEnd('/'),
						Name = item.Name,
						Price = Convert.ToDouble(item.Prices.PriceMin.Amount),
						NamePrefix = item.NamePrefix.ToString(),
						Currency = item.Prices.PriceMin.Currency.ToString()
					});
				});
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				_repository.Add(productItems);
			}
		}

		protected ProgressHandler CreateProgressHandler()
		{
			var progressHandler = new ProgressHandler((size, downloaded) =>
			{
				if (size.HasValue)
				{
					var percent = Convert.ToDouble(downloaded) / size.Value * 100;
				}
			});
			return progressHandler;
		}
	}
}

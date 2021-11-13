using Laba_3.Models;
using Laba_3.Services;
using Laba_3.Services.Helpers;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Laba_3.ViewModels
{
	internal class MainPageViewModel : BaseViewModel
	{
		private readonly IUserService _userService;
		private readonly IList<IMood> _moodSource;

		public IObservableObject<IProfile> Profile { get; set; }

		public ObservableCollection<IMood> Moods { get; private set; }
		public IMood PreviousMood { get; set; }
		public IMood CurrentMood { get; set; }
		public IMood CurrentItem { get; set; }

		public ICommand ItemChangedCommand => new Command<Mood>(ItemChanged);

		public IObservableObject<string> Recomendation { get; private set; }

		public MainPageViewModel(IUserService userService)
		{
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));

			Profile = new ObservableObject<IProfile> { Property = _userService.Load() };

			_moodSource = new List<IMood>();
			Recomendation = new ObservableObject<string> { Property = string.Empty };

			CreateMoodsCollection();

			CurrentItem = Moods.Skip(3).FirstOrDefault();
			OnPropertyChanged("CurrentItem");
		}

		void CreateMoodsCollection()
		{
			_moodSource.Add(new Mood
			{
				Title = "Спокойным",
				ImageUrl = "Assets/Calm.png",
				MoodType = MoodType.Calm
			});

			_moodSource.Add(new Mood
			{
				Title = "Расслабленным",
				ImageUrl = "Assets/Relax.png",
				MoodType = MoodType.Relax
			});

			_moodSource.Add(new Mood
			{
				Title = "Сосредоточенным",
				ImageUrl = "Assets/Focus.png",
				MoodType = MoodType.Focus
			});

			Moods = new ObservableCollection<IMood>(_moodSource);
		}

		void ItemChanged(Mood item)
		{	
			PreviousMood = CurrentItem;
			CurrentItem = item;
			OnPropertyChanged("PreviousMood");
			OnPropertyChanged("CurrentMood");

			Recomendation.Property = Faker.Lorem.Paragraph();
		}
	}
}

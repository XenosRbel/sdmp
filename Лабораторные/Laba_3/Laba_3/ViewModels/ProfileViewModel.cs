using Laba_3.Models;
using Laba_3.Services;
using Laba_3.Services.Helpers;
using MvvmHelpers.Commands;
using System;
using System.Windows.Input;

namespace Laba_3.ViewModels
{
	internal class ProfileViewModel : BaseViewModel
	{
		private readonly IUserService _userService;

		public IObservableObject<IProfile> Profile { get; set; }
		public ICommand SaveProfileCommand { get; set; }

		public ProfileViewModel(IUserService userService)
		{
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));

			SaveProfileCommand = new Command(() => OnSaveProfileCommand());

			Profile = new ObservableObject<IProfile> { Property = _userService.Load() };
		}

		protected void OnSaveProfileCommand()
		{
			_userService.Save(Profile.Property);
		}
	}
}

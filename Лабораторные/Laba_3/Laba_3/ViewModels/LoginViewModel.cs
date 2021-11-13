using Firebase.Auth;
using Laba_3.Models;
using Laba_3.Services;
using Laba_3.Services.Helpers;
using Laba_3.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Laba_3.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public IObservableObject<IProfile> Profile { get; set; }
		public ICommand LoginCommand { get; set; }
		public ICommand RegistrationCommand { get; set; }

		private readonly IUserRegisterService _userRegisterService;
		private readonly FirebaseAuthProvider _firebaseAuthProvider;

		public LoginViewModel(FirebaseAuthProvider firebaseAuthProvider, IUserRegisterService userRegisterService)
		{
			LoginCommand = new Command(OnLoginClicked);
			RegistrationCommand = new Command(OnRegistrationClicked);

			_firebaseAuthProvider = firebaseAuthProvider ?? throw new ArgumentNullException(nameof(firebaseAuthProvider));
			_userRegisterService = userRegisterService ?? throw new ArgumentNullException(nameof(userRegisterService));

			Profile = new ObservableObject<IProfile>
			{
				Property = new Profile
				{
					Password = new ObservableObject<string>
					{
						Property = string.Empty
					},
					Email = new ObservableObject<string>
					{
						Property = string.Empty
					},
					Name = new ObservableObject<string>
					{
						Property = string.Empty
					}
				}
			};
		}

		private async void OnRegistrationClicked(object obj)
		{
			bool result = await _userRegisterService.CreateUserWithEmailAndPasswordAsync(Profile.Property);

			if (result)
			{
				await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			}
		}

		private async void OnLoginClicked(object obj)
		{
			bool result = await _userRegisterService.SignInWithEmailAndPasswordAsync(Profile.Property);

			if (result)
			{
				await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			}
		}
	}
}

using Firebase.Auth;
using Laba_3.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Laba_3.Services
{
	internal class UserRegisterService : IUserRegisterService
	{
		private readonly FirebaseAuthProvider _firebaseAuthProvider;
		private readonly IUserService _userService;

		public UserRegisterService(FirebaseAuthProvider firebaseAuthProvider, IUserService userService)
		{
			_firebaseAuthProvider = firebaseAuthProvider ?? throw new ArgumentNullException(nameof(firebaseAuthProvider));
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
		}

		public async Task<bool> CreateUserWithEmailAndPasswordAsync(IProfile profile)
		{
			bool result = false;

			await _firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(profile.Email.Property, profile.Password.Property).ContinueWith(task =>
			{
				if (task.IsCanceled)
				{
					DependencyService.Get<IToast>().Show(task.Exception.Message);
					Debug.Fail("CreateUserWithEmailAndPasswordAsync was canceled.");
				}
				if (task.IsFaulted)
				{
					DependencyService.Get<IToast>().Show(task.Exception.Message);
					Debug.Fail("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);

				}

				if (task.Exception == null)
				{
					// Firebase user has been created.
					var newUser = task.Result.User;
					Debug.WriteLine("Firebase user created successfully: {0} ({1})",
						newUser.DisplayName, newUser.LocalId);

					_userService.Save(profile);
					DependencyService.Get<IToast>().Show("Пользователь зарегестрирован успешно!");

					result = true;
				}
			});

			return result;
		}

		public async Task<bool> SignInWithEmailAndPasswordAsync(IProfile profile)
		{
			bool result = false;

			await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(profile.Email.Property, profile.Password.Property).ContinueWith(task =>
			{
				if (task.IsCanceled)
				{
					DependencyService.Get<IToast>().Show(task.Exception.Message);
					Debug.Fail("SignInWithEmailAndPasswordAsync was canceled.");
				}
				if (task.IsFaulted)
				{
					DependencyService.Get<IToast>().Show(task.Exception.Message);
					Debug.Fail("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);

				}
				if (task.Exception == null)
				{
					var newUser = task.Result.User;
					Debug.WriteLine("Firebase user login successfully: {0} ({1})",
						newUser.DisplayName, newUser.LocalId);

					DependencyService.Get<IToast>().Show("Авторизация прошла успешно!");

					result = true;
				}
			});

			return result;
		}
	}
}

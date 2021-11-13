using Firebase.Auth;
using Laba_3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Laba_3.Services
{
	public class Container : IContainer
	{
		public IServiceProvider ServiceProvider { get; private set; }
		public IConfig Config { get; private set; }

		private readonly ServiceCollection _services;

		public Container(IConfig config)
		{
			_services = new ServiceCollection();

			Config = config ?? throw new ArgumentNullException(nameof(config));
			var firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(Config.FirebaseApiKey));

			_services.AddSingleton(firebaseAuthProvider);
			_services.AddSingleton<IUserService, UserService>();
			_services.AddSingleton<IUserRegisterService, UserRegisterService>();

			_services.AddTransient<AboutViewModel>();
			_services.AddTransient<LoginViewModel>();
			_services.AddTransient<ProfileViewModel>();
			_services.AddTransient<MainPageViewModel>();

			ServiceProvider = _services.BuildServiceProvider();
		}
	}
}

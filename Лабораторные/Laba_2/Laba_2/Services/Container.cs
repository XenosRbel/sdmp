using Laba_2.Services.Network.Api;
using Laba_2.Services.Network.Request;
using Laba_2.Services.Repositories;
using Laba_2.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Laba_2.Services
{
	public class Container : IContainer
	{
		public ServiceProvider ServiceProvider { get; private set; }
		public IConfig Config { get; private set; }

		private readonly ServiceCollection _services;

		public Container(IConfig config)
		{
			_services = new ServiceCollection();

			Config = config ?? throw new ArgumentNullException(nameof(config));

			_services.AddSingleton<IRepository, MemoryRepository>();
			_services.AddSingleton<IRequestService, RequestService>();
			_services.AddSingleton<IApiService, ApiService>();
			_services.AddSingleton<PhoneService>();
			_services.AddSingleton<TelescopeService>();
			_services.AddSingleton(Config);

			_services.AddTransient<HomeViewModel>();
			_services.AddTransient<ProfileViewModel>();

			ServiceProvider = _services.BuildServiceProvider();
		}
	}
}

using Laba_3.Models;
using NetworkApi.Api;
using NetworkApi.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Laba_3.Services
{
	internal class HoroscopeService : ServiceBase, IHoroscopeService
	{
		private readonly IConfig _config;

		public HoroscopeService(IApiService apiService, IConfig config)
		{
			_apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		internal override Uri GenerateUri()
		{
			var uri = new Uri("", UriKind.Absolute);

			return uri;
		}

		public Task<IHoroscopeResponse> FetchAsync(IProgressHandler progressHandler, CancellationToken token)
		{
			return base.FetchAsync<IHoroscopeResponse>(progressHandler, token);
		}
	}
}

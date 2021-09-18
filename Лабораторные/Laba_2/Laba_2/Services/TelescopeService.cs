using Laba_2.Services.Network.Api;
using System;

namespace Laba_2.Services
{
	internal class TelescopeService : ProductServiceBase, IProductService
	{
		private readonly IConfig _config;

		public TelescopeService(IApiService apiService, IConfig config)
		{
			this._apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		internal override Uri GenerateUri()
		{
			var uri = new Uri(_config.OnlinerTelescopeApiUrl, UriKind.Absolute);

			return uri;
		}
	}
}

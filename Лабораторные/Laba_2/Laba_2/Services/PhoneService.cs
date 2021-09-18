using Laba_2.Services.Network.Api;
using System;

namespace Laba_2.Services
{
	internal class PhoneService : ProductServiceBase, IProductService
	{
		private readonly IConfig _config;

		public PhoneService(IApiService apiService, IConfig config)
		{
			_apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		internal override Uri GenerateUri()
		{
			var uri = new Uri(_config.OnlinerPhonesApiUrl, UriKind.Absolute);

			return uri;
		}
	}
}

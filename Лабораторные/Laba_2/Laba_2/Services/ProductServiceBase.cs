using Laba_2.Services.Network;
using Laba_2.Services.Network.Api;
using Laba_2.Services.Network.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Laba_2.Services
{
	internal abstract class ProductServiceBase
	{
		internal IApiService _apiService;

		public async Task<ProductResponse> FetchAsync(ProgressHandler progressHandler,
			CancellationToken token)
		{
			Uri uri = GenerateUri();

			return await _apiService.GetAsync<ProductResponse>(uri.OriginalString, token, progressHandler);
		}

		internal abstract Uri GenerateUri();
	}
}
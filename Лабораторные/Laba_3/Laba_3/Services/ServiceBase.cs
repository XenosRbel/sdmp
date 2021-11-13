using NetworkApi.Api;
using NetworkApi.Response;
using NetworkApi.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Laba_3.Services
{
	internal abstract class ServiceBase
	{
		internal IApiService _apiService;

		public async Task<TResponse> FetchAsync<TResponse>(IProgressHandler progressHandler,
			CancellationToken token) where TResponse : IBaseResponse
		{
			Uri uri = GenerateUri();

			return await _apiService.GetAsync<TResponse>(uri.OriginalString, token, progressHandler);
		}

		internal abstract Uri GenerateUri();
	}
}

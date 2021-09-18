using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Laba_2.Services.Network.Request;
using Laba_2.Services.Network.Response;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Laba_2.Services.Network.Api
{
	internal class ApiService : IApiService
	{
		private readonly IRequestService _requestService;

		public ApiService(IRequestService requestService)
		{
			this._requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
		}

		public async Task<TResponse> GetAsync<TResponse>(
			string apiUrl,
			CancellationToken token,
			ProgressHandler progressHandler = null)
			where TResponse : BaseResponse
		{
			if (apiUrl == null) throw new ArgumentNullException(nameof(apiUrl));

			var response = await _requestService.RequestAsync(() => CreateRequestMessage(apiUrl), 10000, token);
			var deserializedResponse = await HandleResponse<TResponse>(response, progressHandler, token);

			return deserializedResponse;
		}

		private HttpRequestMessage CreateRequestMessage(string apiUrl)
		{
			return RequestBuilder.CreateRequest()
				.SetUrl(apiUrl)
				.SetHeaders(new Dictionary<string, string>
				{
					//{AppIdHeader, config.InstallationId},
					//{VoucherIdHeader, config.VoucherId},
				})
				.SetHttpMethod(HttpMethod.Get)
				.Build();
		}

		private async Task<TResponse> HandleResponse<TResponse>(
			HttpResponseMessage response,
			ProgressHandler progressHandler,
			CancellationToken token)
			where TResponse : BaseResponse
		{
			Stream stream = await GetStreamFromResponse(response, progressHandler, token);

			TResponse responseObject;
			try
			{
				responseObject = await Task.Run(() => Deserialize<TResponse>(stream), token);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Can not deserialize object with type {typeof(TResponse)}", e);
			}


			if (responseObject?.Error != null)
			{
				throw new ApiRequestException("Error was encounted while request processing", responseObject.Error);
			}

			return responseObject;
		}

		private async Task<Stream> GetStreamFromResponse(HttpResponseMessage response, ProgressHandler progressHandler, CancellationToken token)
		{
			var stream = await response.Content.ReadAsStreamAsync();

			if (progressHandler == null) return stream;

			long? contentLength = response.Content.Headers.ContentLength;

			var progressStream = new ProgressStream(stream, contentLength, token);

			progressStream.ProgressChanged += progressHandler.ProgressChanged;

			return progressStream;
		}

		private TResponse Deserialize<TResponse>(Stream stream)
		{
			StreamReader reader = new StreamReader(stream);
			string json = reader.ReadToEnd();


			return JsonConvert.DeserializeObject<TResponse>(json, Converter.Settings);
		}
	}
}
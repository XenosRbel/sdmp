using NetworkApi.Exceptions;
using NetworkApi.Models;
using NetworkApi.Request;
using NetworkApi.Response;
using NetworkApi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkApi.Api
{
    internal class ApiService : IApiService
    {
        private readonly string _serverUrl;

        private readonly IRecoverableRequestService _recoverableRequestService;

		public ApiService(IRecoverableRequestService recoverableRequestService, string serverUrl)
		{
			_recoverableRequestService = recoverableRequestService ?? throw new ArgumentNullException(nameof(recoverableRequestService));
			_serverUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
        }

		public async Task<TResponse> GetAsync<TResponse>(
            string apiUrl,
            CancellationToken token,
            IProgressHandler progressHandler = null)
            where TResponse : IBaseResponse
        {
            if (apiUrl == null) throw new ArgumentNullException(nameof(apiUrl));

            var response = await _recoverableRequestService.RequestAsync(() => CreateRequestMessage(apiUrl), token);
            var deserializedResponse = await HandleResponse<TResponse>(response, progressHandler, token);

            return deserializedResponse;
        }

        public async Task GetFile(ILoadFileRequest loadFileRequest, CancellationToken token, IProgressHandler progressHandler = null)
        {
            if (loadFileRequest.FilePath == null) throw new ArgumentNullException(nameof(loadFileRequest));

            var response = await _recoverableRequestService.RequestAsync(() => CreateRequestMessage(loadFileRequest.ApiUrl), 10000, token);
            var stream = await GetStreamFromResponse(response, progressHandler, token);

            await Task.Run(() =>
            {
                using (var file = new FileStream(loadFileRequest.FilePath, FileMode.CreateNew))
                {
                    stream.CopyTo(file);
                }
			}, token);
        }

        public HttpRequestMessage CreateBroadcastRequestMessage(string apiUrl)
        {
            var requestMessage = CreateRequestMessage(apiUrl);

            var baseUrl = _serverUrl ?? string.Empty;
            var fullUrlPath = Path.Combine(baseUrl, requestMessage.RequestUri.OriginalString);
            var fullUrl = new Uri(fullUrlPath);

            requestMessage.RequestUri = fullUrl;
            return requestMessage;
        }

        private HttpRequestMessage CreateRequestMessage(string apiUrl)
        {
            return RequestBuilder.CreateRequest()
                .SetUrl(apiUrl)
                .SetHeaders(new Dictionary<string, string>
                {
                })
                .SetHttpMethod(HttpMethod.Get)
                .Build();
        }

        private async Task<TResponse> HandleResponse<TResponse>(
            HttpResponseMessage response,
            IProgressHandler progressHandler,
            CancellationToken token)
            where TResponse : IBaseResponse
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

        private async Task<Stream> GetStreamFromResponse(HttpResponseMessage response, IProgressHandler progressHandler, CancellationToken token)
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
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<TResponse>(jsonTextReader);
            }
        }
    }
}

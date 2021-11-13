using NetworkApi.Exceptions;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkApi.Request
{
    internal class RequestService : IRequestService, IDisposable
    {
        private readonly HttpClient client;
        private readonly string _serverUrl;

		public RequestService(string serverUrl)
		{
            _serverUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));

            client = new HttpClient();

			client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
        }

		public async Task<HttpResponseMessage> RequestAsync(
            Func<HttpRequestMessage> requestMessageFactory,
            int timeoutMillis,
            CancellationToken token)
        {
            if (requestMessageFactory == null) throw new ArgumentNullException(nameof(requestMessageFactory));
            if (timeoutMillis <= 0) throw new ArgumentOutOfRangeException(nameof(timeoutMillis));

            var requestMessage = GetRequestMessage(requestMessageFactory);

            var url = BuildFullUrl(requestMessage);

            requestMessage.RequestUri = url;

            var chainedCancellationToken = CreateCancellationToken(timeoutMillis, token);

            var response = await SendRequest(requestMessage, chainedCancellationToken);

            ThrowIfUnsuccessfull(response);

            return response;
        }

        private CancellationToken CreateCancellationToken(int timeoutMillis, CancellationToken token)
        {
            var cancellationTokenSource = new CancellationTokenSource(timeoutMillis);

            var result = cancellationTokenSource.Token;

            if (token != CancellationToken.None)
            {
                result = CancellationTokenSource.CreateLinkedTokenSource(result, token).Token;
            }

            return result;
        }

        private static HttpRequestMessage GetRequestMessage(Func<HttpRequestMessage> requestMessageFactory)
        {
            var requestMessage = requestMessageFactory.Invoke();

            if (requestMessage == null) throw new InvalidOperationException("Request message can not be null");

            return requestMessage;
        }

        private Uri BuildFullUrl(HttpRequestMessage requestMessage)
        {
            var originalUrl = requestMessage.RequestUri.OriginalString;

            if (requestMessage.RequestUri.IsAbsoluteUri)
            {
                return new Uri(originalUrl);
            }

            var baseUrl = _serverUrl ?? string.Empty;

            var fullUrl = Path.Combine(baseUrl, originalUrl);

            return new Uri(fullUrl);
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage requestMessage, CancellationToken token)
        {
            try
            {
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, token);
            }
            catch (HttpRequestException ex)
            {
                Debug.Fail("Network is unawailable", ex.Message);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                Debug.Fail("Network connection timeout", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Debug.Fail("Can not perform request", ex.Message);
                throw;
            }
        }

        private void ThrowIfUnsuccessfull(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                Debug.Fail($"Unsuccessfull status code returned:  {response.StatusCode}; {response.Content}");
                throw new UnsuccessfullRequestException(response);
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}

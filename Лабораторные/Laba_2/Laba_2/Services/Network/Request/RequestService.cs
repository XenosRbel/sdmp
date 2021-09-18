using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Laba_2.Services.Network.Request
{
	internal class RequestService : IRequestService, IDisposable
	{
		private readonly HttpClient _client;

		public RequestService()
		{
			_client = new HttpClient();

			_client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
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

			var sw = Stopwatch.StartNew();
			Debug.WriteLine($"{url}..");

			var response = await SendRequest(requestMessage, chainedCancellationToken);

			Debug.WriteLine($"{url} - completed in {sw.ElapsedMilliseconds} ms");

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

			return new Uri(string.Empty);
		}

		private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage requestMessage, CancellationToken token)
		{
			try
			{
				return await _client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, token);
			}
			catch (HttpRequestException ex)
			{
				Debug.WriteLine(ex, "Network is unawailable");
				throw;
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine(ex, "Network connection timeout");
				throw;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex, "Can not perform request");
				throw;
			}
		}

		private void ThrowIfUnsuccessfull(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode)
			{
				Debug.WriteLine($"Unsuccessfull status code returned:  {response.StatusCode}; {response.Content}");
				throw new UnsuccessfullRequestException(response);
			}
		}

		public void Dispose()
		{
			_client?.Dispose();
		}
	}
}

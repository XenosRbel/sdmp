using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Laba_2.Services.Network.Request
{
	internal class RequestBuilder
	{
		private HttpMethod method;
		private string url;
		private IDictionary<string, string> headers;

		private RequestBuilder() { }

		public static RequestBuilder CreateRequest()
		{
			return new RequestBuilder();
		}

		public RequestBuilder SetHttpMethod(HttpMethod method)
		{
			this.method = method ?? throw new ArgumentNullException(nameof(method));

			return this;
		}

		public RequestBuilder SetUrl(string url)
		{
			this.url = url ?? throw new ArgumentNullException(nameof(url));
			return this;
		}

		public RequestBuilder SetHeaders(IDictionary<string, string> headers)
		{
			this.headers = headers ?? throw new ArgumentNullException(nameof(headers));
			return this;
		}

		public HttpRequestMessage Build()
		{
			if (string.IsNullOrEmpty(url)) throw new InvalidOperationException("Url can not be empty");

			var requestMessage = new HttpRequestMessage(method ?? HttpMethod.Get, url);

			if (headers != null)
			{
				foreach (var header in headers)
				{
					var added = requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
					if (!added)
					{
						throw new InvalidOperationException($"Header {header.Key} with value {header.Value} can not be added");
					}
				}
			}

			return requestMessage;
		}
	}
}

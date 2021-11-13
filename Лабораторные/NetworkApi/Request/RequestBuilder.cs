using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NetworkApi.Request
{
	internal class RequestBuilder : IRequestBuilder
	{
		private HttpMethod _method;
		private string _url;
		private IDictionary<string, string> _headers;

		private RequestBuilder() { }

		public static RequestBuilder CreateRequest()
		{
			return new RequestBuilder();
		}

		public IRequestBuilder SetHttpMethod(HttpMethod method)
		{
			_method = method ?? throw new ArgumentNullException(nameof(method));

			return this;
		}

		public IRequestBuilder SetUrl(string url)
		{
			_url = url ?? throw new ArgumentNullException(nameof(url));
			return this;
		}

		public IRequestBuilder SetHeaders(IDictionary<string, string> headers)
		{
			_headers = headers ?? throw new ArgumentNullException(nameof(headers));
			return this;
		}

		public HttpRequestMessage Build()
		{
			if (string.IsNullOrEmpty(_url)) throw new InvalidOperationException("Url can not be empty");

			var requestMessage = new HttpRequestMessage(_method ?? HttpMethod.Get, _url);

			if (_headers != null)
			{
				foreach (var header in _headers)
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

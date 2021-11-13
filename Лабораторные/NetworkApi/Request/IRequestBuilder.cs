using System.Collections.Generic;
using System.Net.Http;

namespace NetworkApi.Request
{
	public interface IRequestBuilder
	{
		HttpRequestMessage Build();
		IRequestBuilder SetHeaders(IDictionary<string, string> headers);
		IRequestBuilder SetHttpMethod(HttpMethod method);
		IRequestBuilder SetUrl(string url);
	}
}

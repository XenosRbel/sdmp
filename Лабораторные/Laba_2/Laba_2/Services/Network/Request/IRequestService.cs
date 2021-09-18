using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Laba_2.Services.Network.Request
{
	public interface IRequestService
	{
		Task<HttpResponseMessage> RequestAsync(Func<HttpRequestMessage> requestMessageFactory, int timeoutMillis, CancellationToken token);
	}
}

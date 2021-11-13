using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkApi.Request
{
	public interface IRecoverableRequestService : IRequestService
    {
        Task<HttpResponseMessage> RequestAsync(Func<HttpRequestMessage> requestMessageFactory, CancellationToken token);
    }
}

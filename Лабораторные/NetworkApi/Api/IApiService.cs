using NetworkApi.Models;
using NetworkApi.Response;
using NetworkApi.Services;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkApi.Api
{
	public interface IApiService
    {
        Task<TResponse> GetAsync<TResponse>(string apiUrl, CancellationToken token, IProgressHandler progressHandler = null)
            where TResponse : IBaseResponse;

        Task GetFile(ILoadFileRequest loadFileRequest, CancellationToken token, IProgressHandler progressHandler = null);
        HttpRequestMessage CreateBroadcastRequestMessage(string apiUrl);

    }
}

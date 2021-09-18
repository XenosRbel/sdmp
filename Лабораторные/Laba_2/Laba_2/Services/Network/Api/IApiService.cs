using Laba_2.Services.Network.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Laba_2.Services.Network.Api
{
	public interface IApiService
	{
		Task<TResponse> GetAsync<TResponse>(string apiUrl, CancellationToken token, ProgressHandler progressHandler = null)
			where TResponse : BaseResponse;
	}
}

using Laba_2.Services.Network;
using Laba_2.Services.Network.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Laba_2.Services
{
	internal interface IProductService
	{
		Task<ProductResponse> FetchAsync(ProgressHandler progressHandler, CancellationToken token);
	}
}
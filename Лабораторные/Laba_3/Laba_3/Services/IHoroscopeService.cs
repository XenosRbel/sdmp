using Laba_3.Models;
using NetworkApi.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Laba_3.Services
{
	internal interface IHoroscopeService
	{
		Task<IHoroscopeResponse> FetchAsync(IProgressHandler progressHandler, CancellationToken token);
	}
}

using System.Threading;
using System.Threading.Tasks;

namespace NetworkApi.Reachability
{
	public interface INetworkReachability
    {
        Task<bool> IsNetworkAvailableAsync(int timeoutPerRequestMillis, int timeoutPerWholeAttemptMillis, CancellationToken external);
    }
}

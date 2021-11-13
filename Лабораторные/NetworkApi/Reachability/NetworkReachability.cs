using NetworkApi.Request;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkApi.Reachability
{
	internal class NetworkReachability : INetworkReachability
    {
        private readonly IRequestService _requestService;
        private readonly string _networkReachabilityUrl;
        private readonly string _networkReachabilitySubstring;

        public NetworkReachability(IRequestService requestService, string networkReachabilityUrl, string networkReachabilitySubstring)
        {
            _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            _networkReachabilityUrl = networkReachabilityUrl ?? throw new ArgumentNullException(nameof(networkReachabilityUrl));
            _networkReachabilitySubstring = networkReachabilitySubstring ?? throw new ArgumentNullException(nameof(networkReachabilitySubstring));
        }

        public async Task<bool> IsNetworkAvailableAsync(int timeoutPerRequestMillis, int timeoutPerWholeAttemptMillis, CancellationToken external)
        {
            #region ValidateTimeouts

            if (timeoutPerRequestMillis <= 0)
                throw new ArgumentOutOfRangeException(nameof(timeoutPerRequestMillis));

            if (timeoutPerWholeAttemptMillis <= 0)
                throw new ArgumentOutOfRangeException(nameof(timeoutPerWholeAttemptMillis));

            if (timeoutPerWholeAttemptMillis < timeoutPerRequestMillis)
                throw new ArgumentException($"{nameof(timeoutPerWholeAttemptMillis)} must be greater than {nameof(timeoutPerRequestMillis)}");

            #endregion

            var cancellationTokenSource = new CancellationTokenSource(timeoutPerWholeAttemptMillis);

            var tokenForWholeAttempt = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token, external).Token;

            bool isAvailable;
            do
            {
                try
                {
                    isAvailable = await CheckAvailabilityAsync(_networkReachabilityUrl, timeoutPerRequestMillis, tokenForWholeAttempt);
                }
                catch (Exception)
                {
                    isAvailable = false;

                    if (tokenForWholeAttempt.IsCancellationRequested)
                    {
                        break;
                    }
                }

            } while (!isAvailable);

            return isAvailable;
        }

        private async Task<bool> CheckAvailabilityAsync(string url, int timeoutMillis, CancellationToken cancellationToken)
        {
            bool isAvailable;

            HttpRequestMessage RequestMessageFactory() => RequestBuilder.CreateRequest()
                .SetUrl(url)
                .SetHttpMethod(HttpMethod.Get)
                .Build();

            var content = await _requestService.RequestAsync(RequestMessageFactory, timeoutMillis, cancellationToken);

            var stringContent = await content.Content.ReadAsStringAsync();
            isAvailable = stringContent.Contains(_networkReachabilitySubstring);

            return isAvailable;
        }
    }
}

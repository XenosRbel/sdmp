using NetworkApi.Exceptions;
using NetworkApi.Reachability;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkApi.Request
{
	internal class RecoverableRequestService : IRecoverableRequestService
	{
		private readonly INetworkReachability _networkReachabilityChecker;
		private readonly IRequestService _requestService;
		private readonly int _reachabilityRequestTimeoutMillis;
		private readonly int _reachabilityWatingForInternetAfterFailureMillis;
		private readonly int _attemptsCount;
		private readonly int _delayBetweenAttemptsMillis;
		private readonly int _connectionTimeoutMillis;

		public RecoverableRequestService(
			INetworkReachability networkReachabilityChecker,
			IRequestService requestService,
			int reachabilityRequestTimeoutMillis,
			int reachabilityWatingForInternetAfterFailureMillis,
			int attemptsCount,
			int delayBetweenAttemptsMillis,
			int connectionTimeoutMillis)
		{
			_networkReachabilityChecker = networkReachabilityChecker ?? throw new ArgumentNullException(nameof(networkReachabilityChecker));
			_requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
			_reachabilityRequestTimeoutMillis = reachabilityRequestTimeoutMillis;
			_reachabilityWatingForInternetAfterFailureMillis = reachabilityWatingForInternetAfterFailureMillis;
			_attemptsCount = attemptsCount;
			_delayBetweenAttemptsMillis = delayBetweenAttemptsMillis;
			_connectionTimeoutMillis = connectionTimeoutMillis;
		}


		public async Task<HttpResponseMessage> RequestAsync(
			Func<HttpRequestMessage> requestMessageFactory,
			int timeoutMillis,
			CancellationToken token)
		{
			if (requestMessageFactory == null) throw new ArgumentNullException(nameof(requestMessageFactory));
			if (timeoutMillis <= 0) throw new ArgumentOutOfRangeException(nameof(timeoutMillis));

			var attemptsCount = _attemptsCount;
			var currentAttempt = 0;

			bool isNetworkAvailable;

			var exceptions = new List<Exception>();
			do
			{
				currentAttempt++;

				try
				{
					return await _requestService.RequestAsync(requestMessageFactory, timeoutMillis, token);
				}
				catch (Exception ex)
				{
					await Task.Delay(_delayBetweenAttemptsMillis, token);

					isNetworkAvailable = await _networkReachabilityChecker.IsNetworkAvailableAsync(
						_reachabilityRequestTimeoutMillis,
						_reachabilityWatingForInternetAfterFailureMillis,
						CancellationToken.None);

					exceptions.Add(ex);
				}

			} while (isNetworkAvailable && (currentAttempt < attemptsCount));


			throw new NetworkAvailabilityExceptions("Network is unavailable", new AggregateException(exceptions));
		}

		public Task<HttpResponseMessage> RequestAsync(Func<HttpRequestMessage> requestMessageFactory, CancellationToken token)
		{
			return RequestAsync(requestMessageFactory, _connectionTimeoutMillis, token);
		}
	}
}

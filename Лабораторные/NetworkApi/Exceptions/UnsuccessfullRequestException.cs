using System;
using System.Net.Http;

namespace NetworkApi.Exceptions
{
	internal class UnsuccessfullRequestException : Exception
	{
		public UnsuccessfullRequestException()
		{
		}

		public UnsuccessfullRequestException(HttpResponseMessage response)
		{
			this.Response = response;
		}

		public UnsuccessfullRequestException(string message) : base(message)
		{
		}

		public UnsuccessfullRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public HttpResponseMessage Response { get; set; }
	}
}

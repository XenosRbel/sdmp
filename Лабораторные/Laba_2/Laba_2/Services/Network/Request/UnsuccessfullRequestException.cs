using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Laba_2.Services.Network.Request
{
	[Serializable]
	internal class UnsuccessfullRequestException : Exception
	{
		private readonly HttpResponseMessage response;

		public UnsuccessfullRequestException()
		{
		}

		public UnsuccessfullRequestException(HttpResponseMessage response)
		{
			this.response = response;
		}

		public UnsuccessfullRequestException(string message) : base(message)
		{
		}

		public UnsuccessfullRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UnsuccessfullRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
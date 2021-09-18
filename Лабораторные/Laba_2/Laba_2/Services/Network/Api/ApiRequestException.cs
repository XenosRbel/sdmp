using Laba_2.Services.Network.Response;
using System;

namespace Laba_2.Services.Network.Api
{
	internal class ApiRequestException : Exception
	{
		private string message;
		public ErrorResponse ErrorDto { get; private set; }

		public ApiRequestException()
		{
		}

		public ApiRequestException(string message) : base(message)
		{
		}

		public ApiRequestException(string message, ErrorResponse errorDto)
		{
			this.message = message;
			ErrorDto = errorDto;
		}

		public ApiRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public override string ToString()
		{
			if (ErrorDto == null) return base.ToString();


			return "Request error: \n " +
				   $"Code: {ErrorDto?.Code} \n";
		}
	}
}
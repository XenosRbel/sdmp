using NetworkApi.Response;
using System;

namespace NetworkApi.Exceptions
{
	public class ApiRequestException : Exception
    {
        public IErrorResponse ErrorDto { get; private set; }

        public ApiRequestException()
        {
        }

        public ApiRequestException(string message) : base(message)
        {
        }

        public ApiRequestException(string message, IErrorResponse errorDto) : base(message)
        {
            this.ErrorDto = errorDto;
        }

        public ApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override string ToString()
        {
            if (ErrorDto == null) return base.ToString();


            return "Request error: \n " +
                   $"Code: {ErrorDto?.Code} \n" +
                   $"Message: {ErrorDto?.Message} " +
                   $"Description: {ErrorDto?.Code}";
        }
    }
}

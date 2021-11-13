using System;

namespace NetworkApi.Exceptions
{
	internal class NetworkAvailabilityExceptions : Exception
    {
        public NetworkAvailabilityExceptions()
        {
        }

        public NetworkAvailabilityExceptions(string message) : base(message)
        {
        }

        public NetworkAvailabilityExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

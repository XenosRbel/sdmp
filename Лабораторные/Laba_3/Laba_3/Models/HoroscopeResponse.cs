using NetworkApi.Response;

namespace Laba_3.Models
{
	internal class HoroscopeResponse : IBaseResponse, IHoroscopeResponse
	{
		public IErrorResponse Error { get; set; }
		public string Protocol { get; set; }
	}
}

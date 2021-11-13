namespace NetworkApi.Response
{
	internal class BaseResponse : IBaseResponse
	{
		public IErrorResponse Error { get; set; }
		public string Protocol { get; set; }
	}
}

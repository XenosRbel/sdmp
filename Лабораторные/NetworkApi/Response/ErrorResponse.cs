namespace NetworkApi.Response
{
	internal class ErrorResponse : IErrorResponse
	{
		public string Code { get; set; }

		public string Message { get; set; }

		public string Description { get; set; }
	}
}

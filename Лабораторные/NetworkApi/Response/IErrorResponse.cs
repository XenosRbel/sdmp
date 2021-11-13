namespace NetworkApi.Response
{
	public interface IErrorResponse
	{
		string Code { get; set; }
		string Description { get; set; }
		string Message { get; set; }
	}
}

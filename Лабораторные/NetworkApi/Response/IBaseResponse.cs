namespace NetworkApi.Response
{
	public interface IBaseResponse
	{
		IErrorResponse Error { get; set; }
		string Protocol { get; set; }
	}
}

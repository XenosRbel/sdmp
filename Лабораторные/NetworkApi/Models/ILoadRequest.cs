namespace NetworkApi.Models
{
	public interface ILoadRequest
	{
		string ApiUrl { get; set; }
		int Status { get; set; }
	}
}
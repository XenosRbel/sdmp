namespace NetworkApi.Models
{
	public interface ILoadFileRequest : ILoadRequest
	{
		string FilePath { get; set; }
	}
}
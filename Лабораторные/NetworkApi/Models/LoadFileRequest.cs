namespace NetworkApi.Models
{
	internal class LoadFileRequest : ILoadFileRequest
	{
		public int Status { set; get; }
		public string ApiUrl { set; get; }
		public string FilePath { set; get; }
	}
}

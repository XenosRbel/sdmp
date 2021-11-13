namespace NetworkApi.Models
{
	internal class LoadRequest : ILoadRequest
	{
		public int Status { set; get; }
		public string ApiUrl { set; get; }
	}
}

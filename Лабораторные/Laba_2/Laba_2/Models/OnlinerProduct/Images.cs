using Newtonsoft.Json;

namespace Laba_2.Models.OnlinerProduct
{
	public partial class Images
	{
		[JsonProperty("header")]
		public string Header { get; set; }

		[JsonProperty("icon")]
		public object Icon { get; set; }
	}
}

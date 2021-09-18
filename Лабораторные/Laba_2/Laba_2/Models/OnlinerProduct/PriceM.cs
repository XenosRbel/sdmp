using Newtonsoft.Json;

namespace Laba_2.Models.OnlinerProduct
{
	public partial class PriceM
	{
		[JsonProperty("amount")]
		public string Amount { get; set; }

		[JsonProperty("currency")]
		public Currency Currency { get; set; }
	}
}

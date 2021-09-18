using Newtonsoft.Json;

namespace Laba_2.Models.OnlinerProduct
{
	public partial class Prices
	{
		[JsonProperty("price_min")]
		public PriceM PriceMin { get; set; }

		[JsonProperty("price_max")]
		public PriceM PriceMax { get; set; }
	}
}

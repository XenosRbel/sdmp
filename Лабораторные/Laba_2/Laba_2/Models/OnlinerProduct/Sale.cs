using Newtonsoft.Json;

namespace Laba_2.Models.OnlinerProduct
{
	public partial class Sale
	{
		[JsonProperty("is_on_sale")]
		public bool IsOnSale { get; set; }

		[JsonProperty("discount")]
		public long Discount { get; set; }

		[JsonProperty("min_prices_median")]
		public MinPricesMedian MinPricesMedian { get; set; }
	}
}

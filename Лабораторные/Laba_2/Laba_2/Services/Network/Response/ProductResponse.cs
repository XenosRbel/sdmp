using Laba_2.Models.OnlinerProduct;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Laba_2.Services.Network.Response
{
	internal class ProductResponse : BaseResponse
	{
		[JsonProperty("products")]
		public List<Product> Products { get; set; }

		[JsonProperty("total")]
		public long Total { get; set; }
	}
}

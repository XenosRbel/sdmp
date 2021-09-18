using Newtonsoft.Json;
using System;

namespace Laba_2.Models.OnlinerProduct
{
	public partial class Product : IProduct
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("full_name")]
		public string FullName { get; set; }

		[JsonProperty("name_prefix")]
		public NamePrefix NamePrefix { get; set; }

		[JsonProperty("extended_name")]
		public string ExtendedName { get; set; }

		[JsonProperty("images")]
		public Images Images { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("micro_description")]
		public string MicroDescription { get; set; }

		[JsonProperty("html_url")]
		public Uri HtmlUrl { get; set; }

		[JsonProperty("prices")]
		public Prices Prices { get; set; }

		[JsonProperty("sale")]
		public Sale Sale { get; set; }

		[JsonProperty("url")]
		public Uri Url { get; set; }
	}
}

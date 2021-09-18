using System;

namespace Laba_2.Models.OnlinerProduct
{
	public interface IProduct
	{
		string Description { get; set; }
		string ExtendedName { get; set; }
		string FullName { get; set; }
		Uri HtmlUrl { get; set; }
		long Id { get; set; }
		Images Images { get; set; }
		string Key { get; set; }
		string MicroDescription { get; set; }
		string Name { get; set; }
		NamePrefix NamePrefix { get; set; }
		Prices Prices { get; set; }
		Sale Sale { get; set; }
		Uri Url { get; set; }
	}
}
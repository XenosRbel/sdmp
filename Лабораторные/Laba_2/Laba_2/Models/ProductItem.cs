namespace Laba_2.Models
{
	public class ProductItem : IProductItem, IBaseEntity
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string ExtendedName { get; set; }

		public double Price { get; set; }

		public string ImageUrl { get; set; }

		public string NamePrefix { get; set; }

		public string Currency { get; set; }
	}
}

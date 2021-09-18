namespace Laba_2.Models
{
	public interface IProductItem : IBaseEntity
	{
		string Name { get; }
		string ExtendedName { get; }
		double Price { get; }
		string ImageUrl { get; }
		string NamePrefix { get; }
		string Currency { get; }
	}
}

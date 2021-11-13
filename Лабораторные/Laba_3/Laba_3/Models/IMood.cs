namespace Laba_3.Models
{
	public interface IMood
	{
		string ImageUrl { get; set; }
		string Title { get; set; }
		MoodType MoodType { get; set; }
	}
}
using Laba_3.Services.Helpers;

namespace Laba_3.Models
{
	internal class Profile : IProfile
	{
		public ObservableObject<string> Name { get; set; }

		public ObservableObject<string> Email { get; set; }
		public ObservableObject<string> Password { get; set; }
	}
}

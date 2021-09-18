using Laba_2.Services.Helpers;
using System;

namespace Laba_2.Models
{
	internal class Profile : IProfile
	{
		public ObservableObject<string> LastName { get; set; }

		public ObservableObject<string> FirstName { get; set; }

		public ObservableObject<string> AvatarUrl { get; set; }

		public ObservableObject<string> PhoneNumber { get; set; }

		public ObservableObject<DateTime> DateOfBirth { get; set; }
	}
}

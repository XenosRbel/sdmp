using Laba_2.Services.Helpers;
using System;

namespace Laba_2.Models
{
	internal interface IProfile
	{
		ObservableObject<string> AvatarUrl { get; set; }
		ObservableObject<DateTime> DateOfBirth { get; set; }
		ObservableObject<string> FirstName { get; set; }
		ObservableObject<string> LastName { get; set; }
		ObservableObject<string> PhoneNumber { get; set; }
	}
}
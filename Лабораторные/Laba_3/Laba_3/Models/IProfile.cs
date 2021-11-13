using Laba_3.Services.Helpers;
using System;

namespace Laba_3.Models
{
	public interface IProfile
	{
		ObservableObject<string> Password { get; set; }
		ObservableObject<string> Email { get; set; }
		ObservableObject<string> Name { get; set; }
	}
}

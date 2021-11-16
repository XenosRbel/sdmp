using System;
using System.Collections.Generic;
using System.Text;
using Laba_4.Models;

namespace Laba_4.Validations.Rules
{
	public class IsNameNotNullOrWhiteSpace : IBaseValidationRule<Contact>
	{
		public IReadOnlyList<string> Properties { get; } = new List<string>() { nameof(Contact.Name) };
		public string ValidationMessage { get; set; }
		public bool Check(Contact value)
		{
			return !string.IsNullOrWhiteSpace(value.Name);
		}
	}

	public class IsPhoneNumberNotNullOrWhiteSpace : IBaseValidationRule<Contact>
	{
		public IReadOnlyList<string> Properties { get; } = new List<string>() { nameof(Contact.PhoneNumber) };
		public string ValidationMessage { get; set; }
		public bool Check(Contact value)
		{
			return !string.IsNullOrWhiteSpace(value.PhoneNumber);
		}
	}

	public class IsPhotoPathNotNullOrWhiteSpace : IBaseValidationRule<Contact>
	{
		public IReadOnlyList<string> Properties { get; } = new List<string>() { nameof(Contact.PhotoPath) };
		public string ValidationMessage { get; set; }
		public bool Check(Contact value)
		{
			return !string.IsNullOrWhiteSpace(value.PhotoPath);
		}
	}
}

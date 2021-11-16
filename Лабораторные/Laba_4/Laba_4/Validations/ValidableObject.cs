using System.Collections.Generic;
using System.Linq;

namespace Laba_4.Validations
{
	public class ValidableObject<T> : IValidity
	{
		private readonly IReadOnlyList<IBaseValidationRule<T>> _rules;

		public bool IsValid { get; set; }

		public ValidableObject(IReadOnlyList<IBaseValidationRule<T>> rules)
		{
			IsValid = false;
			_rules = rules;
		}

		public ValidationResult Validate(T value)
		{
			var errors = _rules.Where(item => !item.Check(value))
				.ToDictionary(x => x.Properties, y => y.ValidationMessage);
			var validationResult = new ValidationResult(errors);

			return validationResult;
		}
	}
}

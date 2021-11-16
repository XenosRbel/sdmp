using System.Collections.Generic;
using System.Linq;

namespace Laba_4.Validations
{
	public class ValidationResult
	{
		public bool IsValid => !Errors.Any();

		public IDictionary<IReadOnlyList<string>, string> Errors { get; }

		public ValidationResult(Dictionary<IReadOnlyList<string>, string> errors)
		{
			if (errors == null)
			{
				errors = new Dictionary<IReadOnlyList<string>, string>();
			}
			Errors = errors;
		}
	}
}

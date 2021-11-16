using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Laba_4.Models;

namespace Laba_4.Services
{
	public interface INavigationService
	{
		Task PopAsync();
		Task PushContactDetailsPageAsync(Contact contact);
		Task PushContactsPageAsync();
		Task PushEditContactAsync();
		Task PushEditContactAsync(Contact contact);
	}
}

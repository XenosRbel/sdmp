using Laba_3.Models;

namespace Laba_3.Services
{
	public interface IUserService
	{
		IProfile Load();
		void Save(IProfile profile);
	}
}
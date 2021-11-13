using Laba_3.Models;
using System.Threading.Tasks;

namespace Laba_3.Services
{
	public interface IUserRegisterService
	{
		Task<bool> CreateUserWithEmailAndPasswordAsync(IProfile Profile);
		Task<bool> SignInWithEmailAndPasswordAsync(IProfile profile);
	}
}
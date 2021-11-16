using Laba_4.DataCore;

namespace Laba_4.Services
{
	public interface IContainer
	{
		IRepository GetRepository();
		INavigationService GetNavigationService();
	}
}
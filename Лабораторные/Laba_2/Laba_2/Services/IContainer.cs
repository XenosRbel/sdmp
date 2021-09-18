using Microsoft.Extensions.DependencyInjection;

namespace Laba_2.Services
{
	public interface IContainer
	{
		IConfig Config { get; }
		ServiceProvider ServiceProvider { get; }
	}
}
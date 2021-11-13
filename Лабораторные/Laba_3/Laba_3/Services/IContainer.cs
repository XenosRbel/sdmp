using System;

namespace Laba_3.Services
{
	public interface IContainer
	{
		IConfig Config { get; }
		IServiceProvider ServiceProvider { get; }
	}
}

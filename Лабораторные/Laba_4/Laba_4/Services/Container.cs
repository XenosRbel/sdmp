using System;
using Laba_4.DataCore;
using Xamarin.Forms;

namespace Laba_4.Services
{
	public class Container : IContainer
	{
		private readonly Application _application;
		private readonly IRepository _repositoryLazy;
		private readonly INavigationService _navigationServiceLazy;

		public Container(Application application)
		{
			//var liteDbRepositoryFactory = new LiteDbRepositoryFactory("contact.db");
			//var repository = new LiteDbRepository(liteDbRepositoryFactory.GetRepository());
			var repository = new MemoryRepository();

			_application = application;
			_repositoryLazy = repository;
			_navigationServiceLazy = new NavigationService(_application, this);
		}

		public IRepository GetRepository()
		{
			return _repositoryLazy;
		}

		public INavigationService GetNavigationService()
		{
			return _navigationServiceLazy;
		}
	}
}
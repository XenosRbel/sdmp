using Laba_2.Models;
using System.Collections.Generic;

namespace Laba_2.Services.Repositories
{
	public interface IRepository
	{
		event MemoryRepository.RepositoryMethodContainer InsertCompleted;

		T GetById<T>(long id) where T : class, IBaseEntity;
		IEnumerable<T> GetAll<T>() where T : class, IBaseEntity;
		void Add<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
		void Add<T>(T entity) where T : class, IBaseEntity;
		void Save();
	}
}

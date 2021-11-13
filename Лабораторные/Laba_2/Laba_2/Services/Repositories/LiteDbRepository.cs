using Laba_2.Models;
using Laba_2.Services.Repositories;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_2.Services.Repositories
{
	public class LiteDbRepository : IRepository
	{
		private readonly LiteRepository _liteRepository;

		public LiteDbRepository(LiteRepository liteRepository)
		{
			_liteRepository = liteRepository ?? throw new ArgumentNullException(nameof(liteRepository));
		}

		public T GetById<T>(long id) where T : class, IBaseEntity
		{			
			return _liteRepository.First<T>(item => item.Id == id);
		}

		public IEnumerable<T> GetAll<T>() where T : class, IBaseEntity
		{
			return _liteRepository.Query<T>().ToEnumerable();
		}

		public void Add<T>(IEnumerable<T> entities) where T : class, IBaseEntity
		{
			_liteRepository.Insert<T>(entities);
		}

		public void Add<T>(T entity) where T : class, IBaseEntity
		{
			_liteRepository.Insert<T>(entity);
		}

		public void Save()
		{
			
		}

		public IList<T> Load<T>() where T : class, IBaseEntity
		{
			return GetAll<T>().ToList();
		}

		public event MemoryRepository.RepositoryMethodContainer InsertCompleted;
	}
}

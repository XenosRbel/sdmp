using Laba_1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba_1.Services
{
	public class MemoryRepository : IRepository
	{
		private readonly IList<object> _dbList;

		public MemoryRepository()
		{
			_dbList = new List<object>();
		}

		public Task AddAsync<T>(T entity) where T : class, IBaseEntity
		{
			return Task.Run(() =>
			{
				entity.Id = _dbList.Count;
				_dbList.Add(entity);

				return true;
			});
		}

		public Task AddAllAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity
		{
			RemoveAllAsync<T>();

			return Task.Run(() =>
			{
				entities = AddIndexToEntities<T>(entities);

				foreach (var entity in entities)
				{
					_dbList.Add(entity);
				}

				return true;
			});
		}

		public async Task<IList<T>> GetAllAsync<T>() where T : class, IBaseEntity
		{
			return await Task.Run(() => _dbList.OfType<T>().ToList());
		}

		public Task RemoveAllAsync<T>() where T : class, IBaseEntity
		{
			return Task.Run(() =>
			{
				_dbList.Clear();
				return true;
			});
		}

		private IEnumerable<T> AddIndexToEntities<T>(IEnumerable<T> entities) where T : class, IBaseEntity
		{
			var list = entities.ToList();

			for (int i = 0; i < list.Count; i++)
			{
				var item = list[i];
				item.Id = i;
			}

			return list;
		}
	}
}

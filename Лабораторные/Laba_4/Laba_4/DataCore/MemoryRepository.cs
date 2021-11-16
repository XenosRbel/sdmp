using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laba_4.Models;

namespace Laba_4.DataCore
{
	public class MemoryRepository : IRepository
	{
		private readonly IList<object> _dbList;

		public MemoryRepository()
		{
			_dbList = new List<object>();
		}

		public Task AddOrUpdateAllAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity
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
			var list= entities.ToList();

			for (int i = 0; i < list.Count; i++)
			{
				var item = list[i];
				item.Id = i;
			}

			return list;
		}
	}
}
using Laba_1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Laba_1.Services
{
	public interface IRepository
	{
		Task AddAsync<T>(T entity) where T : class, IBaseEntity;
		Task AddAllAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
		Task<IList<T>> GetAllAsync<T>() where T : class, IBaseEntity;
		Task RemoveAllAsync<T>() where T : class, IBaseEntity;
	}
}

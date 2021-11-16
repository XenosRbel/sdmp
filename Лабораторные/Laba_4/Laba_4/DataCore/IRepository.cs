using System.Collections.Generic;
using System.Threading.Tasks;
using Laba_4.Models; 

namespace Laba_4.DataCore
{
	public interface IRepository
	{
		Task AddOrUpdateAllAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
		Task<IList<T>> GetAllAsync<T>() where T : class, IBaseEntity;
		Task RemoveAllAsync<T>() where T : class, IBaseEntity;
	}
}

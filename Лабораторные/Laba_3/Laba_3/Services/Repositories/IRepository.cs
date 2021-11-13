using Laba_3.Models;
using System.Collections.Generic;

namespace Laba_3.Services.Repositories
{
	public interface IRepository
	{
		T GetById<T>(long id) where T : class, IBaseEntity;
		IEnumerable<T> GetAll<T>() where T : class, IBaseEntity;
		void Add<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
		void Add<T>(T entity) where T : class, IBaseEntity;
		void Save();
		IList<T> Load<T>() where T : class, IBaseEntity;
	}
}

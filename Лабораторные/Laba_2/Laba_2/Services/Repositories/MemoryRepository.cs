using Laba_2.Models;
using Laba_2.Services.Network.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Laba_2.Services.Repositories
{
	public class MemoryRepository : IRepository
	{
		private readonly List<object> _dbList;
		private readonly string FILE_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db_sdmp_laba2.json");

		#region Events

		public delegate void RepositoryMethodContainer(object sender);
		public event RepositoryMethodContainer InsertCompleted;

		#endregion

		public MemoryRepository()
		{
			_dbList = new List<object>();
		}

		public T GetById<T>(long id) where T : class, IBaseEntity
		{
			return _dbList.OfType<T>().ToList().Find(item => item.Id == id);
		}

		public IEnumerable<T> GetAll<T>() where T : class, IBaseEntity
		{
			return _dbList.OfType<T>().ToList();
		}

		public void Add<T>(IEnumerable<T> entities) where T : class, IBaseEntity
		{
			entities = AddIndexToEntities(entities);

			_dbList.AddRange(entities);

			Save();
			OnInsertCompleted();
		}

		public void Add<T>(T entity) where T : class, IBaseEntity
		{
			entity = AddIndexToEntity(entity);

			_dbList.Add(entity);

			Save();
			OnInsertCompleted();
		}

		public void Save()
		{
			var jsonString = JsonConvert.SerializeObject(_dbList, Converter.Settings);
			File.WriteAllText(FILE_PATH, jsonString);
		}

		public IList<T> Load<T>() where T : class, IBaseEntity
		{
			var fileData = string.Empty;

			if (File.Exists(FILE_PATH))
			{
				fileData = File.ReadAllText(FILE_PATH);

				return JsonConvert.DeserializeObject<List<T>>(fileData, Converter.Settings);
			} else
			{
				return new List<T>();
			}
		}

		private IEnumerable<T> AddIndexToEntities<T>(IEnumerable<T> entities) where T : class, IBaseEntity
		{
			var items = entities.ToList();
			var currentIndex = _dbList.Count;

			foreach (var item in items)
			{
				item.Id = currentIndex++;
			}

			return items;
		}

		private T AddIndexToEntity<T>(T entity) where T : class, IBaseEntity
		{
			var currentIndex = _dbList.Count;

			entity.Id = currentIndex++;

			return entity;
		}


		#region protected

		protected virtual void OnInsertCompleted()
		{
			InsertCompleted?.Invoke(this);
		}

		#endregion
	}
}

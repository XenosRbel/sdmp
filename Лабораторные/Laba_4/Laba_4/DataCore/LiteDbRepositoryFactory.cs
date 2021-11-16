using System;
using System.IO;
using System.Threading.Tasks;
using LiteDB;
using Xamarin.Essentials;

namespace Laba_4.DataCore
{
	public class LiteDbRepositoryFactory
	{
		private const string DbDir = "DataStorage";
		private readonly string _dbFilePath;

		public LiteDbRepositoryFactory(string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException(nameof(fileName));

			var dirPath = Path.Combine(FileSystem.CacheDirectory, DbDir);
			Directory.CreateDirectory(dirPath);
			_dbFilePath = Path.Combine(dirPath, fileName);
		}

		public LiteRepository GetRepository()
		{
			return new LiteRepository(_dbFilePath);
		}
	}
}
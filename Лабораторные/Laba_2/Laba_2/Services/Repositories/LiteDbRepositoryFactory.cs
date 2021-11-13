using LiteDB;
using System;
using System.IO;

namespace Laba_2.Services.Repositories
{
	public class LiteDbRepositoryFactory
	{
		private const string DbDir = "DataStorage";
		private readonly string _dbFilePath;

		public LiteDbRepositoryFactory(string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException(nameof(fileName));

			var dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DbDir);
			Directory.CreateDirectory(dirPath);
			_dbFilePath = Path.Combine(dirPath, fileName);
		}

		public LiteRepository GetRepository()
		{
			return new LiteRepository(_dbFilePath);
		}
	}
}

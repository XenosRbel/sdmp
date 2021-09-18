using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace Laba_2.Services
{
	internal class ApplicationConfig
	{
		public static IConfig Configuration { get; }

		private const string ConfigFilename = "Configs.json";

		static ApplicationConfig()
		{
			try
			{
				var assembly = typeof(ApplicationConfig).GetTypeInfo().Assembly;

				var pathToFile = GetResourcePath(assembly);

				using (var stream = assembly.GetManifestResourceStream(pathToFile))
				using (var reader = new StreamReader(stream))
				{
					Configuration = JsonConvert.DeserializeObject<Config>(reader.ReadToEnd());
				}
			}
			catch
			{
				throw new ArgumentNullException(nameof(ApplicationConfig));
			}
		}

		private static string GetResourcePath(Assembly assembly)
		{
			var assemblyPrefix = assembly.GetName().Name;
			return $"{assemblyPrefix}.{ConfigFilename}";
		}
	}
}

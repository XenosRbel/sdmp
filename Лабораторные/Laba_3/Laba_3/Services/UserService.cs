using Laba_3.Models;
using Laba_3.Services.Helpers;
using Newtonsoft.Json;
using System.IO;
using Xamarin.Essentials;

namespace Laba_3.Services
{
	internal class UserService : IUserService
	{
		private readonly string PROFILE_FOLDER = Path.Combine(FileSystem.CacheDirectory, "Profile");
		private const string PROFILE_FILE_NAME = "profile.json";

		public UserService()
		{
			Directory.CreateDirectory(PROFILE_FOLDER);

			if (!File.Exists(Path.Combine(PROFILE_FOLDER, PROFILE_FILE_NAME)))
			{
				File.Create(Path.Combine(PROFILE_FOLDER, PROFILE_FILE_NAME)).Close();
			}
		}

		public void Save(IProfile profile)
		{
			var jsonString = JsonConvert.SerializeObject(profile);
			File.WriteAllText(Path.Combine(PROFILE_FOLDER, PROFILE_FILE_NAME), jsonString);
		}

		public IProfile Load()
		{
			var fileData = File.ReadAllText(Path.Combine(PROFILE_FOLDER, PROFILE_FILE_NAME));

			if (string.IsNullOrWhiteSpace(fileData))
			{
				return new Profile
				{
					Name = new ObservableObject<string> { Property = string.Empty },
					Email = new ObservableObject<string> { Property = string.Empty }
				};
			}
			else
			{
				return JsonConvert.DeserializeObject<Profile>(fileData);
			}
		}
	}
}

using Laba_2.Models;
using Laba_2.Services.Helpers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Laba_2.ViewModels
{
	internal class ProfileViewModel : BaseViewModel
	{
		private readonly string PROFILE_FOLDER = Path.Combine(FileSystem.CacheDirectory, "Profile");
		private const string PROFILE_FILE_NAME = "profile.json";

		public ObservableObject<Profile> Profile { get; set; }
		public ICommand PickAvatarCommand { get; set; }
		public ICommand SaveProfileCommand { get; set; }

		public ProfileViewModel()
		{
			PickAvatarCommand = new Command(() => OnPickAvatarCommand());
			SaveProfileCommand = new Command(() => OnSaveProfileCommand());

			Directory.CreateDirectory(PROFILE_FOLDER);

			if (!File.Exists(Path.Combine(PROFILE_FOLDER, PROFILE_FILE_NAME)))
			{
				File.Create(Path.Combine(PROFILE_FOLDER, PROFILE_FILE_NAME)).Close();
			}

			Profile = LoadProfile();
		}

		protected void OnSaveProfileCommand()
		{
			var jsonString = JsonConvert.SerializeObject(Profile);
			File.WriteAllText(Path.Combine(PROFILE_FOLDER, PROFILE_FILE_NAME), jsonString);
		}

		protected ObservableObject<Profile> LoadProfile()
		{
			var fileData = File.ReadAllText(Path.Combine(PROFILE_FOLDER, PROFILE_FILE_NAME));

			if (string.IsNullOrWhiteSpace(fileData))
			{
				return new ObservableObject<Profile>
				{
					Property = new Profile
					{
						AvatarUrl = new ObservableObject<string> { Property = string.Empty },
						FirstName = new ObservableObject<string> { Property = string.Empty },
						LastName = new ObservableObject<string> { Property = string.Empty },
						PhoneNumber = new ObservableObject<string> { Property = string.Empty },
						DateOfBirth = new ObservableObject<DateTime> { Property = DateTime.Now.Date }
					}
				};
			}
			else
			{
				return JsonConvert.DeserializeObject<ObservableObject<Profile>>(fileData);
			}

		}

		protected async void OnPickAvatarCommand()
		{
			await PickAndShow(PickOptions.Images);
		}

		protected async Task<FileResult> PickAndShow(PickOptions options)
		{
			try
			{
				var result = await FilePicker.PickAsync(options);
				if (result != null)
				{
					if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
						result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
					{
						var stream = await result.OpenReadAsync();
						var filePath = SaveFileFromStream(result.FileName, stream);

						Profile.Property.AvatarUrl.Property = filePath;
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				// The user canceled or something went wrong
			}

			return null;
		}

		protected string SaveFileFromStream(string fileName, Stream stream)
		{
			var filePath = Path.Combine(PROFILE_FOLDER, fileName);
			using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				stream.CopyTo(fileStream);
			}

			return filePath;
		}
	}
}

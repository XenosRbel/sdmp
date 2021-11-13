using System.ComponentModel;

namespace Laba_3.Services.Helpers
{
	public interface IObservableObject<T>
	{
		T Property { get; set; }

		event PropertyChangedEventHandler PropertyChanged;

		void OnPropertyChanged();
	}
}

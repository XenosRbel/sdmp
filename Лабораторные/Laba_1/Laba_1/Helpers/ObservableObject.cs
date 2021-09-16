using System.ComponentModel;

namespace Laba_1.Helpers
{
	public class ObservableObject<T> : INotifyPropertyChanged
    {
        private T _property;

        public T Property
        {
            get => _property;
            set
            {
                if (_property != null && _property.Equals(value)) return;

                _property = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Property"));
        }
    }
}

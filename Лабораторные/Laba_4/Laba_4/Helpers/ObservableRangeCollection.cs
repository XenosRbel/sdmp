using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Laba_4.Helpers
{
	public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        public ObservableRangeCollection() : base()
        {
        }

        public ObservableRangeCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            CheckReentrancy();

            int startIndex = Count;
            var changedItems = collection is List<T> ? (List<T>)collection : new List<T>(collection);
            foreach (var i in changedItems)
                Items.Add(i);

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));
        }
    }
}

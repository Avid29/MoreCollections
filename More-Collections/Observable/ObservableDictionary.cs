using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace MoreCollections.Observable
{
    /// <summary>
    /// Represents a collection of key/value pairs that provides notifcations when updated
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the Fictionary</typeparam>
    /// <typeparam name="TValue">The type of the values in the Dictionary</typeparam>
    public class ObservableDictionary<TKey, TValue> :
        ICollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>,
        IEnumerable<TValue>,
        INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly SynchronizationContext _context;
        private ConcurrentDictionary<TKey, TValue> _dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class that
        /// contains elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        public ObservableDictionary(ICollection<KeyValuePair<TKey, TValue>> collection)
        {
            _context = SynchronizationContext.Current;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(collection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class that is empty and has the default initial capacity.
        /// </summary>
        public ObservableDictionary()
        {
            _context = SynchronizationContext.Current;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(new List<KeyValuePair<TKey, TValue>>());
        }

        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets an <see cref="ICollection{TKey}"/> containing the keys of the <see cref="ObservableDictionary{TKey, TValue}"/>.
        /// </summary>
        public ICollection<TKey> Keys => _dictionary.Keys;

        /// <summary>
        /// Gets an <see cref="ICollection{TValue}"/> containing the values in the <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        public ICollection<TValue> Values => _dictionary.Values;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        public int Count => _dictionary.Count;

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns>The element with the specified key.</returns>
        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set => UpdateWithNotification(key, value);
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(TKey key, TValue value)
        {
            TryAddWithNotification(key, value);
        }

        /// <summary>
        /// Determines whether the <see cref="ObservableDictionary{TKey, TValue}"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="ObservableDictionary{TKey, TValue}"/></param>
        /// <returns> true if the <see cref="ObservableDictionary{TKey, TValue}"/> contains an element with the key; otherwise, false.</returns>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.
        /// This method also returns false if key was not found in the original <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </returns>
        public bool Remove(TKey key)
        {
            TValue temp;
            return TryRemoveWithNotification(key, out temp);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key, if the
        /// key is found; otherwise, the default value for the type of the value parameter.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if the object that implements <see cref="ObservableDictionary{TKey, TValue}"/> contains
        /// an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Removes all items from the ObservableHashedCollection
        /// </summary>
        public void Clear()
        {
            ClearWithNotification();
        }

        private List<string> propertiesToUpdate = new List<string> { "Count", "Keys", "Values" };

        /// <summary>
        /// Implements update of bound properties
        /// </summary>
        protected virtual void NotifyObserversOfChange()
        {
            var collectionHandler = CollectionChanged;
            var propertyHandler = PropertyChanged;
            if (collectionHandler != null || propertyHandler != null)
            {
                _context.Post(
                    s =>
                    {
                        collectionHandler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        if (propertyHandler != null)
                        {
                            foreach (string property in propertiesToUpdate)
                            {
                                propertyHandler(this, new PropertyChangedEventArgs(property));
                            }
                        }
                    }, null);
            }
        }

        private bool TryAddWithNotification(KeyValuePair<TKey, TValue> item)
        {
            return TryAddWithNotification(item.Key, item.Value);
        }

        private bool TryAddWithNotification(TKey key, TValue value)
        {
            bool result = _dictionary.TryAdd(key, value);
            if (result)
            {
                NotifyObserversOfChange();
            }

            return result;
        }

        private bool TryRemoveWithNotification(TKey key, out TValue value)
        {
            bool result = _dictionary.TryRemove(key, out value);
            if (result)
            {
                NotifyObserversOfChange();
            }

            return result;
        }

        private void UpdateWithNotification(TKey key, TValue value)
        {
            _dictionary[key] = value;
            NotifyObserversOfChange();
        }

        private void ClearWithNotification()
        {
            _dictionary.Clear();
            NotifyObserversOfChange();
        }

        /// <summary>
        /// Adds an item to the <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ObservableDictionary{TKey, TValue}"/></param>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            TryAddWithNotification(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            TValue temp;
            return TryRemoveWithNotification(item.Key, out temp);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator GetEnumerator()
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).GetEnumerator();
        }
    }
}
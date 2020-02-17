namespace MoreCollections.Generic
{
    /// <summary>
    /// Represents a strongly typed DoubleEndedQueue of objects
    /// </summary>
    /// <typeparam name="T">The type of elements in the Deque</typeparam>
    public class Deque<T>
    {
        private const int _DefaultCapacity = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deque{T}"/> class
        /// </summary>
        /// <param name="capacity">Initial capacity of the <see cref="Deque{T}"/></param>
        public Deque(int capacity = _DefaultCapacity)
        {
            _shardings = new T[1][];
            _shardings[0] = new T[capacity];
            _frontIndex = _shardings[0].Length;
            _backIndex = -1;
        }

        /// <summary>
        /// Adds an object to the Front of the <see cref="Deque{T}"/>
        /// </summary>
        /// <param name="value">The object to be added to the front of the <see cref="Deque{T}"/></param>
        public void PushFront(T value)
        {
            CheckThenReserveInFront();
            _shardings[0][_frontIndex - 1] = value;
            _frontIndex--;
            Count++;

            if (_backIndex == -1)
            {
                _backIndex = _frontIndex;
            }
        }

        /// <summary>
        /// Adds an object to the back of the <see cref="Deque{T}"/>
        /// </summary>
        /// <param name="value">The object to be added to the back of the <see cref="Deque{T}"/></param>
        public void PushBack(T value)
        {
            CheckThenReserveInBack();
            _shardings[_shardings.Length - 1][_backIndex + 1] = value;
            _backIndex++;
            Count++;

            if (_frontIndex == _shardings[0].Length)
            {
                _frontIndex = _backIndex;
            }
        }

        /// <summary>
        /// Removes and returns the object at the front of the <see cref="Deque{T}"/>
        /// </summary>
        /// <returns>The object that is removed from the front of the <see cref="Deque{T}"/></returns>
        public T PopFront()
        {
            T value = PeekFront();
            _shardings[0][_frontIndex] = default;
            _frontIndex++;
            Count--;
            CheckThenUnreserveInFront();
            return value;
        }

        /// <summary>
        /// Removes and returns the object at the back of the <see cref="Deque{T}"/>
        /// </summary>
        /// <returns>The object that is removed from the back of the <see cref="Deque{T}"/></returns>
        public T PopBack()
        {
            T value = PeekBack();
            _shardings[_shardings.Length - 1][_backIndex] = default;
            _backIndex--;
            Count--;
            CheckThenUnreserveInBack();
            return value;
        }

        /// <summary>
        /// Gets the value from the front of the <see cref="Deque{T}"/>
        /// </summary>
        /// <returns>The frontmost value in the <see cref="Deque{T}"/></returns>
        public T PeekFront()
        {
            // TODO: Check OutOfIndex
            return _shardings[0][_frontIndex];
        }

        /// <summary>
        /// Gets the value from the back of the <see cref="Deque{T}"/>
        /// </summary>
        /// <returns>The backmost value in the <see cref="Deque{T}"/></returns>
        public T PeekBack()
        {
            // TODO: Check OutOfIndex
            return _shardings[_shardings.Length - 1][_backIndex];
        }

        private void CheckThenReserveInFront()
        {
            if (_frontIndex != 0)
            {
                // No need to increase capacity
                return;
            }

            // Makes a new shards array
            T[][] newShards = new T[_shardings.Length + 1][];
            _shardings.CopyTo(newShards, 1);
            newShards[0] = new T[Count]; // Doubles capacity
            _shardings = newShards;

            // Reset _FrontIndex
            _frontIndex = _shardings[0].Length;
        }

        private void CheckThenReserveInBack()
        {
            if (_backIndex + 1 != _shardings[_shardings.Length - 1].Length)
            {
                // No need to increase capacity
                return;
            }

            T[][] newShards = new T[_shardings.Length + 1][];
            _shardings.CopyTo(newShards, 0);
            newShards[newShards.Length - 1] = new T[Capacity]; // Doubles capacity
            _shardings = newShards;

            // Reset back index
            _backIndex = -1;
        }

        private void CheckThenUnreserveInFront()
        {
            if (_shardings.Length == 1 || _frontIndex != _shardings[0].Length)
            {
                // No need or no room to remove shard
                return;
            }

            T[][] newShards = new T[_shardings.Length - 1][];
            for (int i = 1; i < _shardings.Length; i++)
            {
                newShards[i - 1] = _shardings[i];
            }

            _shardings = newShards;
            _frontIndex = 0;
        }

        private void CheckThenUnreserveInBack()
        {
            if (_shardings.Length == 1 || _backIndex != -1)
            {
                // No need or no room to remove shard
                return;
            }

            T[][] newShards = new T[_shardings.Length - 1][];
            for (int i = 0; i < newShards.Length; i++)
            {
                newShards[i] = _shardings[i];
            }
            _shardings = newShards;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="Deque{T}"/>
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the total number of elements the internal data structure can hold without resizing.
        /// </summary>
        public int Capacity
        {
            get
            {
                int capacity = 0;
                foreach (T[] shard in _shardings)
                {
                    capacity += shard.Length;
                }

                return capacity;
            }
        }

        private T[][] _shardings;

        private int _frontIndex;

        private int _backIndex;
    }
}

using System;

namespace MoreCollections.Generic
{
    /// <summary>
    /// Represents a strongly typed <see cref="Deque{T}"/> of objects
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="Deque{T}"/></typeparam>
    public class Deque<T>
    {
        private const int _DefaultChucnkSize = 8;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deque{T}"/> class.
        /// </summary>
        /// <param name="capacity">Initial capacity of the <see cref="Deque{T}"/></param>
        public Deque(int capacity = _DefaultChucnkSize)
        {
            map = new T[3][];
            map[1] = new T[capacity];
            chunkSize = capacity;
            frontInternalIndex = capacity / 2;
            backInternalIndex = frontInternalIndex - 1;
            frontInternalChunkIndex = -1;
            backInternalChunkIndex = 1;
        }

        /// <summary>
        /// Gets or sets the value at <paramref name="index"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Value at <paramref name="index"/> in <see cref="Deque{T}"/></returns>
        public T this[int index]
        {
            get
            {
                (int, int) indexes = GetRealIndexesFromExternal(index);
                return map[indexes.Item1][indexes.Item2];
            }
            set
            {
                (int, int) indexes = GetRealIndexesFromExternal(index);
                map[indexes.Item1][indexes.Item2] = value;
            }
        }

        /// <summary>
        /// Adds an object to the Front of the <see cref="Deque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the front of the <see cref="Deque{T}"/>.</param>
        public void PushFront(T value)
        {
            frontInternalIndex--;
            CheckAndReserveFront();
            this[0] = value;
        }

        /// <summary>
        /// Adds an object to the back of the <see cref="Deque{T}"/>
        /// </summary>
        /// <param name="value">The object to be added to the back of the <see cref="Deque{T}"/></param>
        public void PushBack(T value)
        {
            backInternalIndex++;
            CheckAndReserveBack();
            this[Count - 1] = value;
        }

        /// <summary>
        /// Removes and returns the object at the front of the <see cref="Deque{T}"/>
        /// </summary>
        /// <returns>The object that is removed from the front of the <see cref="Deque{T}"/></returns>
        public T PopFront()
        {
            T value = this[0];
            this[0] = default(T);
            frontInternalIndex++;
            return value;
        }

        /// <summary>
        /// Removes and returns the object at the back of the <see cref="Deque{T}"/>
        /// </summary>
        /// <returns>The object that is removed from the back of the <see cref="Deque{T}"/></returns>
        public T PopBack()
        {
            T value = this[Count - 1];
            this[Count - 1] = default(T);
            backInternalIndex--;
            return value;
        }

        /// <summary>
        /// Gets the value from the front of the <see cref="Deque{T}"/>
        /// </summary>
        /// <returns>The frontmost value in the <see cref="Deque{T}"/></returns>
        public T PeekFront()
        {
            return this[0];
        }

        /// <summary>
        /// Gets the value from the back of the <see cref="Deque{T}"/>
        /// </summary>
        /// <returns>The backmost value in the <see cref="Deque{T}"/></returns>
        public T PeekBack()
        {
            return this[Count - 1];
        }

        private void CheckAndReserveFront()
        {
            if (frontInternalIndex < firstReservedInternalIndex)
            {
                int additionalChunks = frontInternalChunkIndex * frontInternalChunkIndex;
                T[][] newMap = new T[map.Length + additionalChunks][];
                map.CopyTo(newMap, additionalChunks);
                newMap[additionalChunks] = new T[chunkSize];
                map = newMap;
                frontInternalChunkIndex -= additionalChunks;
            }
            int realChunk = GetRealIndexesFromInternal(frontInternalIndex).Item1;
            if (map[realChunk] == null)
            {
                map[realChunk] = new T[chunkSize];
            }
        }

        private void CheckAndReserveBack()
        {
            if (backInternalIndex >= lastReservedInternalIndex)
            {
                int additionalChunks = backInternalChunkIndex * backInternalChunkIndex;
                T[][] newMap = new T[map.Length + additionalChunks][];
                map.CopyTo(newMap, 0);
                newMap[backInternalChunkIndex + 1] = new T[chunkSize];
                map = newMap;
                backInternalChunkIndex++;
            }
            int realChunk = GetRealIndexesFromInternal(backInternalIndex).Item1;
            if (map[realChunk] == null)
            {
                map[realChunk] = new T[chunkSize];
            }
        }

        private (int, int) GetRealIndexesFromExternal(int externalIndex)
        {
            if (externalIndex >= Count || externalIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return GetRealIndexesFromInternal(frontInternalIndex + externalIndex);
        }

        private (int, int) GetRealIndexesFromInternal(int internalIndex)
        {
            int internalChunk;
            if (internalIndex < 0)
            {
                internalChunk = ((internalIndex + 1) / chunkSize) - 1;
            }
            else
            {
                internalChunk = internalIndex / chunkSize;
            }

            int chunkOffset = internalIndex % chunkSize;
            if (chunkOffset < 0)
            {
                chunkOffset += chunkSize;
            }

            int realChunk = internalChunk - frontInternalChunkIndex;
            return (realChunk, chunkOffset);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="Deque{T}"/>
        /// </summary>
        public int Count => (backInternalIndex - frontInternalIndex) + 1;

        /// <summary>
        /// Gets the total number of elements the internal data structure can hold without resizing.
        /// </summary>
        public int Capacity
        {
            get
            {
                int capacity = 0;
                foreach (T[] shard in map)
                {
                    capacity += shard.Length;
                }

                return capacity;
            }
        }

        private T[][] map;

        private int frontInternalIndex;
        private int backInternalIndex;

        private int frontInternalChunkIndex;
        private int backInternalChunkIndex;

        /// <summary>
        /// Minimum number of items in a shard.
        /// </summary>
        private int chunkSize;

        /// <summary>
        /// Gets last reserved index using internal indexing system.
        /// </summary>
        private int firstReservedInternalIndex => frontInternalChunkIndex * chunkSize;

        /// <summary>
        /// Gets first reserved index using internal indexing system.
        /// </summary>
        private int lastReservedInternalIndex => (backInternalChunkIndex + 1) * chunkSize - 1;
    }
}

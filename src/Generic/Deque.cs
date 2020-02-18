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
            shardings = new T[1][];
            shardings[0] = new T[capacity];
            chunkSize = capacity;
            frontInternalIndex = capacity / 2;
            backInternalIndex = frontInternalIndex - 1;
            shardingOffset = 0;
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
                return shardings[indexes.Item1][indexes.Item2];
            }
            set
            {
                (int, int) indexes = GetRealIndexesFromExternal(index);
                shardings[indexes.Item1][indexes.Item2] = value;
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
            backInternalIndex--;
            return value;
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
            int chunkOffset = (int)IntAbs(internalIndex % chunkSize);
            int chunk = internalIndex / chunkSize;
            if (internalIndex < 0)
            {
                chunk--;
            }

            int internalShardIndex;
            if (chunk == -1)
            {
                internalShardIndex = 0; // Work around for Log2(0) bug
            }
            else
            {
                internalShardIndex = (int)Math.Log(IntAbs(chunk + 1), 2); // TODO: integer Log2
            }

            if (internalIndex < 0)
            {
                internalShardIndex--;
            }

            int realShard = internalShardIndex + shardingOffset;
            int realShardOffset;
            if (realShard == 0)
            {
                realShardOffset = chunkOffset;
            }
            else
            {
                int outOfShardChunks = IntPow2(IntAbs(internalShardIndex)) - 1;
                realShardOffset = ((chunk - outOfShardChunks) * chunkSize) + chunkOffset;
            }
            return (realShard, realShardOffset);
        }

        private void CheckAndReserveFront()
        {
            if (firstReservedInternalIndex == frontInternalIndex)
            {
                shardingOffset++;
                T[][] newShardings = new T[shardings.Length + 1][];
                shardings.CopyTo(newShardings, 1);
                newShardings[0] = new T[IntPow2(IntAbs(shardingOffset)) * chunkSize];
                shardings = newShardings;
            }
        }

        private void CheckAndReserveBack()
        {
            if (lastReservedInternalIndex == backInternalIndex)
            {
                T[][] newShardings = new T[shardings.Length + 1][];
                shardings.CopyTo(newShardings, 0);
                newShardings[newShardings.Length - 1] = new T[IntPow2(IntAbs(newShardings.Length - 1 - shardingOffset)) * chunkSize];
                shardings = newShardings;
            }
        }

        private int IntPow2(uint exponent)
        {
            int log = 1;
            for (uint i = 0; i < exponent; i++)
            {
                log *= 2;
            }
            return log;
        }

        private uint IntAbs(int value)
        {
            if (value < 0)
            {
                return (uint)(value * -1);
            }
            else
            {
                return (uint)value;
            }
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
                foreach (T[] shard in shardings)
                {
                    capacity += shard.Length;
                }

                return capacity;
            }
        }

        private T[][] shardings;

        private int frontInternalIndex;
        private int backInternalIndex;

        /// <summary>
        /// Minimum number of items in a shard.
        /// </summary>
        private int chunkSize;

        /// <summary>
        /// Number of negative shards in <see cref="shardings"/> array.
        /// </summary>
        private int shardingOffset;

        /// <summary>
        /// Gets last reserved index using internal indexing system.
        /// </summary>
        private int firstReservedInternalIndex => (IntPow2(IntAbs(shardingOffset)) - 1) * 2;

        /// <summary>
        /// Gets first reserved index using internal indexing system.
        /// </summary>
        private int lastReservedInternalIndex => (IntPow2(IntAbs(((shardings.Length) - shardingOffset))) - 1) * 2;
    }
}

// Copyright (c) MoreCollections. All rights reserved.

using MoreCollections.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreCollections.Generic
{
    /// <summary>
    /// An <see cref="IDeque{T}"/> implementation that has dynamic chunk size by distance from zero.
    /// </summary>
    /// <typeparam name="T">The type of items stored in the <see cref="DynamicDeque{T}"/>.</typeparam>
    public class DynamicDeque<T> : IDeque<T>
    {
        private const int _DefaultChucnkSize = 8;

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
        /// Initializes a new instance of the <see cref="DynamicDeque{T}"/> class.
        /// </summary>
        /// <param name="capacity">Initial capacity of the <see cref="DynamicDeque{T}"/>.</param>
        public DynamicDeque(int capacity = _DefaultChucnkSize)
        {
            shardings = new T[1][];
            shardings[0] = new T[capacity];
            chunkSize = capacity;
            frontInternalIndex = capacity / 2;
            backInternalIndex = frontInternalIndex - 1;
            shardingOffset = 0;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="DynamicDeque{T}"/>.
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

        /// <summary>
        /// Gets last reserved index using internal indexing system.
        /// </summary>
        private int FirstReservedInternalIndex => (IntPow2(IntAbs(shardingOffset)) - 1) * -2 * chunkSize;

        /// <summary>
        /// Gets first reserved index using internal indexing system.
        /// </summary>
        private int LastReservedInternalIndex => (IntPow2(IntAbs(shardings.Length - shardingOffset)) - 1) * chunkSize;

        /// <summary>
        /// Gets or sets the value at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index to get or set in the array.</param>
        /// <returns>Value at <paramref name="index"/> in <see cref="Deque{T}"/>.</returns>
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
        /// Adds an object to the back of the <see cref="DynamicDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the back of the <see cref="DynamicDeque{T}"/>.</param>
        public void PushBack(T value)
        {
            backInternalIndex++;
            CheckAndReserveBack();
            this[Count - 1] = value;
        }

        /// <summary>
        /// Removes and returns the object at the front of the <see cref="DynamicDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the front of the <see cref="DynamicDeque{T}"/>.</returns>
        public T PopFront()
        {
            T value = this[0];
            this[0] = default(T);
            frontInternalIndex++;
            return value;
        }

        /// <summary>
        /// Removes and returns the object at the back of the <see cref="Deque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the back of the <see cref="Deque{T}"/>.</returns>
        public T PopBack()
        {
            T value = this[Count - 1];
            this[Count - 1] = default(T);
            backInternalIndex--;
            CheckAndUnreserveBack();
            return value;
        }

        /// <summary>
        /// Gets the value from the front of the <see cref="Deque{T}"/>.
        /// </summary>
        /// <returns>The frontmost value in the <see cref="Deque{T}"/>.</returns>
        public T PeekFront()
        {
            return this[0];
        }

        /// <summary>
        /// Gets the value from the back of the <see cref="Deque{T}"/>.
        /// </summary>
        /// <returns>The backmost value in the <see cref="Deque{T}"/>.</returns>
        public T PeekBack()
        {
            return this[Count - 1];
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return new DequeEnum<T>(this);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
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
            int chunkOffset;
            if (internalIndex < 0)
            {
                chunkOffset = ((internalIndex + 1) % chunkSize) * -1;
            }
            else
            {
                chunkOffset = internalIndex % chunkSize;
            }

            int chunk = internalIndex / chunkSize;
            if (internalIndex < 0 && internalIndex % chunkSize != 0)
            {
                chunk--;
            }

            int internalShardIndex = (int)Math.Log(IntAbs(chunk) + 1, 2);
            if (internalIndex < 0)
            {
                internalShardIndex *= -1;
            }

            int realShard = internalShardIndex + shardingOffset;
            int realShardOffset;
            if (internalShardIndex == 0)
            {
                realShardOffset = chunkOffset;
            }
            else
            {
                int outOfShardChunks = IntPow2(IntAbs(internalShardIndex)) - 1;
                realShardOffset = (((int)IntAbs(chunk) - outOfShardChunks) * chunkSize) + chunkOffset;
            }

            return (realShard, realShardOffset);
        }

        private void CheckAndReserveFront()
        {
            if (FirstReservedInternalIndex == frontInternalIndex)
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
            if (LastReservedInternalIndex == backInternalIndex)
            {
                T[][] newShardings = new T[shardings.Length + 1][];
                shardings.CopyTo(newShardings, 0);
                newShardings[newShardings.Length - 1] = new T[IntPow2(IntAbs(newShardings.Length - 1 - shardingOffset)) * chunkSize];
                shardings = newShardings;
            }
        }

        private void CheckAndUnreserveBack()
        {
            // Checks if the last reserved shard is neccessary
            (int, int) reals = GetRealIndexesFromInternal(Count);
            if (reals.Item1 <= shardings.Length)
            {
                T[][] newShardings = new T[shardings.Length - 1][];
                for (int i = 0; i < newShardings.Length; i++)
                {
                    newShardings[i] = shardings[i];
                }

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
    }
}

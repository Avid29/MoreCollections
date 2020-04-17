// Copyright (c) MoreCollections. All rights reserved.

using MoreCollections.Generic;
using MoreCollections.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AllCollections.Generic
{
    /// <summary>
    /// An <see cref="IDeque{T}"/> implementation that has chunks to lower copy operations during reallocation.
    /// </summary>
    /// <remarks>
    /// A <see cref="IDeque{T}"/> that uses a 2D array to lower copy count on reallocation at the cost of extra overhead and more frequent allocations. The size of the chunks is constant.
    /// Items stored in <see cref="GridDeque{T}"/> have constant pointers because the items themselves aren't copied.
    /// </remarks>
    /// <typeparam name="T">The type of elements in the <see cref="GridDeque{T}"/>.</typeparam>
    public class GridDeque<T> : IDeque<T>
    {
        private const int _DefaultChunkSize = 128;

        private T[][] map;

        /// <summary>
        /// The real index of the 0th index.
        /// </summary>
        private int firstChunkIndex;

        /// <summary>
        /// index of the first item in the first chunk.
        /// </summary>
        private int firstRealIndex;

        /// <summary>
        /// The number of items in the <see cref="GridDeque{T}"/>.
        /// </summary>
        private int count;

        /// <summary>
        /// Minimum number of items in a shard.
        /// </summary>
        private int chunkSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridDeque{T}"/> class.
        /// </summary>
        public GridDeque() : this(_DefaultChunkSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridDeque{T}"/> class with an initial capcity of <paramref name="capacity"/>.
        /// </summary>
        /// <param name="capacity">Initial capacity of the <see cref="GridDeque{T}"/>.</param>
        public GridDeque(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity can not be less than 1.");
            }

            map = new T[3][];
            map[0] = new T[capacity];
            chunkSize = capacity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridDeque{T}"/> class that contains elements
        /// copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <param name="chunkSize">Amount of memory reserved at a time <see cref="GridDeque{T}"/>.</param>
        public GridDeque(IEnumerable<T> collection, int chunkSize = _DefaultChunkSize) : this(chunkSize)
        {
            foreach (var item in collection)
            {
                PushBack(item);
            }
        }

        /// <summary>
        /// Gets the number of items in the <see cref="GridDeque{T}"/>.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Gets the chunks size of the Deque.
        /// </summary>
        public int ChunkSize => chunkSize;

        /// <summary>
        /// Gets the number of chunks in use.
        /// </summary>
        private int ActiveChunkCount => (count / chunkSize) + 1;

        private int LastRealIndex => (count + firstRealIndex) % chunkSize;

        /// <summary>
        /// Gets or sets the value at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>Value at <paramref name="index"/> in <see cref="GridDeque{T}"/>.</returns>
        public T this[int index]
        {
            get
            {
                if (index > count)
                {
                    throw new IndexOutOfRangeException();
                }

                (int, int) indices = GetRealIndices(index);
                return map[indices.Item1][indices.Item2];
            }

            set
            {
                if (index > count)
                {
                    throw new IndexOutOfRangeException();
                }

                (int, int) indices = GetRealIndices(index);
                map[indices.Item1][indices.Item2] = value;
            }
        }

        /// <summary>
        /// Adds an object to the Front of the <see cref="GridDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the front of the <see cref="GridDeque{T}"/>.</param>
        public void PushFront(T value)
        {
            count++;
            CheckAndAllocateFront();

            firstRealIndex--;
            if (firstRealIndex < 0)
            {
                firstChunkIndex = (firstChunkIndex + map.Length - 1) % map.Length;
                map[firstChunkIndex] = new T[chunkSize];
                firstRealIndex = chunkSize - 1;
            }

            map[firstChunkIndex][firstRealIndex] = value;
        }

        /// <summary>
        /// Adds an object to the back of the <see cref="GridDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the back of the <see cref="GridDeque{T}"/>.</param>
        public void PushBack(T value)
        {
            count++;
            CheckAndAllocateBack();

            if (LastRealIndex == 0)
            {
                map[ActiveChunkCount + firstChunkIndex - 1] = new T[chunkSize];
            }

            this[count - 1] = value;
        }

        /// <summary>
        /// Removes and returns the object at the front of the <see cref="GridDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the front of the <see cref="GridDeque{T}"/>.</returns>
        public T PopFront()
        {
            T value = this[0];
            this[0] = default(T);
            count--;

            firstRealIndex++;
            if (firstRealIndex == chunkSize)
            {
                map[firstChunkIndex] = null;
                firstChunkIndex = (firstChunkIndex + 1) % map.Length;
                firstRealIndex = 0;
            }

            return value;
        }

        /// <summary>
        /// Removes and returns the object at the back of the <see cref="GridDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the back of the <see cref="GridDeque{T}"/>.</returns>
        public T PopBack()
        {
            T value = this[count - 1];
            this[count - 1] = default(T);
            count--;
            return value;
        }

        /// <summary>
        /// Gets the value from the front of the <see cref="GridDeque{T}"/>.
        /// </summary>
        /// <returns>The frontmost value in the <see cref="GridDeque{T}"/>.</returns>
        public T PeekFront()
        {
            return map[firstChunkIndex][firstRealIndex];
        }

        /// <summary>
        /// Gets the value from the back of the <see cref="GridDeque{T}"/>.
        /// </summary>
        /// <returns>The backmost value in the <see cref="GridDeque{T}"/>.</returns>
        public T PeekBack()
        {
            return this[count - 1];
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

        private (int, int) GetRealIndices(int index)
        {
            int internalChunk = index / chunkSize;
            int realChunk = (internalChunk + firstChunkIndex) % map.Length;
            int realIndex = (firstRealIndex + index) % chunkSize;
            return (realChunk, realIndex);
        }

        private int GetVirtualChunk(int chunk)
        {
            // TODO: Compare speeds of negative check vs remove.

            // Gets rid of negative by adding chunk map length
            chunk += firstChunkIndex + map.Length;

            return chunk % map.Length;
        }

        private void CheckAndAllocateFront()
        {
            if (firstRealIndex == 0 && map[GetVirtualChunk(-1)] != null)
            {
                Reallocate();
            }
        }

        private void CheckAndAllocateBack()
        {
            if (LastRealIndex == 0 && map[GetVirtualChunk(ActiveChunkCount)] != null)
            {
                Reallocate();
            }
        }

        /// <summary>
        /// Doubles allocation size and realligns the chunks.
        /// </summary>
        private void Reallocate()
        {
            // Creates a new map with double the chunks
            T[][] newMap = new T[map.Length * 2][];

            // Copies chunks
            // TODO: Try conditional front < back.
            int firstSegmentLength = map.Length - firstChunkIndex;
            Array.Copy(map, firstChunkIndex, newMap, 0, firstSegmentLength);
            Array.Copy(map, 0, newMap, firstSegmentLength, firstChunkIndex);

            // Adjusts instance to the new map
            map = newMap;
            firstChunkIndex = 0;
        }
    }
}

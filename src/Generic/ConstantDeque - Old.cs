// Copyright (c) MoreCollections. All rights reserved.

using MoreCollections.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreCollections.Generic
{
    /// <summary>
    /// Represents a strongly typed <see cref="ConstantDeque{T}"/> of objects.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ConstantDeque{T}"/>.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Testing")]
    public class ConstantDequeOld<T> : IDeque<T>
    {
        private const int _DefaultChunkSize = 128;

        private T[][] map;

        /// <summary>
        /// internal index of first item.
        /// </summary>
        private int frontInternalIndex;

        /// <summary>
        /// internal index of last item.
        /// </summary>
        private int backInternalIndex;

        /// <summary>
        /// Minimum number of items in a shard.
        /// </summary>
        private int chunkSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantDequeOld{T}"/> class.
        /// </summary>
        public ConstantDequeOld() : this(_DefaultChunkSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantDequeOld{T}"/> class with an initial capcity of <paramref name="capacity"/>.
        /// </summary>
        /// <param name="capacity">Initial capacity of the <see cref="ConstantDequeOld{T}"/>.</param>
        public ConstantDequeOld(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity can not be less than 1.");
            }

            map = new T[3][];
            map[1] = new T[capacity];
            chunkSize = capacity;
            frontInternalIndex = capacity / 2;
            backInternalIndex = frontInternalIndex - 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantDequeOld{T}"/> class that contains elements
        /// copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <param name="chunkSize">Amount of memory reserved at a time <see cref="ConstantDequeOld{T}"/>.</param>
        public ConstantDequeOld(IEnumerable<T> collection, int chunkSize = _DefaultChunkSize) : this(chunkSize)
        {
            foreach (var item in collection)
            {
                PushBack(item);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ConstantDeque{T}"/>.
        /// </summary>
        public int Count => (backInternalIndex - frontInternalIndex) + 1;

        /// <summary>
        /// Gets the total number of elements the internal data structure can hold without resizing.
        /// </summary>
        public int Capacity
        {
            get
            {
                int firstChunk = GetRealIndexesFromExternal(0).Item1;
                int lastChunk = GetRealIndexesFromExternal(Count - 1).Item1;
                return ((lastChunk - firstChunk) + 1) * chunkSize;
            }
        }

        /// <summary>
        /// Gets the internal chunk index of first item in the map.
        /// </summary>
        private int FrontInternalChunkIndex
        {
            get
            {
                int index = frontInternalIndex / chunkSize;
                if (frontInternalIndex < 0)
                {
                    index--;
                }

                return index;
            }
        }

        /// <summary>
        /// Gets the internal chunk index of last item in the map.
        /// </summary>
        private int BackInternalChunkIndex
        {
            get
            {
                int index = backInternalIndex / chunkSize;
                if (backInternalIndex < 0)
                {
                    index--;
                }

                return index;
            }
        }

        /// <summary>
        /// Gets last reserved index using internal indexing system.
        /// </summary>
        private int FirstReservedInternalIndex => FrontInternalChunkIndex * chunkSize;

        /// <summary>
        /// Gets first reserved index using internal indexing system.
        /// </summary>
        private int LastReservedInternalIndex => ((BackInternalChunkIndex + 1) * chunkSize) - 1;

        /// <summary>
        /// Gets or sets the value at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>Value at <paramref name="index"/> in <see cref="ConstantDeque{T}"/>.</returns>
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
        /// Adds an object to the Front of the <see cref="ConstantDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the front of the <see cref="ConstantDeque{T}"/>.</param>
        public void PushFront(T value)
        {
            frontInternalIndex--;
            CheckAndReserveFront();
            this[0] = value;
        }

        /// <summary>
        /// Adds an object to the back of the <see cref="ConstantDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the back of the <see cref="ConstantDeque{T}"/>.</param>
        public void PushBack(T value)
        {
            backInternalIndex++;
            CheckAndReserveBack();
            this[Count - 1] = value;
        }

        /// <summary>
        /// Removes and returns the object at the front of the <see cref="ConstantDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the front of the <see cref="ConstantDeque{T}"/>.</returns>
        public T PopFront()
        {
            T value = this[0];
            this[0] = default(T);
            frontInternalIndex++;
            CheckAndUnreserveFront();
            return value;
        }

        /// <summary>
        /// Removes and returns the object at the back of the <see cref="ConstantDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the back of the <see cref="ConstantDeque{T}"/>.</returns>
        public T PopBack()
        {
            T value = this[Count - 1];
            this[Count - 1] = default(T);
            backInternalIndex--;
            CheckAndUnreserveBack();
            return value;
        }

        /// <summary>
        /// Gets the value from the front of the <see cref="ConstantDeque{T}"/>.
        /// </summary>
        /// <returns>The frontmost value in the <see cref="ConstantDeque{T}"/>.</returns>
        public T PeekFront()
        {
            return this[0];
        }

        /// <summary>
        /// Gets the value from the back of the <see cref="ConstantDeque{T}"/>.
        /// </summary>
        /// <returns>The backmost value in the <see cref="ConstantDeque{T}"/>.</returns>
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

        /// <summary>
        /// Make sure the space for the next front value is allocated.
        /// </summary>
        private void CheckAndReserveFront()
        {
            if (frontInternalIndex < FirstReservedInternalIndex)
            {
                // More than one chunk space is reserved at a time, but the only one of the new chunks is created
                int additionalChunks = FrontInternalChunkIndex * FrontInternalChunkIndex;
                T[][] newMap = new T[map.Length + additionalChunks][];
                map.CopyTo(newMap, additionalChunks);
                newMap[additionalChunks - 1] = new T[chunkSize];
                map = newMap;
            }

            // Make sure chunk exists before adding a value to it
            int realChunk = GetRealIndexesFromInternal(frontInternalIndex).Item1;
            if (map[realChunk] == null)
            {
                map[realChunk] = new T[chunkSize];
            }
        }

        /// <summary>
        /// Make sure the space for the next back value is allocated.
        /// </summary>
        private void CheckAndReserveBack()
        {
            if (backInternalIndex >= LastReservedInternalIndex)
            {
                // More than one chunk space is reserved at a time, but the only one of the new chunks is created
                int additionalChunks = BackInternalChunkIndex * BackInternalChunkIndex;
                T[][] newMap = new T[map.Length + additionalChunks][];
                map.CopyTo(newMap, 0);
                newMap[BackInternalChunkIndex + 2] = new T[chunkSize];
                map = newMap;
            }

            // Make sure chunk exists before adding a value to it
            int realChunk = GetRealIndexesFromInternal(backInternalIndex).Item1;
            if (map[realChunk] == null)
            {
                map[realChunk] = new T[chunkSize];
            }
        }

        /// <summary>
        /// Checks if the first chunk is empty and clears it from reference.
        /// </summary>
        private void CheckAndUnreserveFront()
        {
            if (GetInternalChunkFromInternal(frontInternalIndex) > FrontInternalChunkIndex)
            {
                int realChunk = GetInternalChunkFromInternal(frontInternalIndex);
                map[realChunk] = null;
            }
        }

        /// <summary>
        /// Checks if the last chunk is empty and clears it from reference.
        /// </summary>
        private void CheckAndUnreserveBack()
        {
            if (GetInternalChunkFromInternal(backInternalIndex) < BackInternalChunkIndex)
            {
                int realChunk = BackInternalChunkIndex - FrontInternalChunkIndex;
                map[realChunk] = null;
            }
        }

        private int GetInternalChunkFromInternal(int internalIndex)
        {
            if (internalIndex < 0)
            {
                // index + 1 divided by chunksize, - 1, rounded down is the chunk
                //
                // if (chunksize = 2)
                // -2 -1
                // -----
                // -3 -1
                // -4 -2
                return ((internalIndex + 1) / chunkSize) - 1;
            }
            else
            {
                // index divided by chunksize rounded down is the chunk
                //
                // if (chunksize = 2)
                // 0 1 2 3
                // -------
                // 0 2 4 6
                // 1 3 5 7
                return internalIndex / chunkSize;
            }
        }

        /// <summary>
        /// Gets the real chunk index and chunk offset from an external index.
        /// </summary>
        /// <param name="externalIndex">External index position in <see cref="ConstantDeque{T}"/>.</param>
        /// <returns>(realChunk, chunkOffset).</returns>
        private (int, int) GetRealIndexesFromExternal(int externalIndex)
        {
            if (externalIndex >= Count || externalIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return GetRealIndexesFromInternal(frontInternalIndex + externalIndex);
        }

        /// <summary>
        /// Gets the real chunk index and chunk offset from an internal index.
        /// </summary>
        /// <param name="internalIndex">Internal index position in <see cref="ConstantDeque{T}"/>.</param>
        /// <returns>(realChunk, chunkOffset).</returns>
        private (int, int) GetRealIndexesFromInternal(int internalIndex)
        {
            int internalChunk = GetInternalChunkFromInternal(internalIndex);

            // index mod chunksize is how deep in the chunk the index is
            //
            // if (chunksize = 3)
            //    0 1 2 3
            // ----------
            // 0: 0 3 6 9
            // 1: 1 4 7 10
            // 2: 2 5 8 11
            int chunkOffset = internalIndex % chunkSize;
            if (chunkOffset < 0)
            {
                // If negative modulus, add chunksize
                chunkOffset += chunkSize;
            }

            // finds the map array index from chunk and frontChunk
            //
            //
            // RealChunk     :  0 1 2 3
            // internalChunk : -1 0 1 2
            int realChunk = internalChunk - FrontInternalChunkIndex;
            return (realChunk, chunkOffset);
        }
    }
}

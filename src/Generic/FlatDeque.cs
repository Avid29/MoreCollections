// Copyright (c) MoreCollections. All rights reserved.

using MoreCollections.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreCollections.Generic
{
    /// <summary>
    /// Represents a strongly typed <see cref="FlatDeque{T}"/> of objects.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="FlatDeque{T}"/>.</typeparam>
    public class FlatDeque<T> : IDeque<T>
    {
        private const int _DefaultSize = 4;

        private T[] items;

        /// <summary>
        /// The index of the first item.
        /// </summary>
        private int frontIndex;

        /// <summary>
        /// The number of items in the <see cref="FlatDeque{T}"/>.
        /// </summary>
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatDeque{T}"/> class.
        /// </summary>
        public FlatDeque() : this(_DefaultSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatDeque{T}"/> class.
        /// </summary>
        /// <param name="size">The size of the Deque.</param>
        public FlatDeque(int size)
        {
            items = new T[size];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatDeque{T}"/> class that contains elements
        /// copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <param name="chunkSize">Amount of memory reserved at a time <see cref="FlatDeque{T}"/>.</param>
        public FlatDeque(IEnumerable<T> collection, int chunkSize = _DefaultSize) : this(chunkSize)
        {
            foreach (var item in collection)
            {
                PushBack(item);
            }
        }

        /// <summary>
        /// Gets the number of items in the <see cref="FlatDeque{T}"/>.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Gets the index of the last item in the <see cref="FlatDeque{T}"/>.
        /// </summary>
        private int BackIndex => (frontIndex + count - 1) % items.Length;

        /// <summary>
        /// Gets or sets the value at <paramref name="index"/> in the <see cref="FlatDeque{T}"/>.
        /// </summary>
        /// <param name="index">The index to get or set.</param>
        /// <returns>The value at <paramref name="index"/>.</returns>
        public T this[int index]
        {
            get
            {
                if (index > count)
                {
                    throw new IndexOutOfRangeException();
                }

                return items[(frontIndex + index) % items.Length];
            }

            set
            {
                if (index > count)
                {
                    throw new IndexOutOfRangeException();
                }

                items[(frontIndex + index) % items.Length] = value;
            }
        }

        /// <summary>
        /// Adds an object to the Front of the <see cref="FlatDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the front of the <see cref="FlatDeque{T}"/>.</param>
        public void PushFront(T value)
        {
            int newFront = (frontIndex - 1 + items.Length) % items.Length;
            if (newFront == BackIndex)
            {
                Reallocate();
                newFront = items.Length - 1;
            }

            count++;
            frontIndex = newFront;
            items[newFront] = value;
        }

        /// <summary>
        /// Adds an object to the back of the <see cref="FlatDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the back of the <see cref="FlatDeque{T}"/>.</param>
        public void PushBack(T value)
        {
            int newBack = (BackIndex + 1) % items.Length;
            if (newBack == frontIndex && count != 0)
            {
                Reallocate();
                newBack = count;
            }

            count++;
            items[newBack] = value;
        }

        /// <summary>
        /// Removes and returns the object at the front of the <see cref="FlatDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the front of the <see cref="FlatDeque{T}"/>.</returns>
        public T PopFront()
        {
            T value = items[frontIndex];
            items[frontIndex] = default(T);

            count--;
            frontIndex = (frontIndex + 1) % items.Length;
            return value;
        }

        /// <summary>
        /// Removes and returns the object at the back of the <see cref="FlatDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the back of the <see cref="FlatDeque{T}"/>.</returns>
        public T PopBack()
        {
            T value = this[count - 1];
            items[count - 1] = default(T);

            count--;
            return value;
        }

        /// <summary>
        /// Gets the value from the front of the <see cref="FlatDeque{T}"/>.
        /// </summary>
        /// <returns>The frontmost value in the <see cref="FlatDeque{T}"/>.</returns>
        public T PeekFront()
        {
            return items[frontIndex];
        }

        /// <summary>
        /// Gets the value from the back of the <see cref="FlatDeque{T}"/>.
        /// </summary>
        /// <returns>The backmost value in the <see cref="FlatDeque{T}"/>.</returns>
        public T PeekBack()
        {
            return items[BackIndex];
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
        /// Doubles allocation size and realligns the chunks.
        /// </summary>
        private void Reallocate()
        {
            T[] newItems = new T[items.Length * 2];

            // TODO: Try conditional front < back.
            int firstSegmentLength = items.Length - frontIndex;
            Array.Copy(items, frontIndex, newItems, 0, firstSegmentLength);
            Array.Copy(items, 0, newItems, firstSegmentLength, frontIndex);

            items = newItems;
            frontIndex = 0;
        }
    }
}

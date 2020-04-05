// Copyright (c) MoreCollections. All rights reserved.

using System.Collections.Generic;

namespace MoreCollections.Interfaces
{
    /// <summary>
    /// An interface for the most basic functions of a double ended queue.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="IDeque{T}"/>.</typeparam>
    public interface IDeque<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets the number of items in the <see cref="IDeque"/>.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets or sets the value at <paramref name="index"/> in the <see cref="IDeque{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>Value at <paramref name="index"/> in <see cref="IDeque{T}"/>.</returns>
        T this[int index] { get; set; }

        /// <summary>
        /// Adds an object to the Front of the <see cref="IDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the front of the <see cref="IDeque{T}"/>.</param>
        void PushFront(T value);

        /// <summary>
        /// Adds an object to the back of the <see cref="IDeque{T}"/>.
        /// </summary>
        /// <param name="value">The object to be added to the back of the <see cref="IDeque{T}"/>.</param>
        void PushBack(T value);

        /// <summary>
        /// Removes and returns the object at the front of the <see cref="IDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the front of the <see cref="IDeque{T}"/>.</returns>
        T PopFront();

        /// <summary>
        /// Removes and returns the object at the back of the <see cref="IDeque{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the back of the <see cref="IDeque{T}"/>.</returns>
        T PopBack();

        /// <summary>
        /// Gets the value from the front of the <see cref="IDeque{T}"/>.
        /// </summary>
        /// <returns>The frontmost value in the <see cref="IDeque{T}"/>.</returns>
        T PeekFront();

        /// <summary>
        /// Gets the value from the back of the <see cref="IDeque{T}"/>.
        /// </summary>
        /// <returns>The backmost value in the <see cref="IDeque{T}"/>.</returns>
        T PeekBack();
    }
}

// Copyright (c) MoreCollections. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreCollections.Generic
{
    /// <summary>
    /// Enumerates over each item in a <see cref="Deque{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of items in the <see cref="Deque{T}"/>.</typeparam>
    internal class DequeEnum<T> : IEnumerator<T>, IDisposable
    {
        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        private int position = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="DequeEnum{T}"/> class to enumerate over <paramref name="deque"/>.
        /// </summary>
        /// <param name="deque">The <see cref="Deque{T}"/> to enumerate over.</param>
        public DequeEnum(Deque<T> deque)
        {
            Deque = deque;
        }

        /// <summary>
        /// Gets the IEnumerator <see cref="object"/> implimentation of <see cref="Current"/>.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Gets the current item in the <see cref="Deque{T}"/> from enumeration.
        /// </summary>
        public T Current
        {
            get
            {
                try
                {
                    return Deque[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private Deque<T> Deque { get; }

        /// <summary>
        /// Moves to next item in the <see cref="Deque{T}"/>.
        /// </summary>
        /// <returns>Whether or not there are more items.</returns>
        public bool MoveNext()
        {
            position++;
            return position < Deque.Count;
        }

        /// <summary>
        /// Resets enumeration to the start of the <see cref="Deque{T}"/>.
        /// </summary>
        public void Reset()
        {
            position = -1;
        }

        /// <summary>
        /// Dereference and clean up.
        /// </summary>
        public void Dispose()
        {
            // No actions here, just required for IEnumerator
        }
    }
}

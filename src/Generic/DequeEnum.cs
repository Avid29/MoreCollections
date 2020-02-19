using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreCollections.Generic
{
    internal class DequeEnum<T> : IEnumerator<T>, IDisposable
    {
        public DequeEnum(Deque<T> deque)
        {
            this.deque = deque;
        }

        private Deque<T> deque { get; }

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        private int position = -1;

        public bool MoveNext()
        {
            position++;
            return (position < deque.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current => Current;

        public T Current
        {
            get
            {
                try
                {
                    return deque[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        
        public void Dispose()
        {
            // No actions here, just required for IEnumerator
        }
    }
}

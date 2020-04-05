using MoreCollections.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsTest.Implimentations
{
    public class LinkedListDeque<T> : IDeque<T>
    {
        public LinkedList<T> list = new LinkedList<T>();

        public T this[int index]
        {
            get => list.ElementAt(index);
            set
            {
                LinkedListNode<T> node = list.First;
                for(int i = 0; i < index; i++)
                {
                    node = node.Next;
                }
                node.Value = value;
            }
        }

        public int Count => list.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public T PeekBack()
        {
            return list.Last.Value;
        }

        public T PeekFront()
        {
            return list.First.Value;
        }

        public T PopBack()
        {
            T last = list.Last.Value;
            list.RemoveLast();
            return last;
        }

        public T PopFront()
        {
            T first = list.First.Value;
            list.RemoveFirst();
            return first;
        }

        public void PushBack(T value)
        {
            list.AddLast(value);
        }

        public void PushFront(T value)
        {
            list.AddFirst(value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}

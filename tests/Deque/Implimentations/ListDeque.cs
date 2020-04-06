using MoreCollections.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsTest.Deque.Implimentations
{
    public class ListDeque<T> : IDeque<T>
    {
        private List<T> list = new List<T>();

        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }

        public int Count => list.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public T PeekBack()
        {
            return list.Last();
        }

        public T PeekFront()
        {
            return list[0];
        }

        public T PopBack()
        {
            T value = list.Last();
            list.RemoveAt(list.Count - 1);
            return value;
        }

        public T PopFront()
        {
            T value = list[0];
            list.RemoveAt(0);
            return value;
        }

        public void PushBack(T value)
        {
            list.Add(value);
        }

        public void PushFront(T value)
        {
            list.Insert(0, value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}

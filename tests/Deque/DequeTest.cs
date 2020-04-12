using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;
using MoreCollections.Interfaces;
using System.Collections.Generic;

namespace CollectionsTest
{
    public abstract class DequeTest
    {
        internal IDeque<int> deque;

        internal void Basic1()
        {
            deque.PushFront(2);
            deque.PushBack(2);
            deque.PushFront(2);
            deque.PushBack(2);
            deque.PopFront();
            deque.PopFront();
            deque.PopFront();
            deque.PopFront();
            deque.PushBack(2);
            deque.PushBack(2);
            deque.PushBack(2);
            deque.PushBack(2);
            deque.PushFront(2);
            int value = deque.PopFront();
            Assert.AreEqual(2, value);
        }

        internal void PushFront()
        {
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PushFront(3);
            deque.PushFront(4);
            deque.PushFront(5);
            deque.PushFront(6);
            deque.PushFront(7);
            deque.PushFront(8);
            deque.PushFront(9);

            int front = deque.PeekFront();
            Assert.AreEqual(9, front);

            int back = deque.PeekBack();
            Assert.AreEqual(1, back);
        }

        internal void PushBack()
        {
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PushBack(3);
            deque.PushBack(4);
            deque.PushBack(5);
            deque.PushBack(6);
            deque.PushBack(7);
            deque.PushBack(8);
            deque.PushBack(9);

            int front = deque.PeekFront();
            Assert.AreEqual(1, front);

            int back = deque.PeekBack();
            Assert.AreEqual(9, back);
        }

        internal void PopFront()
        {
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PopFront();
            deque.PushFront(3);

            int front = deque.PeekFront();
            Assert.AreEqual(3, front);

            int back = deque.PeekBack();
            Assert.AreEqual(1, back);
        }

        internal void PopBack()
        {
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PopBack();
            deque.PushBack(3);

            int front = deque.PeekFront();
            Assert.AreEqual(1, front);

            int back = deque.PeekBack();
            Assert.AreEqual(3, back);
        }

        internal void Count()
        {
            deque.PushFront(2);
            deque.PushFront(1);
            deque.PushBack(3);
            deque.PushBack(4);
            deque.PushBack(5);
            Assert.AreEqual(5, deque.Count);
        }

        internal void IEnumerableConstruct()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ConstantDeque<int> deque = new ConstantDeque<int>(list);

            bool equal = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != deque[i])
                {
                    equal = false;
                    break;
                }
            }
            Assert.IsTrue(equal);
        }

        internal void Enumerate()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            ConstantDeque<int> deque = new ConstantDeque<int>(list);

            int i = 0;
            foreach(int value in deque)
            {
                Assert.AreEqual(list[i], value);
                i++;
            }
        }
    }
}

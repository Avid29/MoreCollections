using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;
using System.Collections.Generic;

namespace CollectionsTest
{
    [TestClass]
    public class DequeTest
    {
        [TestMethod]
        public void Basic1()
        {
            ConstantDeque<int> deque = new ConstantDeque<int>();
            deque.PushFront(2);
            int value = deque.PopFront();
            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void Get()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ConstantDeque<int> deque = new ConstantDeque<int>(list, 2);
            Assert.AreEqual(5, deque[4]);
        }

        [TestMethod]
        public void Set()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ConstantDeque<int> deque = new ConstantDeque<int>(list, 2);
            deque[4] = 10;
            Assert.AreEqual(10, deque[4]);
        }

        [TestMethod]
        public void PushFront()
        {
            ConstantDeque<int> deque = new ConstantDeque<int>();
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

        [TestMethod]
        public void PushBack()
        {
            ConstantDeque<int> deque = new ConstantDeque<int>();
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

        [TestMethod]
        public void PopFront()
        {
            ConstantDeque<int> deque = new ConstantDeque<int>(2);
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PopFront();
            deque.PushFront(3);

            int front = deque.PeekFront();
            Assert.AreEqual(3, front);

            int back = deque.PeekBack();
            Assert.AreEqual(1, back);
        }

        [TestMethod]
        public void PopBack()
        {
            ConstantDeque<int> deque = new ConstantDeque<int>(2);
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PopBack();
            deque.PushBack(3);

            int front = deque.PeekFront();
            Assert.AreEqual(1, front);

            int back = deque.PeekBack();
            Assert.AreEqual(3, back);
        }

        [TestMethod]
        public void Count()
        {
            ConstantDeque<int> deque = new ConstantDeque<int>();
            deque.PushFront(2);
            deque.PushFront(1);
            deque.PushBack(3);
            deque.PushBack(4);
            deque.PushBack(5);
            Assert.AreEqual(5, deque.Count);
        }

        [TestMethod]
        public void Capacity()
        {
            ConstantDeque<int> deque = new ConstantDeque<int>(8);
            deque.PushFront(2);
            deque.PushFront(1);
            deque.PushBack(3);
            deque.PushBack(4);
            Assert.AreEqual(8, deque.Capacity);
        }

        [TestMethod]
        public void IEnumerableConstruct()
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

        [TestMethod]
        public void Enumerate()
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

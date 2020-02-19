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
            Deque<int> deque = new Deque<int>();
            deque.PushFront(2);
            int value = deque.PopFront();
            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void Count1()
        {
            Deque<int> deque = new Deque<int>(8);
            deque.PushFront(2);
            deque.PushFront(1);
            deque.PushBack(3);
            deque.PushBack(4);
            deque.PushBack(5);
            Assert.AreEqual(5, deque.Count);
        }

        [TestMethod]
        public void Capacity1()
        {
            Deque<int> deque = new Deque<int>(8);
            deque.PushFront(2);
            deque.PushFront(1);
            deque.PushBack(3);
            deque.PushBack(4);
            Assert.AreEqual(8, deque.Capacity);
        }

        [TestMethod]
        public void IEnumerableConstruct()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            Deque<int> deque = new Deque<int>(list);

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
    }
}

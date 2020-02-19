using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;
using System.Xml;

namespace CollectionsTest
{
    [TestClass]
    public class DequeTest
    {

        [TestMethod]
        public void BasicTest1()
        {
            Deque<int> deque = new Deque<int>();
            deque.PushFront(2);
            int value = deque.PopFront();
            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void CountTest1()
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
        public void CapacityTest1()
        {
            Deque<int> deque = new Deque<int>(8);
            deque.PushFront(2);
            deque.PushFront(1);
            deque.PushBack(3);
            deque.PushBack(4);
            Assert.AreEqual(8, deque.Capacity);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllCollections.Generic;
using System.Collections.Generic;

namespace CollectionsTest.Deque
{
    [TestClass]
    public class DynamicGridDequeTests : DequeTest
    {
        [TestMethod]
        internal void Basic1Dynamic()
        {
            deque = new DynamicGridDeque<int>();
            Basic1();
        }

        [TestMethod]
        public void GetDynamic()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            DynamicGridDeque<int> deque = new DynamicGridDeque<int>(list, 2);
            Assert.AreEqual(5, deque[4]);
        }

        [TestMethod]
        public void SetDynamic()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            DynamicGridDeque<int> deque = new DynamicGridDeque<int>(list, 2);
            deque[4] = 10;
            Assert.AreEqual(10, deque[4]);
        }

        [TestMethod]
        public void PushFrontDynamic()
        {
            deque = new DynamicGridDeque<int>();
            PushFront();
        }

        [TestMethod]
        public void PushBackDynamic()
        {
            deque = new DynamicGridDeque<int>();
            PushBack();
        }

        [TestMethod]
        public void PopFrontDynamic()
        {
            deque = new DynamicGridDeque<int>();
            PopFront();
        }

        [TestMethod]
        public void PopBackDynamic()
        {
            deque = new DynamicGridDeque<int>();
            PopBack();
        }

        [TestMethod]
        public void CountDynamic()
        {
            deque = new GridDeque<int>();
            Count();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;
using System.Collections.Generic;

namespace CollectionsTest.Deque
{
    [TestClass]
    public class DequeTests : DequeTest
    {
        [TestMethod]
        public void Basic1Constant()
        {
            deque = new Deque<int>(2);
            Basic1();
        }

        [TestMethod]
        public void GetConstant()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Deque<int> deque = new Deque<int>(list, 2);
            Assert.AreEqual(5, deque[4]);
        }

        [TestMethod]
        public void SetConstant()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Deque<int> deque = new Deque<int>(list, 2);
            deque[4] = 10;
            Assert.AreEqual(10, deque[4]);
        }

        [TestMethod]
        public void PushFrontFlat()
        {
            deque = new Deque<int>(2);
            PushFront();
        }

        [TestMethod]
        public void PushBackFlat()
        {
            deque = new Deque<int>(2);
            PushBack();
        }

        [TestMethod]
        public void PopFrontFlat()
        {
            deque = new Deque<int>(2);
            PopFront();
        }

        [TestMethod]
        public void PopBackFlat()
        {
            deque = new Deque<int>(2);
            PopBack();
        }

        [TestMethod]
        public void CountFlat()
        {
            deque = new Deque<int>(2);
            Count();
        }
    }
}

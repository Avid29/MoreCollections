using AllCollections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CollectionsTest.Deque
{
    [TestClass]
    public class GridDequeTests : DequeTest
    {
        [TestMethod]
        public void Basic1Grid()
        {
            deque = new GridDeque<int>(2);
            Basic1();
        }

        [TestMethod]
        public void GetGrid()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            GridDeque<int> deque = new GridDeque<int>(list, 2);
            Assert.AreEqual(5, deque[4]);
        }

        [TestMethod]
        public void SetGrid()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            GridDeque<int> deque = new GridDeque<int>(list, 2);
            deque[4] = 10;
            Assert.AreEqual(10, deque[4]);
        }

        [TestMethod]
        public void PushFrontGrid()
        {
            deque = new GridDeque<int>(2);
            PushFront();
        }

        [TestMethod]
        public void PushBackGrid()
        {
            deque = new GridDeque<int>(2);
            PushBack();
        }

        [TestMethod]
        public void PopFrontGrid()
        {
            deque = new GridDeque<int>(2);
            PopFront();
        }

        [TestMethod]
        public void PopBackGrid()
        {
            deque = new GridDeque<int>(2);
            PopBack();
        }

        [TestMethod]
        public void CountGrid()
        {
            deque = new GridDeque<int>(2);
            Count();
        }
    }
}

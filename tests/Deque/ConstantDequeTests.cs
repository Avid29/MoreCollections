using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;
using System.Collections.Generic;

namespace CollectionsTest.Deque
{
    [TestClass]
    public class ConstantDequeTests : DequeTest
    {
        [TestMethod]
        public void Basic1Constant()
        {
            deque = new GridDeque<int>(2);
            Basic1();
        }

        [TestMethod]
        public void GetConstant()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            GridDeque<int> deque = new GridDeque<int>(list, 2);
            Assert.AreEqual(5, deque[4]);
        }

        [TestMethod]
        public void SetConstant()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            GridDeque<int> deque = new GridDeque<int>(list, 2);
            deque[4] = 10;
            Assert.AreEqual(10, deque[4]);
        }

        [TestMethod]
        public void PushFrontConstant()
        {
            deque = new GridDeque<int>(2);
            PushFront();
        }

        [TestMethod]
        public void PushBackConstant()
        {
            deque = new GridDeque<int>(2);
            PushBack();
        }

        [TestMethod]
        public void PopFrontConstant()
        {
            deque = new GridDeque<int>(2);
            PopFront();
        }

        [TestMethod]
        public void PopBackConstant()
        {
            deque = new GridDeque<int>(2);
            PopBack();
        }

        [TestMethod]
        public void CountConstant()
        {
            deque = new GridDeque<int>(2);
            Count();
        }
    }
}

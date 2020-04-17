﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;
using System.Collections.Generic;

namespace CollectionsTest.Deque
{
    [TestClass]
    public class DynamicDequeTests : DequeTest
    {
        [TestMethod]
        internal void Basic1Constant()
        {
            deque = new DynamicGridDeque<int>();
            Basic1();
        }

        [TestMethod]
        public void GetConstant()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            DynamicGridDeque<int> deque = new DynamicGridDeque<int>(list, 2);
            Assert.AreEqual(5, deque[4]);
        }

        [TestMethod]
        public void SetConstant()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            DynamicGridDeque<int> deque = new DynamicGridDeque<int>(list, 2);
            deque[4] = 10;
            Assert.AreEqual(10, deque[4]);
        }

        [TestMethod]
        public void PushFrontConstant()
        {
            deque = new DynamicGridDeque<int>();
            PushFront();
        }

        [TestMethod]
        public void PushBackConstant()
        {
            deque = new DynamicGridDeque<int>();
            PushBack();
        }

        [TestMethod]
        public void PopFrontConstant()
        {
            deque = new DynamicGridDeque<int>();
            PopFront();
        }

        [TestMethod]
        public void PopBackConstant()
        {
            deque = new DynamicGridDeque<int>();
            PopBack();
        }

        [TestMethod]
        public void CountConstant()
        {
            deque = new GridDeque<int>();
            Count();
        }
    }
}

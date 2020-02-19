using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;

namespace CollectionsTest
{
    [TestClass]
    public class DequeTest
    {
        private Deque<int> deque;

        public DequeTest()
        {
            deque = new Deque<int>();
        }

        [TestMethod]
        public void DequeTest1()
        {
            deque.PushFront(1);
            deque.PushFront(10);
            deque.PushBack(5);
            deque.PushFront(2);
            deque.PushBack(5);
            deque.PushBack(5);
            deque.PushFront(10);
            deque.PushFront(4);
            deque.PushFront(60);
            deque.PushFront(54);
            deque.PushFront(14);
            deque.PushFront(10);
            deque.PopBack();
            deque.PopBack();
            deque.PopBack();
            deque.PopBack();
            deque[1] = 8;
            deque.PopBack();
            deque.PopBack();
            deque.PopBack();
            deque.PopBack();
            deque.PopBack();
            deque.PopBack();
            var value = deque.PopBack();

            Assert.AreEqual(8, value);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;

namespace CollectionsTest.Deque.Benchmarks
{
    [TestClass]
    public class DynamicDequeBenchmarks : DequeBenchmark
    {
        public DynamicDequeBenchmarks()
        {
            deque = new DynamicDeque<int>(512);
            for (int i = 0; i < 100; i++)
            {
                deque.PushBack(i);
            }
        }

        [TestMethod]
        public void Initialize()
        {

        }

        [TestMethod]
        public void PushBackDeque()
        {
            PushBack();
        }

        [TestMethod]
        public void PushFrontDeque()
        {
            PushFront();
        }
    }
}

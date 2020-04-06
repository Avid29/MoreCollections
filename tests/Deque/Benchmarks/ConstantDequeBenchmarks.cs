using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections.Generic;

namespace CollectionsTest.Deque.Benchmarks
{
    [TestClass]
    public class ConstantDequeBenchmarks : DequeBenchmark
    {
        public ConstantDequeBenchmarks()
        {
            deque = new ConstantDeque<int>();
            for (int i = 0; i < 1_000; i++)
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

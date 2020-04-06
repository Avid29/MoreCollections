using CollectionsTest.Deque.Implimentations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectionsTest.Deque.Benchmarks
{
    [TestClass]
    public class ListDequeBenchmarks : DequeBenchmark
    {
        public ListDequeBenchmarks()
        {
            deque = new ListDeque<int>();
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
        public void PushBackList()
        {
            PushBack();
        }

        [TestMethod]
        public void PushFrontList()
        {
            PushFront();
        }
    }
}

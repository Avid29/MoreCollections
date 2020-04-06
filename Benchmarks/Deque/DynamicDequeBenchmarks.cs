using MoreCollections.Generic;

namespace Benchmarks.Deque
{
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

        public void Initialize()
        {

        }

        public void PushBackDeque()
        {
            PushBack();
        }

        public void PushFrontDeque()
        {
            PushFront();
        }
    }
}

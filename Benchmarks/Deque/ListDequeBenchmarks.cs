using Implementations.Deque;

namespace Benchmarks.Deque
{
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

        public void Initialize()
        {

        }

        public void PushBackList()
        {
            PushBack();
        }

        public void PushFrontList()
        {
            PushFront();
        }
    }
}

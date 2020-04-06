using MoreCollections.Interfaces;

namespace CollectionsTest.Deque.Benchmarks
{
    public abstract class DequeBenchmark
    {
        internal IDeque<int> deque;

        internal void PushBack()
        {
            deque.PushBack(0);
        }

        internal void PushFront()
        {
            deque.PushFront(0);
        }
    }
}

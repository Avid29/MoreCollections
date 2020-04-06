using MoreCollections.Interfaces;

namespace CollectionsTest.DequeBenchmarks
{
    public class DequeBenchmark
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

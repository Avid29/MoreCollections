using MoreCollections.Interfaces;

namespace Benchmarks.Deque
{
    public abstract class DequeBenchmark
    {
        internal IDeque<int> deque;

        internal void PushBackN(int n = 1)
        {
            for (int i = 0; i < n; i++)
            {
                deque.PushBack(0);
            }
        }

        internal void PushFrontN(int n = 1)
        {
            for(int i = 0; i < n; i++)
            {
                deque.PushFront(0);
            }
        }

        internal void PopBackN(int n = 1)
        {
            if (deque.Count < n)
                return;

            for (int i = 0; i < n; i++)
            {
                deque.PopBack();
            }
        }

        internal void PopFrontN(int n = 1)
        {
            if (deque.Count < n)
                return;

            for (int i = 0; i < n; i++)
            {
                deque.PopFront();
            }
        }
    }
}

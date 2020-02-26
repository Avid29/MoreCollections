using MoreCollections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CollectionsTest
{
    [TestClass]
    public class Benchmarks
    {
        public Benchmarks()
        {
            list = new List<int>()
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
            21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
            31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
            41, 42, 43, 44, 45, 46, 47, 48, 49, 50};

            deque = new Deque<int>(list);
        }

        List<int> list;
        Deque<int> deque;

        [TestMethod]
        public void AAA()
        {

        }

        [TestMethod]
        public void AddList()
        {
            list.Add(51);
        }

        [TestMethod]
        public void AddDeque()
        {
            deque.PushBack(51);
        }

        [TestMethod]
        public void InsertList()
        {
            list.Insert(0, -1);
        }

        [TestMethod]
        public void InsertDeque()
        {
            deque.PushFront(-1);
        }
    }
}

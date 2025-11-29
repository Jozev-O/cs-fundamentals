using Heap;

namespace Priority_Queue
{
    public class Priority_Queue<T> where T : IComparable<T>
    {
        private Heap<T> _heap;
        public Priority_Queue()
        {
            _heap = new Heap<T>();
        }
        public Priority_Queue(List<T> list)
        {
            _heap = new Heap<T>([.. list]);
        }
        public void Enqueue(T item)
        {
            _heap.Insert(item);
        }
        public T Dequeue()
        {
            return _heap.ExtractMin();
        }
        public T Peek()
        {
            return _heap.Peek();
        }
        public bool IsEmpty()
        {
            return _heap.IsEmpty();
        }
        public int Size()
        {
            return _heap.Count;
        }
    }
}

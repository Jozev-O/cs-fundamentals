namespace Heap
{
    public class Heap<T> where T : IComparable<T>
    {
        private const string EMPTY_HEAP_MESSAGE = "Heap is empty.";

        private readonly List<T> _heap = [];
        public int Count => _heap.Count;
        public Heap() { }
        public Heap(T[] list)
        {
            foreach (var item in list)
            {
                Insert(item);
            }
        }
        public void Insert(T item)
        {
            _heap.Add(item);
            HeapifyUp(_heap.Count - 1);
        }
        public T ExtractMin()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException(EMPTY_HEAP_MESSAGE);
            T min = _heap[0];
            _heap[0] = _heap[^1];
            _heap.RemoveAt(_heap.Count - 1);
            HeapifyDown(0);
            return min;
        }
        public T Peek()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException(EMPTY_HEAP_MESSAGE);
            return _heap[0];
        }
        public bool IsEmpty()
        {
            return _heap.Count == 0;
        }
        public override string ToString()
        {
            return string.Join(", ", _heap);
        }
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_heap[index].CompareTo(_heap[parent]) < 0)
                {
                    (_heap[index], _heap[parent]) = (_heap[parent], _heap[index]);
                    index = parent;
                }
                else break;
            }
        }
        private void HeapifyDown(int index)
        {
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int smallest = index;

                if (left < _heap.Count && _heap[left].CompareTo(_heap[smallest]) < 0)
                    smallest = left;

                if (right < _heap.Count && _heap[right].CompareTo(_heap[smallest]) < 0)
                    smallest = right;

                if (smallest == index) break;

                (_heap[index], _heap[smallest]) = (_heap[smallest], _heap[index]);

                index = smallest;
            }
        }
    }
}

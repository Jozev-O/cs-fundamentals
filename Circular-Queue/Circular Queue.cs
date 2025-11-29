namespace Circular_Queue
{
    public class Circular_Queue<T>
    {
        private readonly T[] _queue;
        private readonly int _maxSize;
        private int _front;
        private int _rear;
        public int Length { get; private set; }
        public Circular_Queue(int size)
        {
            if (size <= 0) throw new ArgumentException("Size must be greater than zero.");
            _queue = new T[size];
            _front = -1;
            _rear = -1;
            _maxSize = size;
        }
        public void Enqueue(T item)
        {
            if (_maxSize == Length) throw new InvalidOperationException("Queue is full.");

            if (Length == 0) _rear = _front = 0;

            _queue[_rear++ % _maxSize] = item;
            Length++;
        }
        public T Dequeue()
        {
            if (Length == 0) throw new InvalidOperationException("Queue is empty.");
            T item = _queue[_front];
            _front = (_front + 1) % _maxSize;
            Length--;
            return item;
        }
        public T Peek()
        {
            if (Length == 0) throw new InvalidOperationException("Queue is empty.");
            return _queue[_front];
        }
        public bool IsFull()
        {
            return Length == _maxSize;
        }
        public bool IsEmpty()
        {
            return Length == 0;
        }
    }
}
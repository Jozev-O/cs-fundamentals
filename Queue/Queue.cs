namespace Queue
{
    public class Queue<T>
    {
        private Node<T> _front;
        private Node<T> _rear;
        public int Count { get; private set; }
        public Queue()
        {
            _rear = null;
            _front = null;
            Count = 0;
        }
        public Queue(T value)
        {
            Enqueue(value);
        }
        public void Enqueue(T value)
        {
            Count++;
            var newNode = new Node<T>(value);

            if (_rear == null)
            {
                _rear = newNode;
                _front = newNode;
                return;
            }
            _rear.Next = newNode;
            _rear = _rear.Next;
        }
        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty.");

            var dequeuedValue = _front.Value;
            if (_front == _rear) _rear = null;
            else _front = _front.Next;
            Count--;
            return dequeuedValue;
        }
        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty.");
            return _front.Value;
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }
    }
}

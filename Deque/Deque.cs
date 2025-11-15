namespace Deque
{
    public class Deque<T>
    {
        private const string DEQUE_EMPTY_MESSAGE = "Deque is empty";
        private Node<T> _front { get; set; } = null;
        private Node<T> _rear { get; set; } = null;
        public int Count { get; private set; }
        public void AddFront(T item)
        {
            Node<T> newNode = new(item);
            if (_front == null)
            {
                _front = newNode;
                _rear = _front;
            }
            else
            {
                newNode.Next = _front;
                _front.Prev = newNode;
                _front = newNode;
            }
            Count++;
        }
        public void AddRear(T item)
        {
            Node<T> newNode = new(item);
            if (_rear == null)
            {
                _rear = newNode;
                _front = _rear;
            }
            else
            {
                newNode.Prev = _rear;
                _rear.Next = newNode;
                _rear = newNode;
            }
            Count++;
        }
        public T RemoveFront()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException(DEQUE_EMPTY_MESSAGE);
            }
            T value = _front.Value;

            Count--;
            if (_front == _rear)
            {
                _front = null;
                _rear = null;
                return value;
            }

            _front = _front.Next;
            _front.Prev = null;
            return value;
        }
        public T RemoveRear()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException(DEQUE_EMPTY_MESSAGE);
            }
            T value = _rear.Value;

            Count--;
            if (_front == _rear)
            {
                _front = null;
                _rear = null;
                return value;
            }

            _rear = _rear.Prev;
            _rear.Next = null;
            return value;
        }
        public T PeekFront()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException(DEQUE_EMPTY_MESSAGE);
            }
            return _front.Value;
        }
        public T PeekRear()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException(DEQUE_EMPTY_MESSAGE);
            }
            return _rear.Value;
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }
    }
}
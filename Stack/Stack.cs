using System.Threading;

namespace Stack
{
    internal class Stack<T>
    {
        #region Методы для тестов
        internal Node<T>? GetTopForTesting() => _top;
        internal void SetHeadForTesting(Node<T>? node) => _top = node;
        #endregion
        private Node<T> _top;
        public int Count { get; private set; }
        public Stack()
        {
            _top = null;
            Count = 0;
        }
        public Stack(T value)
        {
            Push(value);
        }
        public void Push(T value)
        {
            Node<T> newNode = new(value);
            newNode.Next = _top;
            _top = newNode;
            Count++;
        }
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
            T poppedValue = _top.Value;
            _top = _top.Next;
            Count--;
            return poppedValue;
        }
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
            return _top.Value;
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }
    }
}

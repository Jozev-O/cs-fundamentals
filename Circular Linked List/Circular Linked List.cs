using System.Text;

namespace Circular_Linked_List
{
    class Circular_Linked_List<T>
    {
        #region Методы для тестов
        internal Circular_Node<T>? GetHeadForTesting() => _head;
        internal Circular_Node<T>? GetTailForTesting() => _tail;
        internal void SetHeadForTesting(Circular_Node<T>? node) => _head = node;
        internal void SetTailForTesting(Circular_Node<T>? node) => _tail = node;
        #endregion
        private Circular_Node<T> _head;
        private Circular_Node<T> _tail;
        public int Count { get; private set; }
        public Circular_Linked_List()
        {
            _head = null;
            Count = 0;
        }
        public Circular_Linked_List(T value)
        {
            _head = new Circular_Node<T>(value);
            _head.Next = _head;
            _tail = _head;
            Count = 1;
        }
        public bool IsEmpty()
        {
            return _head == null;
        }
        public void Add(T value)
        {
            var newNode = new Circular_Node<T>(value);

            if (_head == null)
            {
                _head = newNode;
                _head.Next = newNode;
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                newNode.Next = _head;
                _tail = newNode;
            }
            Count++;
        }
        public void DeleteHead()
        {
            if (_head == null) return;

            if (_head.Next == _head)
            {
                _head = null;
                Count = 0;
                return;
            }
            _tail.Next = _head.Next;
            _head = _tail.Next;
            Count--;
        }
        public void Remove(T value)
        {
            if (_head == null) return;

            if (_head.Value!.Equals(value))
            {
                DeleteHead();
                return;
            }

            var current = _head;
            var currentPrev = _tail;
            do
            {
                if (current.Value.Equals(value))
                {
                    if (current == _tail)
                    {
                        _tail = currentPrev;
                        _tail.Next = _head;
                    }
                    currentPrev.Next = current.Next;
                    Count--;
                    return;
                }
                current = current.Next;
                currentPrev = currentPrev.Next;
            } while (current != _head);
        }
        public bool Search(T value)
        {
            if (_head == null) return false;

            if (_head.Next == _head)
            {
                return _head.Value.Equals(value);
            }

            var current = _head;

            do
            {
                if (current.Value.Equals(value)) return true;

                current = current.Next;

            } while (current != _head);
            return false;
        }
        public void Traverse(Action<T> action)
        {
            if (_head is null) return;
            var current = _head;
            do
            {
                action(current.Value);
                current = current.Next;

            } while (current != _head);
        }
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (_head == null)
                return "List is empty";

            Circular_Node<T>? current = _head;
            do
            {
                if (sb.Length > 0)
                    sb.Append(" -> ");
                sb.Append($"[{current.Value}]");
                current = current.Next;

            } while (current != _head);

            return $"{sb} -> {current.Value}";
        }
    }
}
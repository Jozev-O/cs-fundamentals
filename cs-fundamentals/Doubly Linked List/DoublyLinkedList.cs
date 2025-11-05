using System.Text;

namespace Doubly_Linked_List
{
    public class DoublyLinkedList<T>
    {
        #region Методы для тестов
        internal DoublyNode<T>? GetHeadForTesting() => _head;
        internal DoublyNode<T>? GetTailForTesting() => _tail;
        internal void SetHeadForTesting(DoublyNode<T>? node) => _head = node;
        internal void SetTailForTesting(DoublyNode<T>? node) => _tail = node;
        #endregion
        private DoublyNode<T>? _head { get; set; }
        private DoublyNode<T>? _tail { get; set; }
        public DoublyLinkedList() { }
        public DoublyLinkedList(T value)
        {
            _head = new(value);
            _tail = _head;
        }
        public int GetLength()
        {
            int length = 0;
            DoublyNode<T>? current = _head;
            while (current != null)
            {
                length++;
                current = current.Next;
            }
            return length;
        }
        public bool IsEmpty()
        {
            return _head == null;
        }
        public void Clear()
        {
            _head = null;
            _tail = null;
        }
        public bool Search(T value)
        {
            if (IsEmpty()) return false;

            DoublyNode<T>? current = _head;

            while (current != null)
            {
                if (current.Value!.Equals(value))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }
        public T GetFirst(T value, bool SearchFromTail = false)
        {
            if (_head is null) return default;

            return SearchFromTail ? GetFirstFromTail(value) : GetFirstFromHead(value);
        }
        private T GetFirstFromTail(T value)
        {
            DoublyNode<T>? current = _tail;

            while (current != null)
            {
                if (current!.Value.Equals(value))
                {
                    return current.Value;
                }
                current = current.Previous;
            }
            return default;
        }
        private T GetFirstFromHead(T value)
        {
            DoublyNode<T>? current = _head;

            while (current != null)
            {
                if (current!.Value.Equals(value))
                {
                    return current.Value;
                }
                current = current.Next;
            }
            return default;
        }
        public void DeleteHead()
        {
            if (_head != null)
            {
                _head = _head.Next;
                if (GetLength() > 0) _head.Previous = null;
            }
        }
        public void DeleteTail()
        {
            if (_tail != null)
            {
                _tail = _tail.Previous;
                if (GetLength() > 0) _tail.Next = null;

            }
        }
        public void DeleteByValue(T value)
        {
            if (_head is null) return;

            if (_head.Value.Equals(value))
            {
                if (_head == _tail)
                {
                    _head = null;
                    _tail = null;
                    return;
                }
                _head = _head.Next;
                _head.Previous ??= null;
                return;
            }

            DoublyNode<T>? current = _head;

            while (current?.Next != null)
            {
                if (current.Value.Equals(value))
                {
                    if (current == _tail)
                    {
                        _tail = current.Previous;
                        _tail.Next = null;
                    }
                    else
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                    }
                    return;
                }
                current = current.Next;
            }
        }
        public void InsertAtHead(T value)
        {
            DoublyNode<T> newNode = new(value);
            if (_head == null)
            {
                _head = newNode;
                _tail = _head;
            }
            else
            {
                newNode.Next = _head;
                _head.Previous = newNode;
                _head = newNode;
            }
        }
        public void InsertAtPosition(T value, int position)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(position);

            DoublyNode<T> newNode = new(value);

            if (_head == null)
            {
                _head = newNode;
                _tail = _head;
                return;
            }

            if (position == 0)
            {
                newNode.Next = _head;
                _head = newNode;
                return;
            }

            DoublyNode<T>? current = _head;

            for (int i = 0; i < position && current.Next != null; i++)
            {
                current = current.Next;
            }
            current.Previous.Next = newNode;
            newNode.Previous = current.Previous;

            current.Previous = newNode;
            newNode.Next = current;
        }
        public void InsertAtTail(T value)
        {
            DoublyNode<T> newNode = new(value);

            if (_head == null)
            {
                _head = newNode;
                _tail = _head;
            }
            else
            {
                _tail.Next = newNode;
                newNode.Previous = _tail;
                _tail = newNode;
            }
        }
        public string ToString(bool TailToHead = false)
        {
            var sb = new StringBuilder();

            if (!TailToHead)
            {
                DoublyNode<T>? current = _head;
                while (current != null)
                {
                    if (sb.Length > 0)
                        sb.Append(" <-> ");
                    sb.Append($"[{current.Value}]");
                    current = current.Next;
                }
            }
            else
            {
                DoublyNode<T>? current = _tail;
                while (current != null)
                {
                    if (sb.Length > 0)
                        sb.Append(" <-> ");
                    sb.Append($"[{current.Value}]");
                    current = current.Previous;
                }
            }

            return $"null <- {sb} -> null";
        }
    }
}

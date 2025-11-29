using System.Threading;

namespace Linked_List_Singly
{
    public class SinglyLinkedList<T>
    {
        #region Методы для тестов
        public SinglyNode<T>? GetHeadForTesting() => _head;
        public void SetHeadForTesting(SinglyNode<T>? node) => _head = node;
        #endregion
        private SinglyNode<T>? _head { get; set; }
        public SinglyLinkedList()
        {
            _head = null;
        }
        public SinglyLinkedList(T value)
        {
            _head = new SinglyNode<T>(value);
        }
        public int GetLength()
        {
            int length = 0;
            SinglyNode<T>? current = _head;
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
        public bool Search(T value)
        {
            ArgumentNullException.ThrowIfNull(_head);

            SinglyNode<T>? current = _head;

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
        public T GetFirst(T value)
        {
            SinglyNode<T>? current = _head;

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
            }
        }
        public void DeleteLast()
        {
            SinglyNode<T> current = _head;
            while (current?.Next != null)
            {
                if (current.Next.Next == null)
                {
                    current.Next = null;
                    return;
                }
                current = current.Next;
            }
        }
        public void DeleteByValue(T value)
        {
            if (_head.Value!.Equals(value))
            {
                _head = _head.Next;
                return;
            }

            SinglyNode<T> current = _head;

            while (current.Next != null)
            {
                if (current.Next.Value!.Equals(value))
                {
                    current.Next = current.Next.Next;
                    return;
                }
                current = current.Next;
            }
        }
        public void Add(T value)
        {
            SinglyNode<T> newNode = new(value);
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head = newNode;
            }
        }
        public void AddAt(T value, int position)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(position);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(position, GetLength());

            SinglyNode<T> newNode = new(value);

            if (position == 0)
            {
                newNode.Next = _head;
                _head = newNode;
                return;
            }

            SinglyNode<T>? current = _head;
            for (int i = 0; i < position - 1; i++)
            {
                current = current.Next;
            }
            newNode.Next = current?.Next;
            if (current != null)
            {
                current.Next = newNode;
            }
        }
        public void AddToEnd(T value)
        {
            SinglyNode<T> newNode = new(value);
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                SinglyNode<T> current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }
        public override string ToString()
        {
            SinglyNode<T>? current = _head;
            string result = "";
            while (current != null)
            {
                result += $"{current.Value} -> ";
                current = current.Next;
            }
            return result + "null";
        }
    }
}

using System.Text;

namespace Doubly_Linked_List
{
    public class DoublyLinkedList<T>
    {
        public DoublyNode<T>? Head { get; private set; }
        public DoublyNode<T>? Tail { get; private set; }
        public DoublyLinkedList() { }
        public DoublyLinkedList(T value)
        {
            Head = new(value);
            Tail = Head;
        }
        public int GetLength()
        {
            int length = 0;
            DoublyNode<T>? current = Head;
            while (current != null)
            {
                length++;
                current = current.Next;
            }
            return length;
        }
        public bool IsEmpty()
        {
            return Head == null;
        }
        public void Clear()
        {
            Head = null;
            Tail = null;
        }
        public bool Search(T value)
        {
            if (IsEmpty()) return false;

            DoublyNode<T>? current = Head;

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
            if (Head is null) return default;

            return SearchFromTail ? GetFirstFromTail(value) : GetFirstFromHead(value);
        }
        private T GetFirstFromTail(T value)
        {
            DoublyNode<T>? current = Tail;

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
            DoublyNode<T>? current = Head;

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
            if (Head != null)
            {
                Head = Head.Next;
                if (GetLength() > 0) Head.Previous = null;
            }
        }
        public void DeleteTail()
        {
            if (Tail != null)
            {
                Tail = Tail.Previous;
                if (GetLength() > 0) Tail.Next = null;

            }
        }
        public void DeleteByValue(T value)
        {
            if (Head is null) return;

            if (Head.Value.Equals(value))
            {
                if (Head == Tail)
                {
                    Head = null;
                    Tail = null;
                    return;
                }
                Head = Head.Next;
                Head.Previous ??= null;
                return;
            }

            DoublyNode<T>? current = Head;

            while (current?.Next != null)
            {
                if (current.Value.Equals(value))
                {
                    if (current == Tail)
                    {
                        Tail = current.Previous;
                        Tail.Next = null;
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
            if (Head == null)
            {
                Head = newNode;
                Tail = Head;
            }
            else
            {
                newNode.Next = Head;
                Head.Previous = newNode;
                Head = newNode;
            }
        }
        public void InsertAtPosition(T value, int position)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(position);

            DoublyNode<T> newNode = new(value);

            if (Head == null)
            {
                Head = newNode;
                Tail = Head;
                return;
            }

            if (position == 0)
            {
                newNode.Next = Head;
                Head = newNode;
                return;
            }

            DoublyNode<T>? current = Head;

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

            if (Head == null)
            {
                Head = newNode;
                Tail = Head;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Previous = Tail;
                Tail = newNode;
            }
        }
        public string ToString(bool TailToHead = false)
        {
            var sb = new StringBuilder();

            if (!TailToHead)
            {
                DoublyNode<T>? current = Head;
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
                DoublyNode<T>? current = Tail;
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

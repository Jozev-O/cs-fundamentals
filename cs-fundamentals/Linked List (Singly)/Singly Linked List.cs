namespace Linked_List_Singly
{
    class SinglyLinkedList<T>
    {
        public SinglyNode<T>? Head { get; set; }
        public SinglyLinkedList()
        {
            Head = null;
        }
        public SinglyLinkedList(T value)
        {
            Head = new SinglyNode<T>(value);
        }
        public int GetLength()
        {
            int length = 0;
            SinglyNode<T>? current = Head;
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
        public bool Search(T value)
        {
            ArgumentNullException.ThrowIfNull(Head);

            SinglyNode<T>? current = Head;

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
        public bool Search(SinglyNode<T> node)
        {
            ArgumentNullException.ThrowIfNull(Head);

            SinglyNode<T>? current = Head;

            while (current != null)
            {
                if (current!.Equals(node))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
        public void DeleteFirst()
        {
            if (Head != null)
            {
                Head = Head.Next;
            }
        }
        public void DeleteLast()
        {
            SinglyNode<T> current = Head;
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
            if (Head.Value!.Equals(value))
            {
                Head = Head.Next;
                return;
            }

            SinglyNode<T> current = Head;

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
        public void InsertAtBeginning(T value)
        {
            SinglyNode<T> newNode = new(value);
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                newNode.Next = Head;
                Head = newNode;
            }
        }
        public void InsertAtPosition(T value, int position)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(position);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(position, GetLength());

            SinglyNode<T> newNode = new(value);

            if (position == 0)
            {
                newNode.Next = Head;
                Head = newNode;
                return;
            }

            SinglyNode<T>? current = Head;
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
        public void InsertAtEnd(T value)
        {
            SinglyNode<T> newNode = new(value);
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                SinglyNode<T> current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }
        public override string ToString()
        {
            SinglyNode<T>? current = Head;
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

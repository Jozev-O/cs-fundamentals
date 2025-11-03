namespace Linked_List_Singly
{
    class SinglyNode<T>(T value)
    {
        public SinglyNode<T>? Next { get; set; }
        public T? Value { get; set; } = value;
    }
}

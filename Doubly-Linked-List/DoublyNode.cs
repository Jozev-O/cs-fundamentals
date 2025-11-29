namespace Doubly_Linked_List
{
    public class DoublyNode<T>(T value)
    {
        public T Value { get; set; } = value;
        public DoublyNode<T>? Previous { get; set; }
        public DoublyNode<T>? Next { get; set; }
    }
}

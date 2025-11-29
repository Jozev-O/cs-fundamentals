namespace Circular_Linked_List
{
    public class Circular_Node<T>(T value)
    {
        public T Value { get; set; } = value;
        public Circular_Node<T>? Next { get; set; }
    }
}
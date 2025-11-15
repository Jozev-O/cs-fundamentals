namespace Deque
{
    internal class Node<T>(T value)
    {
        public T Value { get; set; } = value;
        public Node<T>? Prev { get; set; }
        public Node<T>? Next { get; set; }
    }
}

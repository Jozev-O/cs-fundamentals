namespace Queue
{
    internal class Node<T>(T value)
    {
        public Node<T> Next { get; set; } = null;
        public T Value { get; set; } = value;
    }
}
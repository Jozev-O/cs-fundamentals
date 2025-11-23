namespace AVLTree
{
    public class Node<T>(T value)
    {
        public T Value { get; set; } = value;
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }
        public int height { get; set; } = 1;
    }
}
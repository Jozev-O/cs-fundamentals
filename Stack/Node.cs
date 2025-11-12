using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Stack.Tests")]

namespace Stack
{
    internal class Node<T>(T data)
    {
        public Node<T> Next { get; set; }
        public T Value { get; set; } = data;
    }
}

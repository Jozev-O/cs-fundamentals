using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Doubly Linked List.Tests")]

namespace Doubly_Linked_List
{
    internal class DoublyNode<T>(T value)
    {
        public T Value { get; set; } = value;
        public DoublyNode<T>? Previous { get; set; }
        public DoublyNode<T>? Next { get; set; }
    }
}

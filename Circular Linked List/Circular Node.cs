using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Circular Linked List.Tests")]

namespace Circular_Linked_List
{
    internal class Circular_Node<T>(T value)
    {
        public T Value { get; set; } = value;
        public Circular_Node<T>? Next { get; set; }
    }
}
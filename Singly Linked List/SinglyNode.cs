using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Singly Linked List.Tests")]

namespace Linked_List_Singly
{
    internal class SinglyNode<T>(T value)
    {
        public SinglyNode<T>? Next { get; set; }
        public T? Value { get; set; } = value;
    }
}

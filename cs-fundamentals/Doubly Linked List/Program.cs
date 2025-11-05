namespace Doubly_Linked_List
{
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList<int> dll = new(1);

            dll.InsertAtHead(0);

            Console.WriteLine(dll.ToString());
            Console.WriteLine(dll.ToString(true));

            dll.InsertAtTail(3);

            Console.WriteLine(dll.ToString());
            Console.WriteLine(dll.ToString(true));

            dll.InsertAtTail(5);

            Console.WriteLine(dll.ToString());
            Console.WriteLine(dll.ToString(true));

            dll.InsertAtPosition(4, 3);

            Console.WriteLine(dll.ToString());
            Console.WriteLine(dll.ToString(true));

            dll.InsertAtTail(0);

            Console.WriteLine(dll.ToString());
            Console.WriteLine(dll.ToString(true));

            Console.WriteLine(dll.GetFirst(0));
            Console.WriteLine(dll.GetFirst(0, true));
            dll.DeleteByValue(0);
            Console.WriteLine(dll.ToString());
            dll.DeleteHead();
            Console.WriteLine(dll.ToString());
            dll.DeleteTail();
            Console.WriteLine(dll.ToString());


            Console.WriteLine(dll.GetLength());
            Console.WriteLine(dll.IsEmpty());
            Console.WriteLine(dll.Search(4));

            dll.Clear();


            Console.WriteLine(dll.GetLength());
            Console.WriteLine(dll.IsEmpty());
            Console.WriteLine(dll.Search(4));
        }
    }
}

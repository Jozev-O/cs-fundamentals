namespace Linked_List_Singly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>(1);

            list.InsertAtEnd(2);

            list.InsertAtEnd(3);

            list.InsertAtBeginning(0);

            list.InsertAtPosition(5, 2);

            Console.WriteLine(list.ToString());

            list.DeleteByValue(5);

            list.DeleteByValue(6);

            list.DeleteFirst();

            list.DeleteLast();

            Console.WriteLine(list.ToString());

            list.Search(2);

            list.Search(10);

            Console.WriteLine(list.GetLength());

            Console.WriteLine(list.IsEmpty());

            Console.WriteLine(list.ToString());
        }
    }
}

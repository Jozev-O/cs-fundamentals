namespace Linked_List_Singly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>(1);

            list.AddToEnd(2);

            list.AddToEnd(3);

            list.Add(0);

            list.AddAt(5, 2);

            Console.WriteLine(list.ToString());

            list.DeleteByValue(5);

            list.DeleteByValue(6);

            list.DeleteHead();

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

namespace Circular_Linked_List
{
    class Program
    {
        static void Main(string[] args)
        {
            Circular_Linked_List<int> circularList = new();
            circularList.Add(10);
            circularList.Add(20);
            circularList.Add(30);
            circularList.Add(40);
            circularList.Add(50);
            circularList.Add(60);

            circularList.Traverse((v) => Console.Write(v * 2 + " "));
            Console.WriteLine();

            Console.WriteLine(circularList.ToString());

            circularList.Remove(20);

            Console.WriteLine(circularList.ToString());

            circularList.Remove(50);

            Console.WriteLine(circularList.ToString());

            circularList.Remove(60);

            Console.WriteLine(circularList.ToString());

            Console.WriteLine(circularList.Search(50));
            Console.WriteLine(circularList.Search(40));
            var list = new Circular_Linked_List<int>(1);
            Console.WriteLine(list.Count);
            list.Add(2);
            Console.WriteLine(list.Count);

            list = new Circular_Linked_List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine(list.Count);
            list.Remove(1);
            Console.WriteLine(list.Count);
            Console.WriteLine(list.ToString());
            list.Remove(3);
            list.Remove(2);
            Console.WriteLine(list.ToString());

            list = new Circular_Linked_List<int>();
            list.Add(1);
            Console.WriteLine(list.Count);
            list.DeleteHead();
            Console.WriteLine(list.Count);



        }
    }
}

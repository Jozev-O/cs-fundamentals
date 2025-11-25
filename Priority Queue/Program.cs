namespace Priority_Queue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pq = new Priority_Queue<int>();
            pq.Enqueue(100);
            pq.Enqueue(2);
            pq.Enqueue(7);
            pq.Enqueue(5);
            pq.Enqueue(9);
            pq.Enqueue(6);
            pq.Enqueue(8);
            Console.WriteLine($"Size: {pq.Size()}");
            while (!pq.IsEmpty())
            {
                Console.WriteLine(pq.Dequeue());
            }
        }
    }
}

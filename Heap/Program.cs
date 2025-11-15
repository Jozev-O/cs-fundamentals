namespace Heap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new int[] { 5, 3, 8, 1, 2, 7 };
            var heap = new Heap<int>(list);
            Console.WriteLine(heap.ToString());
            while (!heap.IsEmpty())
            {
                heap.ExtractMin();
                Console.WriteLine(heap.ToString());
            }
        }
    }
}

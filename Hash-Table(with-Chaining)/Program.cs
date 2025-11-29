using System.Runtime.ExceptionServices;

namespace Hash_Table__with_Chaining_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var hashTable = new HashTable<string, int>();
            hashTable.Add("one", 1);
            hashTable.Add("two", 2);
            hashTable.Add("three", 3);
            hashTable.Add("four", 4);
            hashTable.Add("five", 5);
            hashTable.Add("six", 6);
            hashTable.Add("seven", 7);
            hashTable.Add("eight", 8);
            hashTable.Add("nine", 9);
            hashTable.Add("ten", 10);
            hashTable.Add("eleven", 11);

            Console.WriteLine(hashTable.ToString());
        }
    }
}

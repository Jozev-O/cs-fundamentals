namespace SkipList
{
    public class Node<T> where T : IComparable<T>
    {
        public T Value = default(T);
        public List<T> Forward = [];
    }
}

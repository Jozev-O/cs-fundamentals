namespace Red_Black_Tree
{
    internal class Node<T>(T value)
    {
        public T Value = value;
        public Node<T> Left;
        public Node<T> Right;
        public Node<T> Parent;
        /// <summary>
        ///(true=red, false=black)
        /// </summary>
        public bool Color = true;
    }
}

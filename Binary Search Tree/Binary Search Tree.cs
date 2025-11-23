using System.Text;

namespace Binary_Search_Tree
{
    public class Binary_Search_Tree<T> where T : IComparable<T>
    {
        private Node<T> Root { get; set; }
        public void Insert(T value)
        {
            Root = Insert(Root, value);
        }
        private Node<T> Insert(Node<T> node, T value)
        {
            if (node is null)
                return new Node<T>(value);
            if (node.Value.CompareTo(value) > 0)
                node.Left = Insert(node.Left, value);
            else
                node.Right = Insert(node.Right, value);
            return node;
        }
        public bool Contains(T value)
        {
            return Contains(Root, value);
        }

        private bool Contains(Node<T> node, T value)
        {
            if (node is null)
                return false;
            if (node.Value.CompareTo(value) == 0)
                return true;
            if (node.Value.CompareTo(value) > 0)
                return Contains(node.Left, value);
            else
                return Contains(node.Right, value);
        }

        public void Delete(T value) // рекурсивно, с обработкой случаев(0/1/2 ребёнка; для 2 — найти min в правом, заменить, удалить min).
        {
            Root = Delete(Root, value);
        }
        private Node<T> Delete(Node<T> node, T value)
        {
            if (node is null)
                return node;
            if (value.CompareTo(node.Value) < 0)
                node.Left = Delete(node.Left, value);

            else if (value.CompareTo(node.Value) > 0)
                node.Right = Delete(node.Right, value);

            else if (node.Left is null)
                return node.Right; // 0 или 1 правый
            else if (node.Right is null)
                return node.Left;   // 1 левый
            else
            {   // 2 ребёнка
                var min = FindMin(node.Right); // минимальный в правом поддереве
                node.Value = min.Value;
                node.Right = Delete(node.Right, min.Value);
            }
            return node;
        }

        private Node<T> FindMin(Node<T> node)
        {
            while (node.Left is not null)
                node = node.Left;
            return node;
        }
        public void InOrder(Action<T> action)
        {
            InOrder(Root, action);
        }

        private void InOrder(Node<T>? node, Action<T> action)
        {
            if (node is null)
                return;
            InOrder(node.Left, action);
            action(node.Value);
            InOrder(node.Right, action);
        }
    }
}

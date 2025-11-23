namespace Binary_Tree
{
    public class BinaryTree<T>
    {
        private const string TREE_IS_EMPTY_MESSAGE = "Binary tree is empty";
        private Node<T> _root { get; set; }
        public void Insert(T value)
        {
            if (IsEmpty())
            {
                _root = new Node<T>(value);
                return;
            }

            Queue<Node<T>> queue = new();
            queue.Enqueue(_root);
            while (queue.Count > 0)
            {
                Node<T> current = queue.Dequeue();
                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
                else
                {
                    current.Left = new Node<T>(value);
                    return;
                }

                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
                else
                {
                    current.Right = new Node<T>(value);
                    return;
                }
            }
        }
        //root-left-right
        public void TraversePreOrder(Action<T> action)
        {
            if (IsEmpty()) throw new InvalidOperationException(TREE_IS_EMPTY_MESSAGE);

            TraversePreOrder(action, _root);

            static void TraversePreOrder(Action<T> action, Node<T> node)
            {
                if (node == null) return;
                action(node.Value);
                TraversePreOrder(action, node.Left);
                TraversePreOrder(action, node.Right);
            }
        }
        //left-root-right
        public void TraverseInOrder(Action<T> action)
        {
            if (IsEmpty()) throw new InvalidOperationException(TREE_IS_EMPTY_MESSAGE);

            TraverseInOrder(action, _root);
            static void TraverseInOrder(Action<T> action, Node<T> node)
            {
                if (node == null) return;
                TraverseInOrder(action, node.Left);
                action(node.Value);
                TraverseInOrder(action, node.Right);
            }
        }
        //left-right-root
        public void TraversePostOrder(Action<T> action)
        {
            if (IsEmpty()) throw new InvalidOperationException(TREE_IS_EMPTY_MESSAGE);

            TraversePostOrder(action, _root);
            static void TraversePostOrder(Action<T> action, Node<T> node)
            {
                if (node == null) return;
                TraversePostOrder(action, node.Left);
                TraversePostOrder(action, node.Right);
                action(node.Value);
            }
        }
        public void TraverseLevelOrder(Action<T> action)
        {
            if (IsEmpty()) throw new InvalidOperationException(TREE_IS_EMPTY_MESSAGE);

            Queue<Node<T>> queue = new();
            queue.Enqueue(_root);
            while (queue.Count > 0)
            {
                Node<T> current = queue.Dequeue();
                action(current.Value);
                if (current.Left != null)
                    queue.Enqueue(current.Left);
                if (current.Right != null)
                    queue.Enqueue(current.Right);
            }
        }
        public bool IsEmpty()
        {
            return _root == null;
        }
        public int Height()
        {
            if (IsEmpty()) throw new InvalidOperationException(TREE_IS_EMPTY_MESSAGE);

            return Height(_root);
            static int Height(Node<T> node)
            {
                if (node == null) return 0;
                return 1 + Math.Max(Height(node.Right), Height(node.Left));
            }
        }
        public int Size()
        {
            if (IsEmpty()) throw new InvalidOperationException(TREE_IS_EMPTY_MESSAGE);

            return Size(_root);
            static int Size(Node<T> node)
            {
                if (node == null) return 0;
                return 1 + Size(node.Left) + Size(node.Right);
            }
        }
    }
}

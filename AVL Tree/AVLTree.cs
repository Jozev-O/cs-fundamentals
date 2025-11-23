namespace AVLTree
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private const string TREE_IS_EMPTY_MESSAGE = "AVL Tree is empty";

        private Node<T> _root { get; set; }
        public bool IsEmpty()
        {
            return _root == null;
        }

        public void Insert(T value)
        {
            if (IsEmpty())
            {
                _root = new Node<T>(value);
                return;
            }
            _root = Insert(_root, value);
        }
        private Node<T> Insert(Node<T> node, T value)
        {
            if (node is null)
            {
                return new Node<T>(value);
            }
            if (value.CompareTo(node.Value) < 0) // — в левое поддерево, иначе в правое.
            {
                node.Left = Insert(node.Left, value);
            }
            else if (value.CompareTo(node.Value) > 0)
            {
                node.Right = Insert(node.Right, value);
            }


            node.height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = GetBalance(node);


            // LL
            if (balance > 1 && (value.CompareTo(node.Left.Value) < 0))
                return RightRotate(node);
            // RR
            if (balance < -1 && (value.CompareTo(node.Right.Value) > 0))
                return LeftRotate(node);
            // LR
            if (balance > 1 && value.CompareTo(node.Left.Value) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }
            // RL
            if (balance < -1 && value.CompareTo(node.Right.Value) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }
            return node;
        }

        private Node<T> LeftRotate(Node<T> node)
        {
            var y = node.Right;
            var temp = y.Left;
            y.Left = node;
            node.Right = temp;
            node.height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            y.height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            return y;
        }

        private Node<T> RightRotate(Node<T> node)
        {
            var x = node.Left;
            var temp = x.Right;
            x.Right = node;
            node.Left = temp;
            node.height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            x.height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }

        private int GetBalance(Node<T> node)
        {
            if (node is null) return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        private int GetHeight(Node<T> node)
        {
            if (IsEmpty()) throw new InvalidOperationException(TREE_IS_EMPTY_MESSAGE);

            return Height(node);
            static int Height(Node<T> node)
            {
                if (node == null) return 0;
                return 1 + Math.Max(Height(node.Right), Height(node.Left));
            }
        }

        public bool Contains(T value)
        {
            return Contains(_root, value);
        }
        private bool Contains(Node<T> node, T value)
        {
            if (node == null) return false;
            if (value.CompareTo(node.Value) == 0) return true;
            return value.CompareTo(node.Value) < 0
                ? Contains(node.Left, value)
                : Contains(node.Right, value);
        }
        public void Delete(T value)
        {
            _root = Delete(_root, value);
        }

        private Node<T> Delete(Node<T> node, T value)
        {
            if (node is null) return node;
            if (value.CompareTo(node.Value) < 0)
                node.Left = Delete(node.Left, value);
            else if (value.CompareTo(node.Value) > 0)
                node.Right = Delete(node.Right, value);
            else if (node.Left is null) return node.Right;
            else if (node.Right is null) return node.Left;
            else
            {
                var min = FindMin(node.Right);
                node.Value = min.Value;
                node.Right = Delete(node.Right, min.Value);
            }
            node.height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = GetBalance(node);

            // LL
            if (balance > 1 && GetBalance(node.Left) >= 0) return RightRotate(node);
            // LR
            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }
            // RR
            if (balance < -1 && GetBalance(node.Right) <= 0) return LeftRotate(node);
            // RL
            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
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
            InOrder(_root, action);
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

//namespace Red_Black_Tree
//{
//    public class Red_Black_Tree<T> where T : IComparable<T>
//    {
//        private Node<T> _root;
//        public void Insert(T value)
//        {
//            _root = Insert(_root, value);
//        }
//        private Node<T> Insert(Node<T> node, T value)
//        {
//            if (node is null)
//                return new Node<T>(value);
//            if (node.Value.CompareTo(value) > 0)
//                node.Left = Insert(node.Left, value);
//            else
//                node.Right = Insert(node.Right, value);
//            return node;
//        }
//        FixInsert(node) :
//    while node.parent.color == red:
//        if node.parent == node.parent.parent.left:
//            uncle = node.parent.parent.right
//            if uncle.color == red:
//                node.parent.color = black
//                uncle.color = black
//                node.parent.parent.color = red
//                node = node.parent.parent
//            else:
//                if node == node.parent.right:
//                    node = node.parent
//                    LeftRotate(node.parent.parent)
//                node.parent.color = black
//                node.parent.parent.color = red
//                RightRotate(node.parent.parent)
//        else:  // симметрично для right
//            // аналогично с дядей слева, ротациями Right/Left
//    root.color = black
//        public void Delete(T value)
//        {

//        }
//        public bool Contains(T value)
//        {
//            return Contains(_root, value);
//        }
//        private bool Contains(Node<T> node, T value)
//        {
//            if (node is null)
//                return false;
//            if (node.Value.CompareTo(value) == 0)
//                return true;
//            if (node.Value.CompareTo(value) > 0)
//                return Contains(node.Left, value);
//            else
//                return Contains(node.Right, value);
//        }
//        public void InOrder(Action<T> action)
//        {
//            InOrder(_root, action);
//        }
//        private void InOrder(Node<T>? node, Action<T> action)
//        {
//            if (node is null)
//                return;
//            InOrder(node.Left, action);
//            action(node.Value);
//            InOrder(node.Right, action);
//        }
//    }
//}

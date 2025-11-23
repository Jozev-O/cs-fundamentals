namespace Binary_Tree.Tests
{

    public class Binary_Tree_Tests
    {
        [Fact]
        public void Constructor_Creates_Empty_Tree()
        {
            // Arrange & Act
            var tree = new BinaryTree<int>();

            // Assert
            Assert.True(tree.IsEmpty());
        }

        [Fact]
        public void Insert_First_Element_Becomes_Root()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act
            tree.Insert(10);

            // Assert
            Assert.False(tree.IsEmpty());
        }

        [Fact]
        public void Insert_Multiple_Elements_Builds_Correct_Tree()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);

            // Assert
            Assert.False(tree.IsEmpty());
            Assert.Equal(4, tree.Size());
        }

        [Fact]
        public void Traverse_Pre_Order_Returns_Correct_Sequence()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);

            var result = new List<int>();

            // Act
            tree.TraversePreOrder(value => result.Add(value));

            // Assert - Pre-order: root-left-right
            // Tree structure:
            //     1
            //    / \
            //   2   3
            //  / \
            // 4   5
            Assert.Equal(new[] { 1, 2, 4, 5, 3 }, result);
        }

        [Fact]
        public void Traverse_In_Order_Returns_Correct_Sequence()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);

            var result = new List<int>();

            // Act
            tree.TraverseInOrder(value => result.Add(value));

            // Assert - In-order: left-root-right
            // Tree structure:
            //     1
            //    / \
            //   2   3
            //  / \
            // 4   5
            Assert.Equal(new[] { 4, 2, 5, 1, 3 }, result);
        }

        [Fact]
        public void Traverse_Post_Order_Returns_Correct_Sequence()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);

            var result = new List<int>();

            // Act
            tree.TraversePostOrder(value => result.Add(value));

            // Assert - Post-order: left-right-root
            // Tree structure:
            //     1
            //    / \
            //   2   3
            //  / \
            // 4   5
            Assert.Equal(new[] { 4, 5, 2, 3, 1 }, result);
        }

        [Fact]
        public void Traverse_Level_Order_Returns_Correct_Sequence()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);

            var result = new List<int>();

            // Act
            tree.TraverseLevelOrder(value => result.Add(value));

            // Assert - Level-order: level by level
            // Tree structure:
            //     1
            //    / \
            //   2   3
            //  / \
            // 4   5
            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, result);
        }

        [Fact]
        public void Traverse_Pre_Order_Empty_Tree_Throws_Exception()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                tree.TraversePreOrder(value => { }));
            Assert.Equal("Binary tree is empty", exception.Message);
        }

        [Fact]
        public void Traverse_In_Order_Empty_Tree_Throws_Exception()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                tree.TraverseInOrder(value => { }));
            Assert.Equal("Binary tree is empty", exception.Message);
        }

        [Fact]
        public void Traverse_Post_Order_Empty_Tree_Throws_Exception()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                tree.TraversePostOrder(value => { }));
            Assert.Equal("Binary tree is empty", exception.Message);
        }

        [Fact]
        public void Traverse_Level_Order_Empty_Tree_Throws_Exception()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                tree.TraverseLevelOrder(value => { }));
            Assert.Equal("Binary tree is empty", exception.Message);
        }

        [Fact]
        public void Height_Empty_Tree_Throws_Exception()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => tree.Height());
            Assert.Equal("Binary tree is empty", exception.Message);
        }

        [Fact]
        public void Height_Single_Node_Returns_One()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Insert(1);

            // Act
            var height = tree.Height();

            // Assert
            Assert.Equal(1, height);
        }

        [Fact]
        public void Height_Multiple_Nodes_Returns_Correct_Height()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Insert(1);  // root
            tree.Insert(2);  // left child
            tree.Insert(3);  // right child
            tree.Insert(4);  // left-left child
            tree.Insert(5);  // left-right child

            // Act
            var height = tree.Height();

            // Assert
            // Tree structure:
            //     1      - level 1
            //    / \
            //   2   3    - level 2
            //  / \
            // 4   5      - level 3
            Assert.Equal(3, height);
        }

        [Fact]
        public void Size_Empty_Tree_Throws_Exception()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => tree.Size());
            Assert.Equal("Binary tree is empty", exception.Message);
        }

        [Fact]
        public void Size_Single_Node_Returns_One()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Insert(1);

            // Act
            var size = tree.Size();

            // Assert
            Assert.Equal(1, size);
        }

        [Fact]
        public void Size_Multiple_Nodes_Returns_Correct_Count()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);

            // Act
            var size = tree.Size();

            // Assert
            Assert.Equal(5, size);
        }

        [Fact]
        public void Is_Empty_Returns_Correct_State()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Assert - Initially empty
            Assert.True(tree.IsEmpty());

            // Act - Add item
            tree.Insert(1);

            // Assert - Not empty
            Assert.False(tree.IsEmpty());
        }

        [Fact]
        public void Tree_With_Strings_Works_Correctly()
        {
            // Arrange
            var tree = new BinaryTree<string>();
            tree.Insert("A");
            tree.Insert("B");
            tree.Insert("C");

            var preOrderResult = new List<string>();
            var inOrderResult = new List<string>();

            // Act
            tree.TraversePreOrder(value => preOrderResult.Add(value));
            tree.TraverseInOrder(value => inOrderResult.Add(value));

            // Assert
            Assert.Equal(3, tree.Size());
            Assert.Equal(2, tree.Height());
            Assert.Equal(["A", "B", "C"], preOrderResult);
            Assert.Equal(["B", "A", "C"], inOrderResult);
        }

        [Fact]
        public void Complex_Tree_Structure_All_Methods_Work()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            // Build this tree:
            //        1
            //       / \
            //      2   3
            //     / \   \
            //    4   5   6
            //   /
            //  7
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);
            tree.Insert(7);

            var preOrder = new List<int>();
            var inOrder = new List<int>();
            var postOrder = new List<int>();
            var levelOrder = new List<int>();

            // Act
            tree.TraversePreOrder(value => preOrder.Add(value));
            tree.TraverseInOrder(value => inOrder.Add(value));
            tree.TraversePostOrder(value => postOrder.Add(value));
            tree.TraverseLevelOrder(value => levelOrder.Add(value));

            // Assert
            Assert.Equal(7, tree.Size());
            Assert.Equal(4, tree.Height());

            // Expected orders based on tree structure
            Assert.Equal([1, 2, 4, 7, 5, 3, 6], preOrder);
            Assert.Equal([7, 4, 2, 5, 1, 3, 6], inOrder);
            Assert.Equal([7, 4, 5, 2, 6, 3, 1], postOrder);
            Assert.Equal([1, 2, 3, 4, 5, 6, 7], levelOrder);
        }

        [Fact]
        public void Large_Tree_Performance_Test()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            const int count = 100;

            // Act
            for (int i = 1; i <= count; i++)
            {
                tree.Insert(i);
            }

            var result = new List<int>();
            tree.TraverseLevelOrder(value => result.Add(value));

            // Assert
            Assert.Equal(count, tree.Size());
            Assert.True(tree.Height() >= (int)Math.Log2(count)); // Minimum height for complete tree
            Assert.Equal(count, result.Count);
        }

        [Fact]
        public void Tree_With_Custom_Objects_Works_Correctly()
        {
            // Arrange
            var tree = new BinaryTree<Person>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 35);

            tree.Insert(person1);
            tree.Insert(person2);
            tree.Insert(person3);

            var result = new List<Person>();

            // Act
            tree.TraversePreOrder(value => result.Add(value));

            // Assert
            Assert.Equal(3, tree.Size());
            Assert.Equal(person1, result[0]);
            Assert.Equal(person2, result[1]);
            Assert.Equal(person3, result[2]);
        }

        private class Person
        {
            public string Name { get; }
            public int Age { get; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public override bool Equals(object obj)
            {
                return obj is Person person &&
                       Name == person.Name &&
                       Age == person.Age;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Age);
            }
        }
    }
}
namespace Binary_Search_Tree.Tests
{
    public class BinarySearchTreeTests
    {
        [Fact]
        public void Constructor_Creates_Empty_Tree()
        {
            // Arrange & Act
            var bst = new Binary_Search_Tree<int>();

            // Assert
            Assert.False(bst.Contains(0));
        }

        [Fact]
        public void Insert_Single_Element_Can_Be_Found()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();

            // Act
            bst.Insert(10);

            // Assert
            Assert.True(bst.Contains(10));
        }

        [Fact]
        public void Insert_Multiple_Elements_All_Can_Be_Found()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();

            // Act
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(7);
            bst.Insert(1);
            bst.Insert(9);

            // Assert
            Assert.True(bst.Contains(5));
            Assert.True(bst.Contains(3));
            Assert.True(bst.Contains(7));
            Assert.True(bst.Contains(1));
            Assert.True(bst.Contains(9));
        }

        [Fact]
        public void Contains_Non_Existent_Element_Returns_False()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(7);

            // Act & Assert
            Assert.False(bst.Contains(10));
            Assert.False(bst.Contains(0));
            Assert.False(bst.Contains(4));
        }

        [Fact]
        public void Contains_Empty_Tree_Returns_False()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();

            // Act & Assert
            Assert.False(bst.Contains(5));
        }

        [Fact]
        public void InOrder_Traversal_Returns_Sorted_Sequence()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(7);
            bst.Insert(1);
            bst.Insert(9);
            bst.Insert(4);
            bst.Insert(6);

            var result = new List<int>();

            // Act
            bst.InOrder(value => result.Add(value));

            // Assert
            Assert.Equal(new[] { 1, 3, 4, 5, 6, 7, 9 }, result);
        }

        [Fact]
        public void InOrder_Traversal_Empty_Tree_Does_Nothing()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            var result = new List<int>();

            // Act
            bst.InOrder(value => result.Add(value));

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Delete_Leaf_Node_Removes_It()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(7);
            // Tree:
            //     5
            //    / \
            //   3   7

            // Act
            bst.Delete(3);

            // Assert
            Assert.False(bst.Contains(3));
            Assert.True(bst.Contains(5));
            Assert.True(bst.Contains(7));

            var result = new List<int>();
            bst.InOrder(value => result.Add(value));
            Assert.Equal(new[] { 5, 7 }, result);
        }

        [Fact]
        public void Delete_Node_With_One_Child_Replaces_With_Child()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(7);
            bst.Insert(1);
            // Tree:
            //     5
            //    / \
            //   3   7
            //  /
            // 1

            // Act
            bst.Delete(3);

            // Assert
            Assert.False(bst.Contains(3));
            Assert.True(bst.Contains(1));
            Assert.True(bst.Contains(5));
            Assert.True(bst.Contains(7));

            var result = new List<int>();
            bst.InOrder(value => result.Add(value));
            Assert.Equal(new[] { 1, 5, 7 }, result);
        }

        [Fact]
        public void Delete_Node_With_Two_Children_Replaces_With_Successor()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(7);
            bst.Insert(1);
            bst.Insert(4);
            bst.Insert(6);
            bst.Insert(9);
            // Tree:
            //       5
            //      / \
            //     3   7
            //    / \ / \
            //   1  4 6  9

            // Act
            bst.Delete(5);

            // Assert
            Assert.False(bst.Contains(5));
            Assert.True(bst.Contains(3));
            Assert.True(bst.Contains(7));
            Assert.True(bst.Contains(1));
            Assert.True(bst.Contains(4));
            Assert.True(bst.Contains(6));
            Assert.True(bst.Contains(9));

            var result = new List<int>();
            bst.InOrder(value => result.Add(value));
            Assert.Equal(new[] { 1, 3, 4, 6, 7, 9 }, result);
        }

        [Fact]
        public void Delete_Root_With_No_Children_Works()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            bst.Insert(5);

            // Act
            bst.Delete(5);

            // Assert
            Assert.False(bst.Contains(5));

            var result = new List<int>();
            bst.InOrder(value => result.Add(value));
            Assert.Empty(result);
        }

        [Fact]
        public void Delete_Non_Existent_Element_Does_Nothing()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(7);

            var before = new List<int>();
            bst.InOrder(value => before.Add(value));

            // Act
            bst.Delete(10);

            // Assert
            var after = new List<int>();
            bst.InOrder(value => after.Add(value));
            Assert.Equal(before, after);
        }

        [Fact]
        public void Insert_Duplicate_Values_Go_To_Left_Subtree()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();

            // Act
            bst.Insert(5);
            bst.Insert(5);
            bst.Insert(5);

            // Assert
            var result = new List<int>();
            bst.InOrder(value => result.Add(value));
            Assert.Equal(new[] { 5, 5, 5 }, result);
        }

        [Fact]
        public void Complex_Sequence_Of_Operations_Works_Correctly()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();

            // Act - Build tree
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);
            bst.Insert(20);
            bst.Insert(40);
            bst.Insert(60);
            bst.Insert(80);
            // Tree:
            //       50
            //      /  \
            //     30   70
            //    / \   / \
            //   20 40 60 80

            // Assert - Initial state
            Assert.True(bst.Contains(50));
            Assert.True(bst.Contains(30));
            Assert.True(bst.Contains(70));

            var initialOrder = new List<int>();
            bst.InOrder(value => initialOrder.Add(value));
            Assert.Equal(new[] { 20, 30, 40, 50, 60, 70, 80 }, initialOrder);

            // Act - Delete node with two children
            bst.Delete(50);

            // Assert - After deletion
            Assert.False(bst.Contains(50));
            Assert.True(bst.Contains(60)); // Successor

            var afterFirstDelete = new List<int>();
            bst.InOrder(value => afterFirstDelete.Add(value));
            Assert.Equal(new[] { 20, 30, 40, 60, 70, 80 }, afterFirstDelete);

            // Act - Delete leaf
            bst.Delete(20);

            // Assert
            Assert.False(bst.Contains(20));

            var afterSecondDelete = new List<int>();
            bst.InOrder(value => afterSecondDelete.Add(value));
            Assert.Equal(new[] { 30, 40, 60, 70, 80 }, afterSecondDelete);

            // Act - Delete node with one child
            bst.Delete(30);

            // Assert
            Assert.False(bst.Contains(30));
            Assert.True(bst.Contains(40));

            var afterThirdDelete = new List<int>();
            bst.InOrder(value => afterThirdDelete.Add(value));
            Assert.Equal(new[] { 40, 60, 70, 80 }, afterThirdDelete);
        }

        [Fact]
        public void Tree_With_Strings_Works_Correctly()
        {
            // Arrange
            var bst = new Binary_Search_Tree<string>();

            // Act
            bst.Insert("apple");
            bst.Insert("banana");
            bst.Insert("cherry");
            bst.Insert("date");

            // Assert
            Assert.True(bst.Contains("apple"));
            Assert.True(bst.Contains("banana"));
            Assert.False(bst.Contains("grape"));

            var result = new List<string>();
            bst.InOrder(value => result.Add(value));
            Assert.Equal(new[] { "apple", "banana", "cherry", "date" }, result);

            // Act - Delete
            bst.Delete("banana");

            // Assert
            Assert.False(bst.Contains("banana"));

            var afterDelete = new List<string>();
            bst.InOrder(value => afterDelete.Add(value));
            Assert.Equal(new[] { "apple", "cherry", "date" }, afterDelete);
        }

        [Fact]
        public void Tree_With_Custom_Objects_Works_Correctly()
        {
            // Arrange
            var bst = new Binary_Search_Tree<Person>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 20);

            // Act
            bst.Insert(person1);
            bst.Insert(person2);
            bst.Insert(person3);

            // Assert
            Assert.True(bst.Contains(person1));
            Assert.True(bst.Contains(person2));
            Assert.True(bst.Contains(person3));

            var result = new List<Person>();
            bst.InOrder(value => result.Add(value));
            // Should be sorted by age: Charlie(20), Alice(25), Bob(30)
            Assert.Equal(person3, result[0]);
            Assert.Equal(person1, result[1]);
            Assert.Equal(person2, result[2]);

            // Act - Delete
            bst.Delete(person1);

            // Assert
            Assert.False(bst.Contains(person1));
        }

        [Fact]
        public void Large_Tree_Operations_Perform_Correctly()
        {
            // Arrange
            var bst = new Binary_Search_Tree<int>();
            var random = new Random(42);
            var values = new List<int>();

            // Insert 100 random values
            for (int i = 0; i < 100; i++)
            {
                int value = random.Next(1, 1000);
                if (!values.Contains(value))
                {
                    values.Add(value);
                    bst.Insert(value);
                }
            }

            // Assert all inserted values can be found
            foreach (int value in values)
            {
                Assert.True(bst.Contains(value));
            }

            // Act - Delete half the values
            for (int i = 0; i < values.Count / 2; i++)
            {
                bst.Delete(values[i]);
            }

            // Assert - Remaining values should still be there
            for (int i = values.Count / 2; i < values.Count; i++)
            {
                Assert.True(bst.Contains(values[i]));
            }

            // Assert - Deleted values should be gone
            for (int i = 0; i < values.Count / 2; i++)
            {
                Assert.False(bst.Contains(values[i]));
            }

            // Assert - In-order traversal should be sorted
            var result = new List<int>();
            bst.InOrder(value => result.Add(value));

            for (int i = 1; i < result.Count; i++)
            {
                Assert.True(result[i - 1].CompareTo(result[i]) <= 0);
            }
        }

        private class Person : IComparable<Person>
        {
            public string Name { get; }
            public int Age { get; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public int CompareTo(Person other)
            {
                return Age.CompareTo(other.Age);
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

namespace AVLTree.Tests
{
    public class AVLTree_Tests
    {

        public class AVLTreeTests
        {
            [Fact]
            public void Constructor_Creates_Empty_Tree()
            {
                // Arrange & Act
                var avl = new AVLTree<int>();

                // Assert
                Assert.True(avl.IsEmpty());
            }

            [Fact]
            public void Insert_Single_Element_Works()
            {
                // Arrange
                var avl = new AVLTree<int>();

                // Act
                avl.Insert(10);

                // Assert
                Assert.False(avl.IsEmpty());
                Assert.True(avl.Contains(10));
            }

            [Fact]
            public void Insert_Multiple_Elements_Maintains_AVL_Property()
            {
                // Arrange
                var avl = new AVLTree<int>();

                // Act
                avl.Insert(10);
                avl.Insert(20);
                avl.Insert(5);
                avl.Insert(15);
                avl.Insert(25);

                // Assert
                Assert.True(avl.Contains(10));
                Assert.True(avl.Contains(20));
                Assert.True(avl.Contains(5));
                Assert.True(avl.Contains(15));
                Assert.True(avl.Contains(25));

                var result = new List<int>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { 5, 10, 15, 20, 25 }, result);
            }

            [Fact]
            public void Contains_Returns_Correct_Results()
            {
                // Arrange
                var avl = new AVLTree<int>();
                avl.Insert(10);
                avl.Insert(5);
                avl.Insert(15);

                // Act & Assert
                Assert.True(avl.Contains(10));
                Assert.True(avl.Contains(5));
                Assert.True(avl.Contains(15));
                Assert.False(avl.Contains(20));
                Assert.False(avl.Contains(0));
            }

            [Fact]
            public void Contains_Empty_Tree_Returns_False()
            {
                // Arrange
                var avl = new AVLTree<int>();

                // Act & Assert
                Assert.False(avl.Contains(10));
            }

            [Fact]
            public void InOrder_Traversal_Returns_Sorted_Sequence()
            {
                // Arrange
                var avl = new AVLTree<int>();
                avl.Insert(50);
                avl.Insert(30);
                avl.Insert(70);
                avl.Insert(20);
                avl.Insert(40);
                avl.Insert(60);
                avl.Insert(80);

                var result = new List<int>();

                // Act
                avl.InOrder(value => result.Add(value));

                // Assert
                Assert.Equal(new[] { 20, 30, 40, 50, 60, 70, 80 }, result);
            }

            [Fact]
            public void InOrder_Empty_Tree_Does_Nothing()
            {
                // Arrange
                var avl = new AVLTree<int>();
                var result = new List<int>();

                // Act
                avl.InOrder(value => result.Add(value));

                // Assert
                Assert.Empty(result);
            }

            [Fact]
            public void Delete_Leaf_Node_Works()
            {
                // Arrange
                var avl = new AVLTree<int>();
                avl.Insert(50);
                avl.Insert(30);
                avl.Insert(70);
                // Tree:
                //     50
                //    /  \
                //   30   70

                // Act
                avl.Delete(30);

                // Assert
                Assert.False(avl.Contains(30));
                Assert.True(avl.Contains(50));
                Assert.True(avl.Contains(70));

                var result = new List<int>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { 50, 70 }, result);
            }

            [Fact]
            public void Delete_Node_With_One_Child_Works()
            {
                // Arrange
                var avl = new AVLTree<int>();
                avl.Insert(50);
                avl.Insert(30);
                avl.Insert(70);
                avl.Insert(20);
                // Tree:
                //     50
                //    /  \
                //   30   70
                //  /
                // 20

                // Act
                avl.Delete(30);

                // Assert
                Assert.False(avl.Contains(30));
                Assert.True(avl.Contains(20));
                Assert.True(avl.Contains(50));
                Assert.True(avl.Contains(70));

                var result = new List<int>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { 20, 50, 70 }, result);
            }

            [Fact]
            public void Delete_Node_With_Two_Children_Works()
            {
                // Arrange
                var avl = new AVLTree<int>();
                avl.Insert(50);
                avl.Insert(30);
                avl.Insert(70);
                avl.Insert(20);
                avl.Insert(40);
                avl.Insert(60);
                avl.Insert(80);
                // Tree:
                //       50
                //      /  \
                //     30   70
                //    / \   / \
                //   20 40 60 80

                // Act
                avl.Delete(50);

                // Assert
                Assert.False(avl.Contains(50));
                Assert.True(avl.Contains(60)); // Successor

                var result = new List<int>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { 20, 30, 40, 60, 70, 80 }, result);
            }

            [Fact]
            public void Delete_Root_With_No_Children_Works()
            {
                // Arrange
                var avl = new AVLTree<int>();
                avl.Insert(50);

                // Act
                avl.Delete(50);

                // Assert
                Assert.False(avl.Contains(50));
                Assert.True(avl.IsEmpty());
            }

            [Fact]
            public void Delete_Non_Existent_Element_Does_Nothing()
            {
                // Arrange
                var avl = new AVLTree<int>();
                avl.Insert(50);
                avl.Insert(30);
                avl.Insert(70);

                var before = new List<int>();
                avl.InOrder(value => before.Add(value));

                // Act
                avl.Delete(100);

                // Assert
                var after = new List<int>();
                avl.InOrder(value => after.Add(value));
                Assert.Equal(before, after);
            }

            [Fact]
            public void Insert_Causes_Right_Rotation_LL_Case()
            {
                // Arrange & Act
                var avl = new AVLTree<int>();
                avl.Insert(30);
                avl.Insert(20);
                avl.Insert(10); // This should cause LL rotation

                // Assert
                var result = new List<int>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { 10, 20, 30 }, result);

                // Verify tree is balanced
                Assert.True(avl.Contains(10));
                Assert.True(avl.Contains(20));
                Assert.True(avl.Contains(30));
            }

            [Fact]
            public void Insert_Causes_Left_Rotation_RR_Case()
            {
                // Arrange & Act
                var avl = new AVLTree<int>();
                avl.Insert(10);
                avl.Insert(20);
                avl.Insert(30); // This should cause RR rotation

                // Assert
                var result = new List<int>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { 10, 20, 30 }, result);

                // Verify tree is balanced
                Assert.True(avl.Contains(10));
                Assert.True(avl.Contains(20));
                Assert.True(avl.Contains(30));
            }

            [Fact]
            public void Insert_Causes_LR_Rotation()
            {
                // Arrange & Act
                var avl = new AVLTree<int>();
                avl.Insert(30);
                avl.Insert(10);
                avl.Insert(20); // This should cause LR rotation

                // Assert
                var result = new List<int>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { 10, 20, 30 }, result);

                // Verify tree is balanced
                Assert.True(avl.Contains(10));
                Assert.True(avl.Contains(20));
                Assert.True(avl.Contains(30));
            }

            [Fact]
            public void Insert_Causes_RL_Rotation()
            {
                // Arrange & Act
                var avl = new AVLTree<int>();
                avl.Insert(10);
                avl.Insert(30);
                avl.Insert(20); // This should cause RL rotation

                // Assert
                var result = new List<int>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { 10, 20, 30 }, result);

                // Verify tree is balanced
                Assert.True(avl.Contains(10));
                Assert.True(avl.Contains(20));
                Assert.True(avl.Contains(30));
            }

            [Fact]
            public void Delete_Causes_Rebalancing()
            {
                // Arrange
                var avl = new AVLTree<int>();
                avl.Insert(50);
                avl.Insert(30);
                avl.Insert(70);
                avl.Insert(20);
                avl.Insert(40);
                avl.Insert(60);
                avl.Insert(80);
                avl.Insert(10);
                avl.Insert(25);
                avl.Insert(35);
                avl.Insert(45);
                // Complex tree that will require rebalancing after deletion

                // Act
                avl.Delete(80);
                avl.Delete(70);

                // Assert - Tree should remain balanced and valid
                var result = new List<int>();
                avl.InOrder(value => result.Add(value));

                // Check that the sequence is sorted (valid BST)
                for (int i = 1; i < result.Count; i++)
                {
                    Assert.True(result[i - 1] < result[i]);
                }

                // Check all remaining elements are present
                Assert.True(avl.Contains(50));
                Assert.True(avl.Contains(30));
                Assert.True(avl.Contains(20));
                Assert.True(avl.Contains(40));
                Assert.True(avl.Contains(60));
                Assert.True(avl.Contains(10));
                Assert.True(avl.Contains(25));
                Assert.True(avl.Contains(35));
                Assert.True(avl.Contains(45));
            }

            [Fact]
            public void Complex_Sequence_Of_Operations_Maintains_AVL_Property()
            {
                // Arrange
                var avl = new AVLTree<int>();

                // Act - Build complex tree
                avl.Insert(44);
                avl.Insert(17);
                avl.Insert(78);
                avl.Insert(32);
                avl.Insert(50);
                avl.Insert(88);
                avl.Insert(48);
                avl.Insert(62);
                avl.Insert(54);

                // Assert - Initial tree is balanced
                var initialOrder = new List<int>();
                avl.InOrder(value => initialOrder.Add(value));
                Assert.Equal(new[] { 17, 32, 44, 48, 50, 54, 62, 78, 88 }, initialOrder);

                // Act - Delete operations
                avl.Delete(32);
                avl.Delete(62);
                avl.Delete(88);

                // Assert - Tree remains balanced after deletions
                var afterDeletions = new List<int>();
                avl.InOrder(value => afterDeletions.Add(value));
                Assert.Equal(new[] { 17, 44, 48, 50, 54, 78 }, afterDeletions);

                // Act - More insertions
                avl.Insert(60);
                avl.Insert(70);
                avl.Insert(65);

                // Assert - Final tree is balanced
                var finalOrder = new List<int>();
                avl.InOrder(value => finalOrder.Add(value));

                // Check sorted order
                for (int i = 1; i < finalOrder.Count; i++)
                {
                    Assert.True(finalOrder[i - 1] < finalOrder[i]);
                }
            }

            [Fact]
            public void Tree_With_Strings_Works_Correctly()
            {
                // Arrange
                var avl = new AVLTree<string>();

                // Act
                avl.Insert("apple");
                avl.Insert("banana");
                avl.Insert("cherry");
                avl.Insert("date");
                avl.Insert("elderberry");

                // Assert
                Assert.True(avl.Contains("apple"));
                Assert.True(avl.Contains("banana"));
                Assert.False(avl.Contains("fig"));

                var result = new List<string>();
                avl.InOrder(value => result.Add(value));
                Assert.Equal(new[] { "apple", "banana", "cherry", "date", "elderberry" }, result);

                // Act - Delete
                avl.Delete("cherry");

                // Assert
                Assert.False(avl.Contains("cherry"));

                var afterDelete = new List<string>();
                avl.InOrder(value => afterDelete.Add(value));
                Assert.Equal(new[] { "apple", "banana", "date", "elderberry" }, afterDelete);
            }

            [Fact]
            public void Tree_With_Custom_Objects_Works_Correctly()
            {
                // Arrange
                var avl = new AVLTree<Person>();
                var person1 = new Person("Alice", 25);
                var person2 = new Person("Bob", 30);
                var person3 = new Person("Charlie", 20);
                var person4 = new Person("David", 35);

                // Act
                avl.Insert(person1);
                avl.Insert(person2);
                avl.Insert(person3);
                avl.Insert(person4);

                // Assert
                Assert.True(avl.Contains(person1));
                Assert.True(avl.Contains(person2));
                Assert.True(avl.Contains(person3));
                Assert.True(avl.Contains(person4));

                var result = new List<Person>();
                avl.InOrder(value => result.Add(value));
                // Should be sorted by age: Charlie(20), Alice(25), Bob(30), David(35)
                Assert.Equal(person3, result[0]);
                Assert.Equal(person1, result[1]);
                Assert.Equal(person2, result[2]);
                Assert.Equal(person4, result[3]);

                // Act - Delete
                avl.Delete(person2);

                // Assert
                Assert.False(avl.Contains(person2));

                var afterDelete = new List<Person>();
                avl.InOrder(value => afterDelete.Add(value));
                Assert.Equal(3, afterDelete.Count);
            }

            [Fact]
            public void Large_Tree_Remains_Balanced()
            {
                // Arrange
                var avl = new AVLTree<int>();
                var random = new Random(42);
                var insertedValues = new HashSet<int>();

                // Act - Insert 100 random values
                for (int i = 0; i < 100; i++)
                {
                    int value;
                    do
                    {
                        value = random.Next(1, 1000);
                    } while (!insertedValues.Add(value));

                    avl.Insert(value);
                }

                // Assert - All inserted values can be found
                foreach (int value in insertedValues)
                {
                    Assert.True(avl.Contains(value));
                }

                // Assert - In-order traversal returns sorted sequence
                var result = new List<int>();
                avl.InOrder(value => result.Add(value));

                for (int i = 1; i < result.Count; i++)
                {
                    Assert.True(result[i - 1] < result[i]);
                }

                // Act - Delete half the values
                var valuesToDelete = new List<int>();
                int deleteCount = insertedValues.Count / 2;
                foreach (int value in insertedValues)
                {
                    if (valuesToDelete.Count < deleteCount)
                    {
                        valuesToDelete.Add(value);
                    }
                    else
                    {
                        break;
                    }
                }

                foreach (int value in valuesToDelete)
                {
                    avl.Delete(value);
                }

                // Assert - Remaining values should still be there
                foreach (int value in insertedValues)
                {
                    if (!valuesToDelete.Contains(value))
                    {
                        Assert.True(avl.Contains(value));
                    }
                }

                // Assert - Deleted values should be gone
                foreach (int value in valuesToDelete)
                {
                    Assert.False(avl.Contains(value));
                }

                // Assert - Tree remains balanced (in-order should still be sorted)
                var afterDeleteResult = new List<int>();
                avl.InOrder(value => afterDeleteResult.Add(value));

                for (int i = 1; i < afterDeleteResult.Count; i++)
                {
                    Assert.True(afterDeleteResult[i - 1] < afterDeleteResult[i]);
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
}
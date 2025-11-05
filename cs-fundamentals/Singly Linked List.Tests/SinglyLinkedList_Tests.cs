using Linked_List_Singly;

namespace Singly_Linked_List.Tests
{
    public class SinglyLinkedList_Tests
    {
        [Fact]
        public void Constructor_WithValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var list = new SinglyLinkedList<int>(52);

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.Equal(52, list.GetHeadForTesting().Value);
        }

        [Fact]
        public void Constructor_WithStringValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var list = new SinglyLinkedList<string>("test");

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.Equal("test", list.GetHeadForTesting().Value);
        }

        [Fact]
        public void Constructor_WithReferenceType_SetsValueCorrectly()
        {
            // Arrange
            var person = new Person("John", 25);

            // Act
            var node = new SinglyNode<Person>(person);

            // Assert
            Assert.Equal(person, node.Value);
            Assert.Equal("John", node.Value.Name);
            Assert.Equal(25, node.Value.Age);

            // Arrange & Act
            var list = new SinglyLinkedList<string>("test");

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.Equal("test", list.GetHeadForTesting().Value);
        }

        [Fact]
        public void Value_Property_CanBeModified()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(10);

            // Act
            list.GetHeadForTesting().Value = 20;

            // Assert
            Assert.Equal(20, list.GetHeadForTesting().Value);
        }

        [Fact]
        public void Head_Next_InitiallyNull()
        {
            // Arrange & Act
            var list = new SinglyLinkedList<int>(1);

            // Assert
            Assert.Null(list.GetHeadForTesting().Next);
        }

        [Fact]
        public void IsEmpty_IsTrue_AfterInitialization()
        {
            // Arrange & Act
            var list = new SinglyLinkedList<int>();

            // Assert
            Assert.True(list.IsEmpty());
        }

        [Fact]
        public void IsEmpty_IsFalse_AfterInitialization()
        {
            // Arrange & Act
            var list = new SinglyLinkedList<int>(1);

            // Assert
            Assert.False(list.IsEmpty());
        }

        [Fact]
        public void Length_Is_One_After_Initialization()
        {
            // Arrange & Act
            var list = new SinglyLinkedList<int>(1);

            // Assert
            Assert.Equal(1, list.GetLength());
        }

        [Fact]
        public void Head_Is_Not_Null_After_Initialization()
        {
            // Arrange & Act
            var list = new SinglyLinkedList<int>(1);

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
        }

        [Fact]
        public void Add_Works_Correctly()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(2);

            // Act
            list.Add(1);

            // Assert
            Assert.Equal(1, list.GetHeadForTesting().Value);
            Assert.NotNull(list.GetHeadForTesting().Next);
            Assert.Equal(2, list.GetHeadForTesting().Next.Value);
        }

        [Fact]
        public void Add_At_Works_Correctly()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1);
            list.Add(3);

            // Act
            list.AddAt(2, 1); // Insert 2 at position 1

            // Assert
            Assert.Equal(3, list.GetHeadForTesting().Value);
            Assert.NotNull(list.GetHeadForTesting().Next);
            Assert.Equal(2, list.GetHeadForTesting().Next.Value);
            Assert.NotNull(list.GetHeadForTesting().Next.Next);
            Assert.Equal(1, list.GetHeadForTesting().Next.Next.Value);

        }

        [Fact]
        public void List_With_Complex_Object_Works_Correctly()
        {
            // Arrange
            var complexObject = new ComplexObject
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Data = new List<int> { 1, 2, 3 }
            };

            // Act
            var list = new SinglyLinkedList<ComplexObject>(complexObject);

            // Assert
            Assert.Equal(complexObject, list.GetHeadForTesting().Value);
            Assert.Equal("Test", list.GetHeadForTesting().Value.Name);
            Assert.Equal(3, list.GetHeadForTesting().Value.Data.Count);
            Assert.Equal(1, list.GetHeadForTesting().Value.Data[0]);
        }

        [Fact]
        public void Search_Finds_Existing_Value()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1);
            list.Add(2);
            list.Add(3);

            // Act & Assert
            Assert.True(list.Search(2));
            Assert.True(list.Search(1));
            Assert.True(list.Search(3));
            Assert.False(list.Search(4));
        }

        [Fact]
        public void Multiple_Add_Works_Correctly()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(3);

            // Act
            list.Add(2);
            list.Add(1);
            list.Add(4);
            list.Add(5);

            // Assert
            Assert.Equal(5, list.GetHeadForTesting().Value);
            Assert.Equal(5, list.GetLength());

            // Verify the sequence
            var current = list.GetHeadForTesting();
            for (int i = 5; i <= 1; i--)
            {
                Assert.Equal(i, current.Value);
                current = current.Next;
            }
        }

        [Fact]
        public void Get_First_Returns_First_Match()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1);

            // Act
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            // Assert
            Assert.Equal(1, list.GetFirst(1));
            Assert.Equal(5, list.GetFirst(5));
            Assert.Equal(0, list.GetFirst(6));
        }

        [Fact]
        public void Delete_Head_WorksCorrectly()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(2);

            // Act
            list.Add(1);

            list.DeleteHead();

            // Assert
            Assert.Equal(1, list.GetLength());
            Assert.Equal(2, list.GetHeadForTesting().Value);
            Assert.Null(list.GetHeadForTesting().Next);
        }

        [Fact]
        public void Delete_Tail_WorksCorrectly()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(2);

            // Act
            list.Add(1);

            list.DeleteLast();

            // Assert
            Assert.Equal(1, list.GetLength());
            Assert.Equal(1, list.GetHeadForTesting().Value);
            Assert.Null(list.GetHeadForTesting().Next);
        }
        [Fact]
        public void Delete_By_Value_Works_Correctly()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(2);

            // Act
            list.Add(1);
            list.Add(10);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            list.DeleteByValue(2);

            // Assert
            Assert.NotEqual(2, list.GetHeadForTesting().Value);
        }

        [Fact]
        public void ToString_Works_Correctly()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(5);

            // Act
            list.Add(4);
            list.Add(3);
            list.Add(2);
            list.Add(1);

            // Assert
            Assert.Equal("1 -> 2 -> 3 -> 4 -> 5 -> null", list.ToString());

        }

        #region Вспомогательные классы для тестирования
        private class Person
        {
            public string Name { get; }
            public int Age { get; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        private class ComplexObject
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public List<int> Data { get; set; } = new List<int>();
        }
        #endregion

    }
}

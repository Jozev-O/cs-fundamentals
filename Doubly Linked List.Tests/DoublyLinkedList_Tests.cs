namespace Doubly_Linked_List.Tests
{
    public class DoublyLinkedList_Tests
    {

        [Fact]
        public void Constructor_WithValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(52);

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.NotNull(list.GetTailForTesting());
            Assert.Equal(52, list.GetHeadForTesting().Value);
            Assert.Equal(52, list.GetTailForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void Constructor_WithStringValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<string>("test");

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.NotNull(list.GetTailForTesting());
            Assert.Equal("test", list.GetHeadForTesting().Value);
            Assert.Equal("test", list.GetTailForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void Constructor_WithReferenceType_SetsValueCorrectly()
        {
            // Arrange
            var person = new Person("John", 25);

            // Act
            var node = new DoublyNode<Person>(person);

            // Assert
            Assert.Equal(person, node.Value);
            Assert.Equal("John", node.Value.Name);
            Assert.Equal(25, node.Value.Age);

            // Arrange & Act
            var list = new DoublyLinkedList<string>("test");

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.NotNull(list.GetTailForTesting());
            Assert.Equal("test", list.GetHeadForTesting().Value);
            Assert.Equal("test", list.GetTailForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void Value_Property_CanBeModified()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(10);

            // Act
            list.GetHeadForTesting().Value = 20;

            // Assert
            Assert.Equal(20, list.GetHeadForTesting().Value);
        }

        [Fact]
        public void Head_Next_InitiallyNull()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(1);

            // Assert
            Assert.Null(list.GetHeadForTesting().Next);
        }

        [Fact]
        public void Head_Previous_InitiallyNull()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(1);

            // Assert
            Assert.Null(list.GetHeadForTesting().Previous);
        }

        [Fact]
        public void Tail_Next_InitiallyNull()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(1);

            // Assert
            Assert.Null(list.GetTailForTesting().Next);
        }

        [Fact]
        public void Tail_Previous_InitiallyNull()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(1);

            // Assert
            Assert.Null(list.GetTailForTesting().Previous);
        }

        [Fact]
        public void IsEmpty_IsTrue_AfterInitialization()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>();

            // Assert
            Assert.True(list.IsEmpty());
        }

        [Fact]
        public void IsEmpty_IsFalse_AfterInitialization()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(1);

            // Assert
            Assert.False(list.IsEmpty());
        }

        [Fact]
        public void Length_IsOne_AfterInitialization()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(1);

            // Assert
            Assert.Equal(1, list.GetLength());
        }

        [Fact]
        public void Head_And_Tail_AreNotNull_AfterInitialization()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(1);

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.NotNull(list.GetTailForTesting());
        }

        [Fact]
        public void Head_And_Tail_AreSame_ForSingleNodeList()
        {
            // Arrange & Act
            var list = new DoublyLinkedList<int>(1);

            // Assert
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void Insert_At_Tail_WorksCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(1);

            // Act
            list.InsertAtTail(2);

            // Assert
            Assert.Equal(1, list.GetHeadForTesting().Value);
            Assert.Equal(2, list.GetTailForTesting().Value);

            Assert.Null(list.GetTailForTesting().Next);
            Assert.Null(list.GetHeadForTesting().Previous);

            Assert.Equal(list.GetTailForTesting(), list.GetHeadForTesting().Next);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting().Previous);
        }

        [Fact]
        public void Insert_At_Head_WorksCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(2);

            // Act
            list.InsertAtHead(1);

            // Assert
            Assert.Equal(1, list.GetHeadForTesting().Value);
            Assert.Equal(2, list.GetTailForTesting().Value);

            Assert.Null(list.GetTailForTesting().Next);
            Assert.Null(list.GetHeadForTesting().Previous);

            Assert.Equal(list.GetTailForTesting(), list.GetHeadForTesting().Next);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting().Previous);
        }

        [Fact]
        public void Insert_At_Position_WorksCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(1);
            list.InsertAtTail(3);

            // Act
            list.InsertAtPosition(2, 1); // Insert 2 at position 1

            // Assert
            Assert.Equal(1, list.GetHeadForTesting().Value);
            Assert.Equal(3, list.GetTailForTesting().Value);
            Assert.Equal(2, list.GetHeadForTesting().Next.Value);
            Assert.Equal(list.GetHeadForTesting().Next, list.GetTailForTesting().Previous);
            Assert.Equal(list.GetHeadForTesting(), list.GetHeadForTesting().Next.Previous);
            Assert.Equal(list.GetTailForTesting(), list.GetHeadForTesting().Next.Next);
        }

        [Fact]
        public void List_WithComplexObject_WorksCorrectly()
        {
            // Arrange
            var complexObject = new ComplexObject
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Data = new List<int> { 1, 2, 3 }
            };

            // Act
            var list = new DoublyLinkedList<ComplexObject>(complexObject);

            // Assert
            Assert.Equal(complexObject, list.GetHeadForTesting().Value);
            Assert.Equal("Test", list.GetHeadForTesting().Value.Name);
            Assert.Equal(3, list.GetHeadForTesting().Value.Data.Count);
            Assert.Equal(1, list.GetHeadForTesting().Value.Data[0]);
        }

        [Fact]
        public void List_Clear_WorksCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(1);
            list.InsertAtTail(2);
            list.InsertAtTail(3);

            // Act
            list.Clear();

            // Assert
            Assert.True(list.IsEmpty());
            Assert.Null(list.GetHeadForTesting());
            Assert.Null(list.GetTailForTesting());
            Assert.Equal(0, list.GetLength());
        }

        [Fact]
        public void Search_FindsExistingValue()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(1);
            list.InsertAtTail(2);
            list.InsertAtTail(3);

            // Act & Assert
            Assert.True(list.Search(2));
            Assert.True(list.Search(1));
            Assert.True(list.Search(3));
            Assert.False(list.Search(4));
        }

        [Fact]
        public void Insert_Multiple_At_Head_And_Tail_WorksCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(3);

            // Act
            list.InsertAtHead(2);
            list.InsertAtHead(1);
            list.InsertAtTail(4);
            list.InsertAtTail(5);

            // Assert
            Assert.Equal(1, list.GetHeadForTesting().Value);
            Assert.Equal(5, list.GetTailForTesting().Value);
            Assert.Equal(5, list.GetLength());

            // Verify the sequence
            var current = list.GetHeadForTesting();
            for (int i = 1; i <= 5; i++)
            {
                Assert.Equal(i, current.Value);
                current = current.Next;
            }
        }

        [Fact]
        public void Get_First_Returns_First_Match()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(1);

            // Act
            list.InsertAtHead(2);
            list.InsertAtHead(3);
            list.InsertAtHead(4);
            list.InsertAtHead(5);

            // Assert
            Assert.Equal(1, list.GetFirst(1));
            Assert.Equal(5, list.GetFirst(5));
            Assert.Equal(0, list.GetFirst(6));
        }

        [Fact]
        public void Delete_Head_WorksCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(2);

            // Act
            list.InsertAtHead(1);

            list.DeleteHead();

            // Assert
            Assert.Equal(1, list.GetLength());
            Assert.Equal(2, list.GetHeadForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
            Assert.Null(list.GetHeadForTesting().Previous);
            Assert.Null(list.GetTailForTesting().Next);
        }

        [Fact]
        public void Delete_Tail_WorksCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(2);

            // Act
            list.InsertAtHead(1);

            list.DeleteTail();

            // Assert
            Assert.Equal(1, list.GetLength());
            Assert.Equal(1, list.GetHeadForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
            Assert.Null(list.GetHeadForTesting().Previous);
            Assert.Null(list.GetTailForTesting().Next);
        }

        [Fact]
        public void ToString_WorksCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<int>(1);

            // Act
            list.InsertAtHead(2);
            list.InsertAtHead(3);
            list.InsertAtHead(4);
            list.InsertAtHead(5);

            // Assert
            Assert.Equal("null <- [1] <-> [2] <-> [3] <-> [4] <-> [5] -> null", list.ToString(true));
            Assert.Equal("null <- [5] <-> [4] <-> [3] <-> [2] <-> [1] -> null", list.ToString());

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

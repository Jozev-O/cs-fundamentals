namespace Circular_Linked_List.Tests
{
    public class Circular_Linked_List
    {
        [Fact]
        public void Constructor_With_Value_Sets_Value_Correctly()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<int>(52);

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.NotNull(list.GetTailForTesting());
            Assert.Equal(52, list.GetHeadForTesting().Value);
            Assert.Equal(52, list.GetTailForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void Constructor_With_String_Value_Sets_Value_Correctly()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<string>("test");

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.NotNull(list.GetTailForTesting());
            Assert.Equal("test", list.GetHeadForTesting().Value);
            Assert.Equal("test", list.GetTailForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void Constructor_With_Reference_Type_Sets_Value_Correctly()
        {
            // Arrange
            var person = new Person("John", 25);

            // Act
            var node = new Circular_Node<Person>(person);

            // Assert
            Assert.Equal(person, node.Value);
            Assert.Equal("John", node.Value.Name);
            Assert.Equal(25, node.Value.Age);

            // Arrange & Act
            var list = new Circular_Linked_List<string>("test");

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.NotNull(list.GetTailForTesting());
            Assert.Equal("test", list.GetHeadForTesting().Value);
            Assert.Equal("test", list.GetTailForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void Value_Property_Can_Be_Modified()
        {
            // Arrange
            var list = new Circular_Linked_List<int>(10);

            // Act
            list.GetHeadForTesting().Value = 20;

            // Assert
            Assert.Equal(20, list.GetHeadForTesting().Value);
        }

        [Fact]
        public void Head_Next_Initially_Is_Not_Null()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<int>(1);

            // Assert
            Assert.NotNull(list.GetHeadForTesting().Next);
        }

        [Fact]
        public void Tail_Next_Initially_Is_Not_Null()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<int>(1);

            // Assert
            Assert.NotNull(list.GetTailForTesting().Next);
        }


        [Fact]
        public void Is_Empty_Is_True_After_Initialization()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<int>();

            // Assert
            Assert.True(list.IsEmpty());
        }

        [Fact]
        public void Is_Empty_Is_False_After_Initialization()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<int>(1);

            // Assert
            Assert.False(list.IsEmpty());
        }

        [Fact]
        public void Length_Is_One_After_Initialization()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<int>(1);

            // Assert
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void Length_Is_Zero_After_Initialization_And_Removing_Head()
        {
            // Arrange 
            var list = new Circular_Linked_List<int>(1);

            // Act
            list.DeleteHead();

            // Assert
            Assert.Equal(0, list.Count);
        }
        [Fact]
        public void Head_And_Tail_Are_Not_Null_AfterInitialization()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<int>(1);

            // Assert
            Assert.NotNull(list.GetHeadForTesting());
            Assert.NotNull(list.GetTailForTesting());
        }

        [Fact]
        public void Head_And_Tail_Are_Same_For_Single_Node_List()
        {
            // Arrange & Act
            var list = new Circular_Linked_List<int>(1);

            // Assert
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void Add_To_Null_List_Works_Correctly()
        {
            // Arrange
            var list = new Circular_Linked_List<int>();

            // Act
            list.Add(1);

            // Assert
            var Head = list.GetHeadForTesting();
            var Tail = list.GetTailForTesting();


            Assert.NotNull(Head);

            Assert.Equal(1, Head.Value);
            Assert.Equal(1, Tail.Value);

            Assert.Equal(Head, Tail);
            Assert.Equal(Head, Head.Next);
            Assert.Equal(Tail, Tail.Next);

            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void Add_To_Singly_Node_List_Works_Correctly()
        {
            // Arrange
            var list = new Circular_Linked_List<int>(1);

            // Act
            list.Add(2);

            // Assert
            var Head = list.GetHeadForTesting();
            var Tail = list.GetTailForTesting();


            Assert.NotNull(Head);
            Assert.NotNull(Tail);

            Assert.Equal(1, Head.Value);
            Assert.Equal(2, Tail.Value);

            Assert.NotEqual(Head, Tail);
            Assert.Equal(Head, Tail.Next);
            Assert.Equal(Tail, Head.Next);

            Assert.Equal(2, list.Count);
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
            var list = new Circular_Linked_List<ComplexObject>(complexObject);

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
            var list = new Circular_Linked_List<int>(1);
            list.Add(2);
            list.Add(3);

            // Act & Assert
            Assert.True(list.Search(2));
            Assert.True(list.Search(1));
            Assert.True(list.Search(3));
            Assert.False(list.Search(4));
        }


        [Fact]
        public void Delete_Head_Works_Correctly()
        {
            // Arrange
            var list = new Circular_Linked_List<int>(2);

            // Act
            list.Add(1);

            list.DeleteHead();

            // Assert
            Assert.Equal(1, list.Count);
            Assert.Equal(1, list.GetHeadForTesting().Value);
            Assert.Equal(list.GetHeadForTesting(), list.GetTailForTesting());
            Assert.Equal(list.GetTailForTesting(), list.GetHeadForTesting());
        }

        [Fact]
        public void Remove_Tail_Works_Correctly()
        {
            // Arrange
            var list = new Circular_Linked_List<int>(3);

            // Act
            list.Add(2);
            list.Add(1);

            list.Remove(2);

            // Assert
            Assert.Equal(2, list.Count);
            Assert.Equal(3, list.GetHeadForTesting().Value);
            Assert.Equal(1, list.GetHeadForTesting().Next.Value);
            Assert.NotEqual(list.GetHeadForTesting(), list.GetTailForTesting());
        }

        [Fact]
        public void ToString_Works_Correctly()
        {
            // Arrange
            var list = new Circular_Linked_List<int>(1);

            // Act
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            // Assert
            Assert.Equal("[1] -> [2] -> [3] -> [4] -> [5] -> 1", list.ToString());

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

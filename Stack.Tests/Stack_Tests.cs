namespace Stack.Tests
{
    public class Stack_Tests
    {
        [Fact]
        public void Constructor_With_Value_Sets_Value_Correctly()
        {
            // Arrange & Act
            var list = new Stack<int>(52);

            // Assert
            Assert.NotNull(list.GetTopForTesting());
            Assert.Null(list.GetTopForTesting().Next);
            Assert.Equal(52, list.GetTopForTesting().Value);
        }

        [Fact]
        public void Constructor_With_String_Value_Sets_Value_Correctly()
        {
            // Arrange & Act
            var list = new Stack<string>("test");

            // Assert
            Assert.NotNull(list.GetTopForTesting());
            Assert.Null(list.GetTopForTesting().Next);
            Assert.Equal("test", list.GetTopForTesting().Value);
        }

        [Fact]
        public void Constructor_With_Reference_Type_Sets_Value_Correctly()
        {
            // Arrange
            var person = new Person("John", 25);

            // Act
            var node = new Node<Person>(person);

            // Assert
            Assert.Equal(person, node.Value);
            Assert.Equal("John", node.Value.Name);
            Assert.Equal(25, node.Value.Age);

            // Arrange & Act
            var list = new Stack<string>("test");

            // Assert
            Assert.NotNull(list.GetTopForTesting());
            Assert.Null(list.GetTopForTesting().Next);
            Assert.Equal("test", list.GetTopForTesting().Value);
        }

        [Fact]
        public void Value_Property_Can_Be_Modified()
        {
            // Arrange
            var list = new Stack<int>(10);

            // Act
            list.GetTopForTesting().Value = 20;

            // Assert
            Assert.Equal(20, list.GetTopForTesting().Value);
        }

        [Fact]
        public void Top_Next_Initially_Is_Null()
        {
            // Arrange & Act
            var list = new Stack<int>(1);

            // Assert
            Assert.Null(list.GetTopForTesting().Next);
        }

        [Fact]
        public void Is_Empty_Is_True_After_Initialization()
        {
            // Arrange & Act
            var list = new Stack<int>();

            // Assert
            Assert.True(list.IsEmpty());
        }

        [Fact]
        public void Is_Empty_Is_False_After_Initialization()
        {
            // Arrange & Act
            var list = new Stack<int>(1);

            // Assert
            Assert.False(list.IsEmpty());
        }

        [Fact]
        public void Count_Is_One_After_Initialization()
        {
            // Arrange & Act
            var list = new Stack<int>(1);

            // Assert
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void Count_Is_Zero_After_Initialization_And_Removing_Top()
        {
            // Arrange 
            var list = new Stack<int>(1);

            // Act
            list.Pop();

            // Assert
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Push_To_Null_List_Works_Correctly()
        {
            // Arrange
            var list = new Stack<int>();

            // Act
            list.Push(1);

            // Assert
            var top = list.GetTopForTesting();

            Assert.NotNull(top);
            Assert.Null(top.Next);
            Assert.Equal(1, top.Value);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void Push_To_Singly_Node_List_Works_Correctly()
        {
            // Arrange
            var list = new Stack<int>(1);

            // Act
            list.Push(2);

            // Assert
            var top = list.GetTopForTesting();


            Assert.NotNull(top);
            Assert.NotNull(top.Next);
            Assert.Null(top.Next.Next);

            Assert.Equal(2, top.Value);
            Assert.Equal(1, top.Next.Value);
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
            var list = new Stack<ComplexObject>(complexObject);

            // Assert
            Assert.Equal(complexObject, list.GetTopForTesting().Value);
            Assert.Equal("Test", list.GetTopForTesting().Value.Name);
            Assert.Equal(3, list.GetTopForTesting().Value.Data.Count);
            Assert.Equal(1, list.GetTopForTesting().Value.Data[0]);
        }

        [Fact]
        public void Pop_Works_Correctly()
        {
            // Arrange
            var list = new Stack<int>(2);

            // Act
            list.Push(1);

            var pop = list.Pop();

            // Assert
            Assert.Equal(1, list.Count);
            Assert.Equal(1, pop);

            Assert.NotNull(list.GetTopForTesting());
            Assert.Equal(2, list.GetTopForTesting().Value);
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
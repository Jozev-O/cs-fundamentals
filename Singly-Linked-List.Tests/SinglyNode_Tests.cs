using Linked_List_Singly;

namespace Singly_Linked_List.Tests
{
    public class SinglyNode_Tests
    {
        [Fact]
        public void Constructor_WithValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var node = new SinglyNode<int>(42);

            // Assert
            Assert.Equal(42, node.Value);
        }

        [Fact]
        public void Constructor_WithStringValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var node = new SinglyNode<string>("test");

            // Assert
            Assert.Equal("test", node.Value);
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
        }

        [Fact]
        public void Value_Property_CanBeModified()
        {
            // Arrange
            var node = new SinglyNode<int>(10);

            // Act
            node.Value = 20;

            // Assert
            Assert.Equal(20, node.Value);
        }

        [Fact]
        public void Next_Property_InitiallyNull()
        {
            // Arrange & Act
            var node = new SinglyNode<int>(1);

            // Assert
            Assert.Null(node.Next);
        }

        [Fact]
        public void Next_Property_CanBeSet()
        {
            // Arrange
            var node1 = new SinglyNode<int>(1);
            var node2 = new SinglyNode<int>(2);

            // Act
            node1.Next = node2;

            // Assert
            Assert.Equal(node2, node1.Next);
            Assert.Equal(2, node1.Next.Value);
        }

        [Fact]
        public void Node_CanFormLinkedList()
        {
            // Arrange
            var node1 = new SinglyNode<int>(1);
            var node2 = new SinglyNode<int>(2);
            var node3 = new SinglyNode<int>(3);

            // Act - создаем двунаправленный список: 1 <-> 2 <-> 3
            node1.Next = node2;
            node2.Next = node3;

            // Assert - проверяем связи вперед
            Assert.Equal(node2, node1.Next);
            Assert.Equal(node3, node2.Next);
            Assert.Null(node3.Next);

            // Assert - проверяем значения
            Assert.Equal(1, node1.Value);
            Assert.Equal(2, node2.Value);
            Assert.Equal(3, node3.Value);
        }

        [Fact]
        public void Node_WithNullValue_WorksCorrectly()
        {
            // Arrange & Act
            var node = new SinglyNode<string?>(null);

            // Assert
            Assert.Null(node.Value);
            Assert.Null(node.Next);
        }

        [Fact]
        public void Value_Property_CanBeSetToNull()
        {
            // Arrange
            var node = new SinglyNode<string?>("not null");

            // Act
            node.Value = null;

            // Assert
            Assert.Null(node.Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Constructor_WithDifferentIntegers_SetsValueCorrectly(int value)
        {
            // Arrange & Act
            var node = new SinglyNode<int>(value);

            // Assert
            Assert.Equal(value, node.Value);
        }

        [Fact]
        public void Node_CanBeUsedInGenericCollection()
        {
            // Arrange
            var nodes = new List<SinglyNode<int>>
          {
              new SinglyNode<int>(1),
              new SinglyNode<int>(2),
              new SinglyNode<int>(3)
          };

            // Act & Assert
            Assert.Equal(3, nodes.Count);
            Assert.Equal(1, nodes[0].Value);
            Assert.Equal(2, nodes[1].Value);
            Assert.Equal(3, nodes[2].Value);
        }

        [Fact]
        public void Previous_And_Next_CanBeCircular()
        {
            // Arrange
            var node1 = new SinglyNode<int>(1);
            var node2 = new SinglyNode<int>(2);

            // Act - создаем циклическую ссылку
            node1.Next = node2;
            node2.Next = node1; // циклическая ссылка

            // Assert
            Assert.Equal(node2, node1.Next);
            Assert.Equal(node1, node2.Next);
        }

        [Fact]
        public void Node_WithComplexObject_WorksCorrectly()
        {
            // Arrange
            var complexObject = new ComplexObject
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Data = new List<int> { 1, 2, 3 }
            };

            // Act
            var node = new SinglyNode<ComplexObject>(complexObject);

            // Assert
            Assert.Equal(complexObject, node.Value);
            Assert.Equal("Test", node.Value.Name);
            Assert.Equal(3, node.Value.Data.Count);
            Assert.Equal(1, node.Value.Data[0]);
        }

        [Fact]
        public void Node_Properties_AreIndependent()
        {
            // Arrange
            var node = new SinglyNode<int>(1);
            var nextNode = new SinglyNode<int>(2);

            // Act
            node.Next = nextNode;

            // Assert - изменение одного свойства не влияет на другие
            Assert.Equal(1, node.Value);
            Assert.Equal(nextNode, node.Next);

            // Act - меняем значение
            node.Value = 100;

            // Assert - связи не изменились
            Assert.Equal(nextNode, node.Next);
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
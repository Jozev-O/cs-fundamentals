namespace Deque.Tests
{
    public class Deque_Tests
    {
        [Fact]
        public void Constructor_Creates_Empty_Deque()
        {
            // Arrange & Act
            var deque = new Deque<int>();

            // Assert
            Assert.True(deque.IsEmpty());
            Assert.Equal(0, deque.Count);
        }

        [Fact]
        public void Add_Front_To_Empty_Deque_Sets_Front_And_Rear()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            deque.AddFront(10);

            // Assert
            Assert.False(deque.IsEmpty());
            Assert.Equal(1, deque.Count);
            Assert.Equal(10, deque.PeekFront());
            Assert.Equal(10, deque.PeekRear());
        }

        [Fact]
        public void Add_Rear_To_Empty_Deque_Sets_Front_And_Rear()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            deque.AddRear(10);

            // Assert
            Assert.False(deque.IsEmpty());
            Assert.Equal(1, deque.Count);
            Assert.Equal(10, deque.PeekFront());
            Assert.Equal(10, deque.PeekRear());
        }

        [Fact]
        public void Add_Front_Multiple_Items_Maintains_Correct_Order()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            deque.AddFront(1);
            deque.AddFront(2);
            deque.AddFront(3);

            // Assert
            Assert.Equal(3, deque.PeekFront());
            Assert.Equal(1, deque.PeekRear());
            Assert.Equal(3, deque.Count);
        }

        [Fact]
        public void Add_Rear_Multiple_Items_Maintains_Correct_Order()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            deque.AddRear(1);
            deque.AddRear(2);
            deque.AddRear(3);

            // Assert
            Assert.Equal(1, deque.PeekFront());
            Assert.Equal(3, deque.PeekRear());
            Assert.Equal(3, deque.Count);
        }

        [Fact]
        public void Remove_Front_Empty_Deque_Throws_Exception()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => deque.RemoveFront());
            Assert.Equal("Deque is empty", exception.Message);
        }

        [Fact]
        public void Remove_Front_Single_Item_Empties_Deque()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddFront(42);

            // Act
            var result = deque.RemoveFront();

            // Assert
            Assert.Equal(42, result);
            Assert.True(deque.IsEmpty());
            Assert.Equal(0, deque.Count);
        }

        [Fact]
        public void Remove_Front_Multiple_Items_Returns_In_Correct_Order()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddFront(1);
            deque.AddFront(2);
            deque.AddFront(3);

            // Act & Assert
            Assert.Equal(3, deque.RemoveFront());
            Assert.Equal(2, deque.RemoveFront());
            Assert.Equal(1, deque.RemoveFront());
            Assert.True(deque.IsEmpty());
        }

        [Fact]
        public void Remove_Rear_Empty_Deque_Throws_Exception()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => deque.RemoveRear());
            Assert.Equal("Deque is empty", exception.Message);
        }

        [Fact]
        public void Remove_Rear_Single_Item_Empties_Deque()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddRear(42);

            // Act
            var result = deque.RemoveRear();

            // Assert
            Assert.Equal(42, result);
            Assert.True(deque.IsEmpty());
            Assert.Equal(0, deque.Count);
        }

        [Fact]
        public void Remove_Rear_Multiple_Items_Returns_In_Correct_Order()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddRear(1);
            deque.AddRear(2);
            deque.AddRear(3);

            // Act & Assert
            Assert.Equal(3, deque.RemoveRear());
            Assert.Equal(2, deque.RemoveRear());
            Assert.Equal(1, deque.RemoveRear());
            Assert.True(deque.IsEmpty());
        }

        [Fact]
        public void Peek_Front_Empty_Deque_Throws_Exception()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => deque.PeekFront());
            Assert.Equal("Deque is empty", exception.Message);
        }

        [Fact]
        public void Peek_Front_Non_Empty_Deque_Returns_Front_Without_Removing()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddFront(1);
            deque.AddFront(2);

            // Act
            var peek1 = deque.PeekFront();
            var count1 = deque.Count;

            var peek2 = deque.PeekFront();
            var count2 = deque.Count;

            // Assert
            Assert.Equal(2, peek1);
            Assert.Equal(2, peek2);
            Assert.Equal(2, count1);
            Assert.Equal(2, count2);
        }

        [Fact]
        public void Peek_Rear_Empty_Deque_Throws_Exception()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => deque.PeekRear());
            Assert.Equal("Deque is empty", exception.Message);
        }

        [Fact]
        public void Peek_Rear_Non_Empty_Deque_Returns_Rear_Without_Removing()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddRear(1);
            deque.AddRear(2);

            // Act
            var peek1 = deque.PeekRear();
            var count1 = deque.Count;

            var peek2 = deque.PeekRear();
            var count2 = deque.Count;

            // Assert
            Assert.Equal(2, peek1);
            Assert.Equal(2, peek2);
            Assert.Equal(2, count1);
            Assert.Equal(2, count2);
        }

        [Fact]
        public void Is_Empty_Returns_Correct_State()
        {
            // Arrange
            var deque = new Deque<int>();

            // Assert - Initially empty
            Assert.True(deque.IsEmpty());

            // Act - Add item
            deque.AddFront(1);

            // Assert - Not empty
            Assert.False(deque.IsEmpty());

            // Act - Remove item
            deque.RemoveFront();

            // Assert - Empty again
            Assert.True(deque.IsEmpty());
        }

        [Fact]
        public void Count_Returns_Correct_Number()
        {
            // Arrange
            var deque = new Deque<int>();

            // Assert - Initial count
            Assert.Equal(0, deque.Count);

            // Act - Add items
            deque.AddFront(1);
            deque.AddRear(2);
            deque.AddFront(3);

            // Assert - After additions
            Assert.Equal(3, deque.Count);

            // Act - Remove items
            deque.RemoveFront();

            // Assert - After removal
            Assert.Equal(2, deque.Count);
        }

        [Fact]
        public void Mixed_Operations_Work_Correctly()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act - Mixed operations
            deque.AddFront(1);  // [1]
            deque.AddRear(2);   // [1, 2]
            deque.AddFront(3);  // [3, 1, 2]
            deque.AddRear(4);   // [3, 1, 2, 4]

            // Assert
            Assert.Equal(3, deque.PeekFront());
            Assert.Equal(4, deque.PeekRear());
            Assert.Equal(4, deque.Count);

            // Act - Remove operations
            Assert.Equal(3, deque.RemoveFront()); // [1, 2, 4]
            Assert.Equal(4, deque.RemoveRear());  // [1, 2]
            Assert.Equal(1, deque.RemoveFront()); // [2]
            Assert.Equal(2, deque.RemoveRear());  // []

            // Assert - Empty
            Assert.True(deque.IsEmpty());
            Assert.Equal(0, deque.Count);
        }

        [Fact]
        public void Deque_With_Strings_Works_Correctly()
        {
            // Arrange
            var deque = new Deque<string>();

            // Act
            deque.AddFront("world");
            deque.AddFront("hello");
            deque.AddRear("!");

            // Assert
            Assert.Equal("hello", deque.PeekFront());
            Assert.Equal("!", deque.PeekRear());
            Assert.Equal(3, deque.Count);

            // Act & Assert
            Assert.Equal("hello", deque.RemoveFront());
            Assert.Equal("!", deque.RemoveRear());
            Assert.Equal("world", deque.RemoveFront());
        }

        [Fact]
        public void Complex_Scenario_All_Operations_Work_Together()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert - Phase 1: Add to both ends
            deque.AddFront(10);
            deque.AddRear(20);
            deque.AddFront(5);
            deque.AddRear(25);

            Assert.Equal(5, deque.PeekFront());
            Assert.Equal(25, deque.PeekRear());
            Assert.Equal(4, deque.Count);

            // Act & Assert - Phase 2: Remove from both ends
            Assert.Equal(5, deque.RemoveFront());
            Assert.Equal(25, deque.RemoveRear());
            Assert.Equal(10, deque.RemoveFront());
            Assert.Equal(20, deque.RemoveRear());

            Assert.True(deque.IsEmpty());
            Assert.Equal(0, deque.Count);

            // Act & Assert - Phase 3: Add after emptying
            deque.AddRear(100);
            deque.AddFront(50);

            Assert.Equal(50, deque.PeekFront());
            Assert.Equal(100, deque.PeekRear());
            Assert.Equal(2, deque.Count);
        }

        [Fact]
        public void Single_Element_Deque_Operations_Work_Correctly()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddFront(42);

            // Assert - Single element state
            Assert.Equal(42, deque.PeekFront());
            Assert.Equal(42, deque.PeekRear());
            Assert.Equal(1, deque.Count);
            Assert.False(deque.IsEmpty());

            // Act & Assert - Remove front
            var result = deque.RemoveFront();
            Assert.Equal(42, result);
            Assert.True(deque.IsEmpty());

            // Reset
            deque.AddRear(99);

            // Act & Assert - Remove rear
            result = deque.RemoveRear();
            Assert.Equal(99, result);
            Assert.True(deque.IsEmpty());
        }
    }
}
namespace Circular_Queue.Tests
{
    public class Circular_Queue_Tests
    {
        [Fact]
        public void Constructor_WithValidSize_CreatesEmptyQueue()
        {
            // Arrange & Act
            var queue = new Circular_Queue<int>(5);

            // Assert
            Assert.Equal(0, queue.Length);
            Assert.True(queue.IsEmpty());
            Assert.False(queue.IsFull());
        }

        [Fact]
        public void Constructor_WithZeroSize_ThrowsException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Circular_Queue<int>(0));
        }

        [Fact]
        public void Constructor_WithNegativeSize_ThrowsException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Circular_Queue<int>(-1));
        }

        [Fact]
        public void Enqueue_ToEmptyQueue_AddsItem()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);

            // Act
            queue.Enqueue(10);

            // Assert
            Assert.Equal(1, queue.Length);
            Assert.Equal(10, queue.Peek());
            Assert.False(queue.IsEmpty());
            Assert.False(queue.IsFull());
        }

        [Fact]
        public void Enqueue_MultipleItems_AddsInOrder()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);

            // Act
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Assert
            Assert.Equal(3, queue.Length);
            Assert.Equal(1, queue.Peek());
            Assert.True(queue.IsFull());
        }

        [Fact]
        public void Enqueue_ToFullQueue_ThrowsException()
        {
            // Arrange
            var queue = new Circular_Queue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => queue.Enqueue(3));
        }

        [Fact]
        public void Enqueue_CircularBehavior_WrapsAround()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Act - Dequeue to make space
            var item1 = queue.Dequeue(); // Remove 1
            queue.Enqueue(4); // Should wrap around

            // Assert
            Assert.Equal(1, item1);
            Assert.Equal(3, queue.Length);
            Assert.Equal(2, queue.Peek());
            Assert.True(queue.IsFull());
        }

        [Fact]
        public void Dequeue_FromEmptyQueue_ThrowsException()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Fact]
        public void Dequeue_SingleItem_ReturnsItemAndEmptiesQueue()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);
            queue.Enqueue(42);

            // Act
            var result = queue.Dequeue();

            // Assert
            Assert.Equal(42, result);
            Assert.Equal(0, queue.Length);
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void Dequeue_MultipleItems_ReturnsInFIFOOrder()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Act & Assert
            Assert.Equal(1, queue.Dequeue());
            Assert.Equal(2, queue.Dequeue());
            Assert.Equal(3, queue.Dequeue());
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void Dequeue_CircularBehavior_WrapsAroundCorrectly()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Act
            var item1 = queue.Dequeue(); // 1
            var item2 = queue.Dequeue(); // 2
            queue.Enqueue(4);
            queue.Enqueue(5);

            // Assert
            Assert.Equal(1, item1);
            Assert.Equal(2, item2);
            Assert.Equal(3, queue.Length);
            Assert.Equal(3, queue.Peek());
            Assert.True(queue.IsFull());

            // Verify order after wrap-around
            Assert.Equal(3, queue.Dequeue());
            Assert.Equal(4, queue.Dequeue());
            Assert.Equal(5, queue.Dequeue());
        }

        [Fact]
        public void Peek_EmptyQueue_ThrowsException()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }

        [Fact]
        public void Peek_ReturnsFirstItemWithoutRemoving()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);
            queue.Enqueue(10);
            queue.Enqueue(20);

            // Act
            var result = queue.Peek();
            var lengthAfterPeek = queue.Length;

            // Assert
            Assert.Equal(10, result);
            Assert.Equal(2, lengthAfterPeek);
        }

        [Fact]
        public void Peek_MultipleCalls_ReturnsSameValue()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);
            queue.Enqueue(5);

            // Act & Assert
            Assert.Equal(5, queue.Peek());
            Assert.Equal(5, queue.Peek());
            Assert.Equal(1, queue.Length);
        }

        [Fact]
        public void IsEmpty_NewQueue_ReturnsTrue()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);

            // Act & Assert
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void IsEmpty_AfterEnqueue_ReturnsFalse()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);
            queue.Enqueue(1);

            // Act & Assert
            Assert.False(queue.IsEmpty());
        }

        [Fact]
        public void IsEmpty_AfterEnqueueDequeue_ReturnsTrue()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);
            queue.Enqueue(1);
            queue.Dequeue();

            // Act & Assert
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void IsFull_NewQueue_ReturnsFalse()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);

            // Act & Assert
            Assert.False(queue.IsFull());
        }

        [Fact]
        public void IsFull_WhenCapacityReached_ReturnsTrue()
        {
            // Arrange
            var queue = new Circular_Queue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            // Act & Assert
            Assert.True(queue.IsFull());
        }

        [Fact]
        public void IsFull_AfterDequeue_ReturnsFalse()
        {
            // Arrange
            var queue = new Circular_Queue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();

            // Act & Assert
            Assert.False(queue.IsFull());
        }

        [Fact]
        public void Length_ReflectsCorrectCount()
        {
            // Arrange
            var queue = new Circular_Queue<int>(3);

            // Act & Assert
            Assert.Equal(0, queue.Length);

            queue.Enqueue(1);
            Assert.Equal(1, queue.Length);

            queue.Enqueue(2);
            Assert.Equal(2, queue.Length);

            queue.Dequeue();
            Assert.Equal(1, queue.Length);

            queue.Dequeue();
            Assert.Equal(0, queue.Length);
        }

        [Fact]
        public void ComplexScenario_MixedOperations()
        {
            // Arrange
            var queue = new Circular_Queue<int>(4);

            // Act & Assert
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Assert.Equal(1, queue.Dequeue());
            Assert.Equal(2, queue.Dequeue());

            queue.Enqueue(4);
            queue.Enqueue(5);

            Assert.Equal(3, queue.Peek());
            Assert.Equal(3, queue.Length);

            Assert.Equal(3, queue.Dequeue());
            Assert.Equal(4, queue.Dequeue());
            Assert.Equal(5, queue.Dequeue());

            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void StressTest_FullCycleOperations()
        {
            // Arrange
            var queue = new Circular_Queue<int>(5);
            const int cycles = 100;

            // Act & Assert
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                // Fill the queue
                for (int i = 0; i < 5; i++)
                {
                    queue.Enqueue(cycle * 10 + i);
                }

                Assert.True(queue.IsFull());

                // Empty the queue
                for (int i = 0; i < 5; i++)
                {
                    Assert.Equal(cycle * 10 + i, queue.Dequeue());
                }

                Assert.True(queue.IsEmpty());
            }
        }

        [Fact]
        public void StringValues_WorkCorrectly()
        {
            // Arrange
            var queue = new Circular_Queue<string>(3);

            // Act
            queue.Enqueue("first");
            queue.Enqueue("second");
            queue.Enqueue("third");

            // Assert
            Assert.Equal("first", queue.Dequeue());
            Assert.Equal("second", queue.Dequeue());
            queue.Enqueue("fourth");
            Assert.Equal("third", queue.Peek());
            Assert.Equal("third", queue.Dequeue());
            Assert.Equal("fourth", queue.Dequeue());
        }

        [Fact]
        public void NullValues_HandledCorrectly()
        {
            // Arrange
            var queue = new Circular_Queue<string>(2);

            // Act
            queue.Enqueue(null);
            queue.Enqueue("not null");

            // Assert
            Assert.Null(queue.Dequeue());
            Assert.Equal("not null", queue.Dequeue());
        }

        [Fact]
        public void CustomObjects_WorkCorrectly()
        {
            // Arrange
            var queue = new Circular_Queue<TestPerson>(2);
            var person1 = new TestPerson("John", 25);
            var person2 = new TestPerson("Jane", 30);

            // Act
            queue.Enqueue(person1);
            queue.Enqueue(person2);

            // Assert
            Assert.Equal(person1, queue.Dequeue());
            Assert.Equal(person2, queue.Peek());
            Assert.Equal(person2, queue.Dequeue());
        }

        [Fact]
        public void ValueTypes_WorkCorrectly()
        {
            // Arrange
            var queue = new Circular_Queue<DateTime>(2);
            var date1 = DateTime.Now;
            var date2 = DateTime.Now.AddDays(1);

            // Act
            queue.Enqueue(date1);
            queue.Enqueue(date2);

            // Assert
            Assert.Equal(date1, queue.Dequeue());
            Assert.Equal(date2, queue.Dequeue());
        }

        // Helper class for testing custom objects
        private class TestPerson
        {
            public string Name { get; }
            public int Age { get; }

            public TestPerson(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public override bool Equals(object obj)
            {
                return obj is TestPerson person &&
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
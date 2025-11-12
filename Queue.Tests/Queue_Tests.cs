namespace Queue.Tests
{
    public class Queue_Tests
    {
        [Fact]
        public void Constructor_Default_CreatesEmptyQueue()
        {
            // Arrange & Act
            var queue = new Queue<int>();

            // Assert
            Assert.Equal(0, queue.Count);
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void Constructor_WithValue_CreatesQueueWithOneElement()
        {
            // Arrange & Act
            var queue = new Queue<int>(42);

            // Assert
            Assert.Equal(1, queue.Count);
            Assert.Equal(42, queue.Peek());
            Assert.False(queue.IsEmpty());
        }

        [Fact]
        public void Constructor_WithNullValue_ThrowsNoException()
        {
            // Arrange & Act
            var queue = new Queue<string>(null);

            // Assert
            Assert.Equal(1, queue.Count);
            Assert.Null(queue.Peek());
        }

        [Fact]
        public void Enqueue_ToEmptyQueue_AddsElement()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act
            queue.Enqueue(10);

            // Assert
            Assert.Equal(1, queue.Count);
            Assert.Equal(10, queue.Peek());
        }

        [Fact]
        public void Enqueue_MultipleElements_AddsInCorrectOrder()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Assert
            Assert.Equal(3, queue.Count);
            Assert.Equal(1, queue.Peek()); // First in should be first out
        }

        [Fact]
        public void Enqueue_NullValue_HandlesCorrectly()
        {
            // Arrange
            var queue = new Queue<string>();

            // Act
            queue.Enqueue(null);

            // Assert
            Assert.Equal(1, queue.Count);
            Assert.Null(queue.Peek());
        }

        [Fact]
        public void Enqueue_IncreasesCount()
        {
            // Arrange
            var queue = new Queue<int>();
            var initialCount = queue.Count;

            // Act
            queue.Enqueue(100);

            // Assert
            Assert.Equal(initialCount + 1, queue.Count);
        }

        [Fact]
        public void Dequeue_FromEmptyQueue_ThrowsException()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Fact]
        public void Dequeue_SingleElementQueue_ReturnsElementAndEmptiesQueue()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(42);

            // Act
            var result = queue.Dequeue();

            // Assert
            Assert.Equal(42, result);
            Assert.Equal(0, queue.Count);
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void Dequeue_MultipleElements_ReturnsInFIFOOrder()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Act & Assert
            Assert.Equal(1, queue.Dequeue());
            Assert.Equal(2, queue.Dequeue());
            Assert.Equal(3, queue.Dequeue());
        }

        [Fact]
        public void Dequeue_DecreasesCount()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            var initialCount = queue.Count;

            // Act
            queue.Dequeue();

            // Assert
            Assert.Equal(initialCount - 1, queue.Count);
        }

        [Fact]
        public void Dequeue_UntilEmpty_ThenThrowsException()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);

            // Act
            queue.Dequeue();
            queue.Dequeue();

            // Assert
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Fact]
        public void Peek_EmptyQueue_ThrowsException()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }

        [Fact]
        public void Peek_ReturnsFirstElementWithoutRemoving()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);

            // Act
            var result = queue.Peek();
            var countAfterPeek = queue.Count;

            // Assert
            Assert.Equal(10, result);
            Assert.Equal(2, countAfterPeek); // Count should not change
        }

        [Fact]
        public void Peek_MultipleCalls_ReturnsSameValue()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(5);

            // Act & Assert
            Assert.Equal(5, queue.Peek());
            Assert.Equal(5, queue.Peek()); // Should return same value
            Assert.Equal(1, queue.Count);  // Count unchanged
        }

        [Fact]
        public void Peek_AfterEnqueueDequeue_ReturnsCorrectElement()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue(); // Remove 1

            // Act
            var result = queue.Peek();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void IsEmpty_NewQueue_ReturnsTrue()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act & Assert
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void IsEmpty_AfterEnqueue_ReturnsFalse()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);

            // Act & Assert
            Assert.False(queue.IsEmpty());
        }

        [Fact]
        public void IsEmpty_AfterEnqueueDequeue_ReturnsTrue()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Dequeue();

            // Act & Assert
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void Count_Operations_ReflectsCorrectState()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act & Assert
            Assert.Equal(0, queue.Count);

            queue.Enqueue(1);
            Assert.Equal(1, queue.Count);

            queue.Enqueue(2);
            Assert.Equal(2, queue.Count);

            queue.Dequeue();
            Assert.Equal(1, queue.Count);

            queue.Dequeue();
            Assert.Equal(0, queue.Count);
        }

        [Fact]
        public void ComplexScenario_MixedOperations_WorksCorrectly()
        {
            // Arrange
            var queue = new Queue<string>();

            // Act & Assert
            queue.Enqueue("first");
            queue.Enqueue("second");

            Assert.Equal("first", queue.Dequeue());
            Assert.Equal(1, queue.Count);

            queue.Enqueue("third");
            Assert.Equal("second", queue.Peek());
            Assert.Equal(2, queue.Count);

            Assert.Equal("second", queue.Dequeue());
            Assert.Equal("third", queue.Dequeue());
            Assert.True(queue.IsEmpty());
        }

        [Fact]
        public void StressTest_ManyOperations_WorksCorrectly()
        {
            // Arrange
            var queue = new Queue<int>();
            const int iterations = 1000;

            // Act - Enqueue many items
            for (int i = 0; i < iterations; i++)
            {
                queue.Enqueue(i);
            }

            // Assert - Dequeue and verify order
            for (int i = 0; i < iterations; i++)
            {
                Assert.Equal(i, queue.Dequeue());
            }

            Assert.True(queue.IsEmpty());
            Assert.Equal(0, queue.Count);
        }

        [Fact]
        public void ValueTypes_WorkCorrectly()
        {
            // Arrange
            var queue = new Queue<DateTime>();
            var date1 = DateTime.Now;
            var date2 = DateTime.Now.AddDays(1);

            // Act
            queue.Enqueue(date1);
            queue.Enqueue(date2);

            // Assert
            Assert.Equal(date1, queue.Dequeue());
            Assert.Equal(date2, queue.Peek());
        }

        [Fact]
        public void Dequeue_SingleElement_SetsFrontAndRearToNull()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);

            // Act
            var result = queue.Dequeue();

            // Assert
            Assert.Equal(1, result);
            Assert.True(queue.IsEmpty());

            // Verify internal state by trying to enqueue again
            queue.Enqueue(2);
            Assert.Equal(1, queue.Count);
            Assert.Equal(2, queue.Peek());
        }

        [Fact]
        public void MultipleEnqueueDequeueOperations_MaintainCorrectState()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act & Assert - Multiple cycles
            queue.Enqueue(1);
            queue.Enqueue(2);
            Assert.Equal(1, queue.Dequeue());

            queue.Enqueue(3);
            queue.Enqueue(4);
            Assert.Equal(2, queue.Dequeue());
            Assert.Equal(3, queue.Dequeue());

            queue.Enqueue(5);
            Assert.Equal(4, queue.Peek());
            Assert.Equal(2, queue.Count);
        }
    }
}
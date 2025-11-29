namespace Priority_Queue.Tests
{

    public class PriorityQueueTests
    {
        [Fact]
        public void Constructor_Creates_Empty_Priority_Queue()
        {
            // Arrange & Act
            var pq = new Priority_Queue<int>();

            // Assert
            Assert.True(pq.IsEmpty());
            Assert.Equal(0, pq.Size());
        }

        [Fact]
        public void Constructor_With_List_Builds_Valid_Priority_Queue()
        {
            // Arrange
            var list = new List<int> { 5, 3, 8, 1, 2 };

            // Act
            var pq = new Priority_Queue<int>(list);

            // Assert
            Assert.False(pq.IsEmpty());
            Assert.Equal(5, pq.Size());
            Assert.Equal(1, pq.Peek());
        }

        [Fact]
        public void Enqueue_Single_Item_Works()
        {
            // Arrange
            var pq = new Priority_Queue<int>();

            // Act
            pq.Enqueue(10);

            // Assert
            Assert.False(pq.IsEmpty());
            Assert.Equal(1, pq.Size());
            Assert.Equal(10, pq.Peek());
        }

        [Fact]
        public void Enqueue_Multiple_Items_Maintains_Priority_Order()
        {
            // Arrange
            var pq = new Priority_Queue<int>();

            // Act
            pq.Enqueue(5);
            pq.Enqueue(3);
            pq.Enqueue(8);
            pq.Enqueue(1);
            pq.Enqueue(2);

            // Assert
            Assert.Equal(5, pq.Size());
            Assert.Equal(1, pq.Peek());
        }

        [Fact]
        public void Dequeue_Empty_Queue_Throws_Exception()
        {
            // Arrange
            var pq = new Priority_Queue<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => pq.Dequeue());
            Assert.Contains("empty", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Dequeue_Single_Item_Returns_Item_And_Empties_Queue()
        {
            // Arrange
            var pq = new Priority_Queue<int>();
            pq.Enqueue(42);

            // Act
            var result = pq.Dequeue();

            // Assert
            Assert.Equal(42, result);
            Assert.True(pq.IsEmpty());
            Assert.Equal(0, pq.Size());
        }

        [Fact]
        public void Dequeue_Multiple_Items_Returns_In_Priority_Order()
        {
            // Arrange
            var pq = new Priority_Queue<int>();
            pq.Enqueue(5);
            pq.Enqueue(3);
            pq.Enqueue(8);
            pq.Enqueue(1);
            pq.Enqueue(2);

            // Act & Assert
            Assert.Equal(1, pq.Dequeue());
            Assert.Equal(2, pq.Dequeue());
            Assert.Equal(3, pq.Dequeue());
            Assert.Equal(5, pq.Dequeue());
            Assert.Equal(8, pq.Dequeue());
            Assert.True(pq.IsEmpty());
        }

        [Fact]
        public void Peek_Empty_Queue_Throws_Exception()
        {
            // Arrange
            var pq = new Priority_Queue<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => pq.Peek());
            Assert.Contains("empty", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Peek_Non_Empty_Queue_Returns_Min_Without_Removing()
        {
            // Arrange
            var pq = new Priority_Queue<int>();
            pq.Enqueue(5);
            pq.Enqueue(3);
            pq.Enqueue(8);

            // Act
            var peek1 = pq.Peek();
            var size1 = pq.Size();

            var peek2 = pq.Peek();
            var size2 = pq.Size();

            // Assert
            Assert.Equal(3, peek1);
            Assert.Equal(3, peek2);
            Assert.Equal(3, size1);
            Assert.Equal(3, size2);
        }

        [Fact]
        public void Is_Empty_Returns_Correct_State()
        {
            // Arrange
            var pq = new Priority_Queue<int>();

            // Assert - Initially empty
            Assert.True(pq.IsEmpty());

            // Act - Add item
            pq.Enqueue(1);

            // Assert - Not empty
            Assert.False(pq.IsEmpty());

            // Act - Remove item
            pq.Dequeue();

            // Assert - Empty again
            Assert.True(pq.IsEmpty());
        }

        [Fact]
        public void Size_Returns_Correct_Number()
        {
            // Arrange
            var pq = new Priority_Queue<int>();

            // Assert - Initial size
            Assert.Equal(0, pq.Size());

            // Act - Add items
            pq.Enqueue(1);
            pq.Enqueue(2);
            pq.Enqueue(3);

            // Assert - After additions
            Assert.Equal(3, pq.Size());

            // Act - Remove items
            pq.Dequeue();

            // Assert - After removal
            Assert.Equal(2, pq.Size());
        }

        [Fact]
        public void Mixed_Operations_Work_Correctly()
        {
            // Arrange
            var pq = new Priority_Queue<int>();

            // Act - Mixed operations
            pq.Enqueue(10);
            pq.Enqueue(5);
            pq.Enqueue(15);

            var first = pq.Dequeue();
            pq.Enqueue(3);
            pq.Enqueue(7);

            var second = pq.Dequeue();
            var third = pq.Dequeue();
            pq.Enqueue(1);

            var fourth = pq.Dequeue();
            var fifth = pq.Dequeue();
            var sixth = pq.Dequeue();

            // Assert
            Assert.Equal(5, first);
            Assert.Equal(3, second);
            Assert.Equal(7, third);
            Assert.Equal(1, fourth);
            Assert.Equal(10, fifth);
            Assert.Equal(15, sixth);
            Assert.True(pq.IsEmpty());
        }

        [Fact]
        public void Priority_Queue_With_Strings_Works_Correctly()
        {
            // Arrange
            var pq = new Priority_Queue<string>();

            // Act
            pq.Enqueue("banana");
            pq.Enqueue("apple");
            pq.Enqueue("cherry");
            pq.Enqueue("date");

            // Assert
            Assert.Equal("apple", pq.Peek());
            Assert.Equal("apple", pq.Dequeue());
            Assert.Equal("banana", pq.Dequeue());
            Assert.Equal("cherry", pq.Dequeue());
            Assert.Equal("date", pq.Dequeue());
        }

        [Fact]
        public void Priority_Queue_With_Custom_Objects_Works_Correctly()
        {
            // Arrange
            var pq = new Priority_Queue<Person>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 20);

            // Act
            pq.Enqueue(person1);
            pq.Enqueue(person2);
            pq.Enqueue(person3);

            // Assert
            Assert.Equal(person3, pq.Peek()); // Youngest first (lowest age)
            Assert.Equal(person3, pq.Dequeue());
            Assert.Equal(person1, pq.Dequeue());
            Assert.Equal(person2, pq.Dequeue());
            Assert.True(pq.IsEmpty());
        }

        [Fact]
        public void Large_Priority_Queue_Operations_Perform_Correctly()
        {
            // Arrange
            var pq = new Priority_Queue<int>();
            var random = new Random(42);
            const int count = 1000;

            // Act - Insert random values
            for (int i = 0; i < count; i++)
            {
                pq.Enqueue(random.Next(1, 10000));
            }

            // Assert
            Assert.Equal(count, pq.Size());

            // Act - Dequeue all values
            int previous = pq.Dequeue();
            int dequeuedCount = 1;

            while (!pq.IsEmpty())
            {
                int current = pq.Dequeue();
                Assert.True(previous <= current, $"Priority order violated: {previous} > {current}");
                previous = current;
                dequeuedCount++;
            }

            // Assert
            Assert.Equal(count, dequeuedCount);
            Assert.True(pq.IsEmpty());
        }

        [Fact]
        public void Complex_Scenario_All_Methods_Work_Together()
        {
            // Arrange
            var pq = new Priority_Queue<int>();

            // Phase 1: Initial insertions
            pq.Enqueue(100);
            pq.Enqueue(50);
            pq.Enqueue(150);
            pq.Enqueue(25);
            pq.Enqueue(75);

            Assert.Equal(5, pq.Size());
            Assert.Equal(25, pq.Peek());

            // Phase 2: Mixed operations
            Assert.Equal(25, pq.Dequeue());
            Assert.Equal(4, pq.Size());

            pq.Enqueue(10);
            Assert.Equal(10, pq.Peek());
            Assert.Equal(5, pq.Size());

            // Phase 3: Multiple dequeues
            Assert.Equal(10, pq.Dequeue());
            Assert.Equal(50, pq.Dequeue());
            Assert.Equal(75, pq.Dequeue());
            Assert.Equal(2, pq.Size());

            // Phase 4: Final operations
            pq.Enqueue(200);
            pq.Enqueue(30);

            Assert.Equal(30, pq.Peek());
            Assert.Equal(4, pq.Size());

            Assert.Equal(30, pq.Dequeue());
            Assert.Equal(100, pq.Dequeue());
            Assert.Equal(150, pq.Dequeue());
            Assert.Equal(200, pq.Dequeue());

            Assert.True(pq.IsEmpty());
            Assert.Equal(0, pq.Size());
        }

        [Fact]
        public void Priority_Queue_Maintains_Min_Heap_Property()
        {
            // Arrange
            var pq = new Priority_Queue<int>();
            var values = new List<int> { 9, 4, 7, 1, 5, 3, 8, 2, 6 };

            // Act
            foreach (var value in values)
            {
                pq.Enqueue(value);
            }

            // Assert - Should dequeue in ascending order
            Assert.Equal(1, pq.Dequeue());
            Assert.Equal(2, pq.Dequeue());
            Assert.Equal(3, pq.Dequeue());
            Assert.Equal(4, pq.Dequeue());
            Assert.Equal(5, pq.Dequeue());
            Assert.Equal(6, pq.Dequeue());
            Assert.Equal(7, pq.Dequeue());
            Assert.Equal(8, pq.Dequeue());
            Assert.Equal(9, pq.Dequeue());
        }

        [Fact]
        public void Empty_Queue_After_Constructor_With_Empty_List()
        {
            // Arrange
            var emptyList = new List<int>();

            // Act
            var pq = new Priority_Queue<int>(emptyList);

            // Assert
            Assert.True(pq.IsEmpty());
            Assert.Equal(0, pq.Size());
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

            public override string ToString()
            {
                return $"{Name}({Age})";
            }
        }
    }
}
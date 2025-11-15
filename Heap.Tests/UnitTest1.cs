namespace Heap.Tests
{
    public class Heap_Tests
    {
        [Fact]
        public void Constructor_Creates_Empty_Heap()
        {
            // Arrange & Act
            var heap = new Heap<int>();

            // Assert
            Assert.True(heap.IsEmpty());
            Assert.Equal(0, heap.Count);
        }

        [Fact]
        public void Constructor_With_Array_Builds_Valid_Heap()
        {
            // Arrange
            int[] array = [5, 3, 8, 1, 2];

            // Act
            var heap = new Heap<int>(array);

            // Assert
            Assert.Equal(5, heap.Count);
            Assert.Equal(1, heap.Peek());
        }

        [Fact]
        public void Insert_Single_Item_Becomes_Root()
        {
            // Arrange
            var heap = new Heap<int>();

            // Act
            heap.Insert(10);

            // Assert
            Assert.False(heap.IsEmpty());
            Assert.Equal(1, heap.Count);
            Assert.Equal(10, heap.Peek());
        }

        [Fact]
        public void Insert_Multiple_Items_Maintains_Heap_Property()
        {
            // Arrange
            var heap = new Heap<int>();

            // Act
            heap.Insert(5);
            heap.Insert(3);
            heap.Insert(8);
            heap.Insert(1);
            heap.Insert(2);

            // Assert
            Assert.Equal(1, heap.Peek());
            Assert.Equal(5, heap.Count);
        }

        [Fact]
        public void ExtractMin_Empty_Heap_Throws_Exception()
        {
            // Arrange
            var heap = new Heap<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => heap.ExtractMin());
            Assert.Equal("Heap is empty.", exception.Message);
        }

        [Fact]
        public void ExtractMin_Single_Item_Returns_Item_And_Empties_Heap()
        {
            // Arrange
            var heap = new Heap<int>();
            heap.Insert(42);

            // Act
            var result = heap.ExtractMin();

            // Assert
            Assert.Equal(42, result);
            Assert.True(heap.IsEmpty());
            Assert.Equal(0, heap.Count);
        }

        [Fact]
        public void ExtractMin_Multiple_Items_Returns_In_Sorted_Order()
        {
            // Arrange
            var heap = new Heap<int>();
            int[] items = [5, 3, 8, 1, 2, 7, 4, 6];

            foreach (var item in items)
                heap.Insert(item);

            // Act & Assert
            int previous = heap.ExtractMin();
            while (!heap.IsEmpty())
            {
                int current = heap.ExtractMin();
                Assert.True(previous <= current);
                previous = current;
            }
        }

        [Fact]
        public void ExtractMin_Complex_Sequence_Maintains_Heap_eProperty()
        {
            // Arrange
            var heap = new Heap<int>();
            heap.Insert(10);
            heap.Insert(5);
            heap.Insert(15);

            // Act & Assert - First extraction
            var first = heap.ExtractMin();
            Assert.Equal(5, first);
            Assert.Equal(2, heap.Count);

            // Act & Assert - Second extraction
            var second = heap.ExtractMin();
            Assert.Equal(10, second);
            Assert.Equal(1, heap.Count);

            // Act & Assert - Third extraction
            var third = heap.ExtractMin();
            Assert.Equal(15, third);
            Assert.True(heap.IsEmpty());
        }

        [Fact]
        public void Peek_Empty_Heap_Throws_Exception()
        {
            // Arrange
            var heap = new Heap<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => heap.Peek());
            Assert.Equal("Heap is empty.", exception.Message);
        }

        [Fact]
        public void Peek_Non_Empty_Heap_Returns_Min_Without_Removing()
        {
            // Arrange
            var heap = new Heap<int>();
            heap.Insert(5);
            heap.Insert(3);
            heap.Insert(8);

            // Act
            var peek1 = heap.Peek();
            var count1 = heap.Count;

            var peek2 = heap.Peek();
            var count2 = heap.Count;

            // Assert
            Assert.Equal(3, peek1);
            Assert.Equal(3, peek2);
            Assert.Equal(3, count1);
            Assert.Equal(3, count2);
            Assert.Equal(count1, count2);
        }

        [Fact]
        public void IsEmpty_Returns_Correct_State()
        {
            // Arrange
            var heap = new Heap<int>();

            // Assert - Initially empty
            Assert.True(heap.IsEmpty());

            // Act - Add item
            heap.Insert(1);

            // Assert - Not empty
            Assert.False(heap.IsEmpty());

            // Act - Remove item
            heap.ExtractMin();

            // Assert - Empty again
            Assert.True(heap.IsEmpty());
        }

        [Fact]
        public void Count_Returns_Correct_Number()
        {
            // Arrange
            var heap = new Heap<int>();

            // Assert - Initial count
            Assert.Equal(0, heap.Count);

            // Act - Add items
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);

            // Assert - After additions
            Assert.Equal(3, heap.Count);

            // Act - Remove items
            heap.ExtractMin();

            // Assert - After removal
            Assert.Equal(2, heap.Count);
        }

        [Fact]
        public void ToString_Returns_Comma_Separated_Values()
        {
            // Arrange
            var heap = new Heap<int>();
            heap.Insert(3);
            heap.Insert(1);
            heap.Insert(2);

            // Act
            var result = heap.ToString();

            // Assert - The exact order depends on heap structure, but should contain all elements
            Assert.Contains("1", result);
            Assert.Contains("2", result);
            Assert.Contains("3", result);
        }

        [Fact]
        public void Heap_With_Strings_Works_Correctly()
        {
            // Arrange
            var heap = new Heap<string>();

            // Act
            heap.Insert("banana");
            heap.Insert("apple");
            heap.Insert("cherry");

            // Assert
            Assert.Equal("apple", heap.Peek());
            Assert.Equal("apple", heap.ExtractMin());
            Assert.Equal("banana", heap.ExtractMin());
            Assert.Equal("cherry", heap.ExtractMin());
        }

        [Fact]
        public void Heap_With_Custom_Objects_Works_Correctly()
        {
            // Arrange
            var heap = new Heap<TestItem>();
            var item1 = new TestItem(3, "C");
            var item2 = new TestItem(1, "A");
            var item3 = new TestItem(2, "B");

            // Act
            heap.Insert(item1);
            heap.Insert(item2);
            heap.Insert(item3);

            // Assert
            Assert.Equal(item2, heap.Peek());
            Assert.Equal(item2, heap.ExtractMin());
            Assert.Equal(item3, heap.ExtractMin());
            Assert.Equal(item1, heap.ExtractMin());
        }

        [Fact]
        public void Large_Dataset_Heap_Property_Maintained()
        {
            // Arrange
            var heap = new Heap<int>();
            var random = new Random(42);
            var numbers = Enumerable.Range(0, 1000).Select(_ => random.Next(1000)).ToArray();

            // Act
            foreach (var number in numbers)
                heap.Insert(number);

            // Assert
            int previous = heap.ExtractMin();
            int count = 1;

            while (!heap.IsEmpty())
            {
                int current = heap.ExtractMin();
                Assert.True(previous <= current, $"Heap property violated: {previous} > {current} at position {count}");
                previous = current;
                count++;
            }

            Assert.Equal(numbers.Length, count);
        }

        private class TestItem : IComparable<TestItem>
        {
            public int Value { get; }
            public string Name { get; }

            public TestItem(int value, string name)
            {
                Value = value;
                Name = name;
            }

            public int CompareTo(TestItem other)
            {
                return Value.CompareTo(other.Value);
            }

            public override bool Equals(object obj)
            {
                return obj is TestItem other && Value == other.Value && Name == other.Name;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Value, Name);
            }

            public override string ToString()
            {
                return $"{Name}({Value})";
            }
        }
    }
}
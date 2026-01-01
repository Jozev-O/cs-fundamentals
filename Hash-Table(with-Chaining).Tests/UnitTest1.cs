namespace Hash_Table_with_Chaining_.Tests
{
    public class HashTableTests
    {
        [Fact]
        public void Constructor_Default_Creates_Table_With_Default_Capacity()
        {
            // Arrange & Act
            var hashTable = new HashTable<string, int>();

            // Assert
            Assert.Equal(0, hashTable.Count);
            // Проверяем что можно добавить элементы
            hashTable.Add("key1", 1);
            Assert.Equal(1, hashTable.Count);
        }

        [Fact]
        public void Constructor_With_Initial_Capacity_Creates_Table_With_Specified_Capacity()
        {
            // Arrange & Act
            var hashTable = new HashTable<string, int>(32);

            // Assert
            hashTable.Add("key1", 1);
            hashTable.Add("key2", 2);
            hashTable.Add("key3", 3);
            Assert.Equal(3, hashTable.Count);
        }

        [Fact]
        public void Add_Null_Key_Throws_ArgumentNullException()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => hashTable.Add(null, 1));
        }

        [Fact]
        public void Add_New_Key_Inserts_Value()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();

            // Act
            hashTable.Add("key1", 100);

            // Assert
            Assert.Equal(100, hashTable.Get("key1"));
            Assert.Equal(1, hashTable.Count);
        }

        [Fact]
        public void Add_Existing_Key_Updates_Value()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 100);

            // Act
            hashTable.Add("key1", 200);

            // Assert
            Assert.Equal(200, hashTable.Get("key1"));
            Assert.Equal(1, hashTable.Count); // Count не должен увеличиться
        }

        [Fact]
        public void Get_Null_Key_Throws_ArgumentNullException()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => hashTable.Get(null));
        }

        [Fact]
        public void Get_Existing_Key_Returns_Correct_Value()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 100);
            hashTable.Add("key2", 200);
            hashTable.Add("key3", 300);

            // Act & Assert
            Assert.Equal(100, hashTable.Get("key1"));
            Assert.Equal(200, hashTable.Get("key2"));
            Assert.Equal(300, hashTable.Get("key3"));
        }

        [Fact]
        public void Get_Non_Existent_Key_Throws_KeyNotFoundException()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 100);

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => hashTable.Get("nonexistent"));
            Assert.Contains("not found", exception.Message);
        }

        [Fact]
        public void Remove_Existing_Key_Returns_True_And_Deletes_Entry()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 100);
            hashTable.Add("key2", 200);

            // Act
            var result = hashTable.Remove("key1");

            // Assert
            Assert.True(result);
            Assert.Equal(1, hashTable.Count);
            Assert.Throws<KeyNotFoundException>(() => hashTable.Get("key1"));
            Assert.Equal(200, hashTable.Get("key2"));
        }

        [Fact]
        public void Remove_Non_Existent_Key_Returns_False()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 100);

            // Act
            var result = hashTable.Remove("nonexistent");

            // Assert
            Assert.False(result);
            Assert.Equal(1, hashTable.Count);
            Assert.Equal(100, hashTable.Get("key1"));
        }

        [Fact]
        public void ContainsKey_Returns_True_For_Existing_Key()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 100);
            hashTable.Add("key2", 200);

            // Act & Assert
            Assert.True(hashTable.ContainsKey("key1"));
            Assert.True(hashTable.ContainsKey("key2"));
        }

        [Fact]
        public void ContainsKey_Returns_False_For_Non_Existent_Key()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 100);

            // Act & Assert
            Assert.False(hashTable.ContainsKey("nonexistent"));
            Assert.False(hashTable.ContainsKey("key2"));
        }

        [Fact]
        public void ContainsKey_Returns_False_For_Null_Key()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();

            // Act & Assert
            // В текущей реализации GetIndex бросит исключение для null,
            // но ContainsKey также должен бросать ArgumentNullException
            Assert.Throws<ArgumentNullException>(() => hashTable.ContainsKey(null));
        }

        [Fact]
        public void GetIndex_Returns_Valid_Index_Range()
        {
            // Arrange
            var hashTable = new HashTable<string, int>(16);

            // Act
            var index1 = hashTable.GetIndex("key1");
            var index2 = hashTable.GetIndex("key2");
            var index3 = hashTable.GetIndex("anotherKey");

            // Assert
            Assert.InRange(index1, 0, 15);
            Assert.InRange(index2, 0, 15);
            Assert.InRange(index3, 0, 15);
        }

        [Fact]
        public void GetIndex_Null_Key_Throws_ArgumentNullException()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => hashTable.GetIndex(null));
        }

        [Fact]
        public void Collisions_Are_Handled_Correctly()
        {
            // Arrange
            var hashTable = new HashTable<int, string>();

            // Создаем коллизии специальными ключами
            // Эти ключи могут иметь одинаковый хеш
            hashTable.Add(1, "value1");
            hashTable.Add(17, "value17"); // Может попасть в тот же bucket если capacity = 16

            // Act & Assert
            Assert.Equal("value1", hashTable.Get(1));
            Assert.Equal("value17", hashTable.Get(17));
            Assert.Equal(2, hashTable.Count);
        }

        [Fact]
        public void Resize_Triggers_When_Load_Factor_Exceeded()
        {
            // Arrange
            var hashTable = new HashTable<string, int>(8);
            // Load factor threshold = 0.75, значит при 6 элементах должно произойти расширение

            // Act
            for (int i = 0; i < 6; i++)
            {
                hashTable.Add($"key{i}", i);
            }

            // Assert - Проверяем что все элементы доступны после resize
            for (int i = 0; i < 6; i++)
            {
                Assert.Equal(i, hashTable.Get($"key{i}"));
            }
            Assert.Equal(6, hashTable.Count);
        }

        [Fact]
        public void Resize_Maintains_All_Elements()
        {
            // Arrange
            var hashTable = new HashTable<string, string>(4);
            var testData = new Dictionary<string, string>
            {
                ["a"] = "apple",
                ["b"] = "banana",
                ["c"] = "cherry",
                ["d"] = "date",
                ["e"] = "elderberry", // Должен вызвать resize
                ["f"] = "fig",
                ["g"] = "grape",
                ["h"] = "honeydew"
            };

            // Act
            foreach (var kvp in testData)
            {
                hashTable.Add(kvp.Key, kvp.Value);
            }

            // Assert
            Assert.Equal(testData.Count, hashTable.Count);
            foreach (var kvp in testData)
            {
                Assert.Equal(kvp.Value, hashTable.Get(kvp.Key));
            }
        }

        [Fact]
        public void Clear_Removes_All_Elements()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 1);
            hashTable.Add("key2", 2);
            hashTable.Add("key3", 3);
            Assert.Equal(3, hashTable.Count);

            // Act
            hashTable.Clear();

            // Assert
            Assert.Equal(0, hashTable.Count);
            Assert.Throws<KeyNotFoundException>(() => hashTable.Get("key1"));
            Assert.Throws<KeyNotFoundException>(() => hashTable.Get("key2"));
            Assert.Throws<KeyNotFoundException>(() => hashTable.Get("key3"));
        }

        [Fact]
        public void Clear_Preserves_Capacity()
        {
            // Arrange
            var hashTable = new HashTable<string, int>(32);
            for (int i = 0; i < 20; i++)
            {
                hashTable.Add($"key{i}", i);
            }

            // Act
            hashTable.Clear();

            // Assert - Можно снова добавить элементы
            hashTable.Add("newKey", 100);
            Assert.Equal(1, hashTable.Count);
            Assert.Equal(100, hashTable.Get("newKey"));
        }

        [Fact]
        public void ToString_Returns_Non_Empty_String()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();
            hashTable.Add("key1", 100);
            hashTable.Add("key2", 200);

            // Act
            var result = hashTable.ToString();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("key1", result);
            Assert.Contains("key2", result);
            Assert.Contains("100", result);
            Assert.Contains("200", result);
        }

        [Fact]
        public void Complex_Operations_Work_Correctly()
        {
            // Arrange
            var hashTable = new HashTable<string, int>();

            // Act - Добавляем элементы
            hashTable.Add("a", 1);
            hashTable.Add("b", 2);
            hashTable.Add("c", 3);
            hashTable.Add("d", 4);

            // Assert - Проверяем добавление
            Assert.Equal(4, hashTable.Count);
            Assert.True(hashTable.ContainsKey("a"));
            Assert.True(hashTable.ContainsKey("b"));
            Assert.True(hashTable.ContainsKey("c"));
            Assert.True(hashTable.ContainsKey("d"));

            // Act - Обновляем элемент
            hashTable.Add("b", 20);

            // Assert - Проверяем обновление
            Assert.Equal(4, hashTable.Count); // Count не изменился
            Assert.Equal(20, hashTable.Get("b"));

            // Act - Удаляем элемент
            var removed = hashTable.Remove("c");

            // Assert - Проверяем удаление
            Assert.True(removed);
            Assert.Equal(3, hashTable.Count);
            Assert.False(hashTable.ContainsKey("c"));
            Assert.Throws<KeyNotFoundException>(() => hashTable.Get("c"));

            // Act - Добавляем новый элемент после удаления
            hashTable.Add("e", 5);

            // Assert - Проверяем добавление нового элемента
            Assert.Equal(4, hashTable.Count);
            Assert.Equal(5, hashTable.Get("e"));
        }

        [Fact]
        public void Stress_Test_With_Many_Elements()
        {
            // Arrange
            var hashTable = new HashTable<int, string>();
            const int count = 1000;

            // Act - Добавляем много элементов
            for (int i = 0; i < count; i++)
            {
                hashTable.Add(i, $"value{i}");
            }

            // Assert - Проверяем что все элементы доступны
            Assert.Equal(count, hashTable.Count);
            for (int i = 0; i < count; i++)
            {
                Assert.Equal($"value{i}", hashTable.Get(i));
            }

            // Act - Удаляем половину элементов
            for (int i = 0; i < count / 2; i++)
            {
                hashTable.Remove(i);
            }

            // Assert - Проверяем оставшиеся элементы
            Assert.Equal(count / 2, hashTable.Count);
            for (int i = count / 2; i < count; i++)
            {
                Assert.Equal($"value{i}", hashTable.Get(i));
            }

            // Act - Добавляем новые элементы
            for (int i = count; i < count + 500; i++)
            {
                hashTable.Add(i, $"newvalue{i}");
            }

            // Assert - Проверяем все элементы
            Assert.Equal(count / 2 + 500, hashTable.Count);
            for (int i = count / 2; i < count + 500; i++)
            {
                Assert.Equal(i < count ? $"value{i}" : $"newvalue{i}", hashTable.Get(i));
            }
        }

        [Fact]
        public void Custom_Object_Keys_Work_Correctly()
        {
            // Arrange
            var hashTable = new HashTable<Person, string>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 35);

            // Act
            hashTable.Add(person1, "Engineer");
            hashTable.Add(person2, "Manager");
            hashTable.Add(person3, "Director");

            // Assert
            Assert.Equal(3, hashTable.Count);
            Assert.Equal("Engineer", hashTable.Get(person1));
            Assert.Equal("Manager", hashTable.Get(person2));
            Assert.Equal("Director", hashTable.Get(person3));

            // Act - Обновляем значение
            hashTable.Add(person2, "Senior Manager");

            // Assert
            Assert.Equal("Senior Manager", hashTable.Get(person2));
            Assert.Equal(3, hashTable.Count);

            // Act - Удаляем
            hashTable.Remove(person1);

            // Assert
            Assert.Equal(2, hashTable.Count);
            Assert.False(hashTable.ContainsKey(person1));
            Assert.True(hashTable.ContainsKey(person2));
            Assert.True(hashTable.ContainsKey(person3));
        }

        private class Person
        {
            public string Name { get; }
            public int Age { get; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
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
        }
    }
}
namespace Set.Tests
{
    public class HashSet
    {
        [Fact]
        public void Constructor_Default_Creates_Set_With_Default_Capacity()
        {
            // Arrange & Act
            var set = new Set.HashSet.HashSet<int>();

            // Assert
            Assert.Equal(0, set.Count);
            // Проверяем что можно добавить элементы
            Assert.True(set.Add(1));
            Assert.Equal(1, set.Count);
        }

        [Fact]
        public void Constructor_With_Initial_Capacity_Creates_Set_With_Specified_Capacity()
        {
            // Arrange & Act
            var set = new Set.HashSet.HashSet<int>(32);

            // Assert
            Assert.True(set.Add(1));
            Assert.True(set.Add(2));
            Assert.True(set.Add(3));
            Assert.Equal(3, set.Count);
        }

        [Fact]
        public void Add_Null_Throws_ArgumentNullException()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => set.Add(null));
        }

        [Fact]
        public void Add_New_Element_Returns_True_And_Increases_Count()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();

            // Act
            var result = set.Add(100);

            // Assert
            Assert.True(result);
            Assert.Equal(1, set.Count);
            Assert.True(set.Contains(100));
        }

        [Fact]
        public void Add_Duplicate_Element_Returns_False_And_Does_Not_Increase_Count()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();
            set.Add(100);

            // Act
            var result = set.Add(100);

            // Assert
            Assert.False(result);
            Assert.Equal(1, set.Count);
            Assert.True(set.Contains(100));
        }

        [Fact]
        public void Remove_Existing_Element_Returns_True_And_Decreases_Count()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();
            set.Add(100);
            set.Add(200);

            // Act
            var result = set.Remove(100);

            // Assert
            Assert.True(result);
            Assert.Equal(1, set.Count);
            Assert.False(set.Contains(100));
            Assert.True(set.Contains(200));
        }

        [Fact]
        public void Remove_Non_Existent_Element_Returns_False()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();
            set.Add(100);

            // Act
            var result = set.Remove(200);

            // Assert
            Assert.False(result);
            Assert.Equal(1, set.Count);
            Assert.True(set.Contains(100));
        }

        [Fact]
        public void Contains_Returns_True_For_Existing_Element()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();
            set.Add(100);
            set.Add(200);
            set.Add(300);

            // Act & Assert
            Assert.True(set.Contains(100));
            Assert.True(set.Contains(200));
            Assert.True(set.Contains(300));
        }

        [Fact]
        public void Contains_Returns_False_For_Non_Existent_Element()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();
            set.Add(100);

            // Act & Assert
            Assert.False(set.Contains(200));
            Assert.False(set.Contains(300));
        }

        [Fact]
        public void Contains_Null_Throws_ArgumentNullException()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => set.Contains(null));
        }

        [Fact]
        public void GetIndex_Returns_Valid_Index_Range()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>(16);

            // Act
            var index1 = set.GetIndex(100);
            var index2 = set.GetIndex(200);
            var index3 = set.GetIndex(300);

            // Assert
            Assert.InRange(index1, 0, 15);
            Assert.InRange(index2, 0, 15);
            Assert.InRange(index3, 0, 15);
        }

        [Fact]
        public void GetIndex_Null_Throws_ArgumentNullException()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => set.GetIndex(null));
        }

        [Fact]
        public void Collisions_Are_Handled_Correctly()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();

            // Добавляем элементы, которые могут вызывать коллизии
            set.Add(1);
            set.Add(17); // Может попасть в тот же bucket если capacity = 16

            // Act & Assert
            Assert.True(set.Contains(1));
            Assert.True(set.Contains(17));
            Assert.Equal(2, set.Count);
        }

        [Fact]
        public void Resize_Triggers_When_Load_Factor_Exceeded()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>(8);
            // Load factor threshold = 0.75, значит при 6 элементах должно произойти расширение

            // Act
            for (int i = 0; i < 6; i++)
            {
                set.Add(i);
            }

            // Assert - Проверяем что все элементы доступны после resize
            for (int i = 0; i < 6; i++)
            {
                Assert.True(set.Contains(i));
            }
            Assert.Equal(6, set.Count);
        }

        [Fact]
        public void Resize_Maintains_All_Elements()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<string>(4);
            var testData = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h" };

            // Act
            foreach (var item in testData)
            {
                set.Add(item);
            }

            // Assert
            Assert.Equal(testData.Count, set.Count);
            foreach (var item in testData)
            {
                Assert.True(set.Contains(item));
            }
        }

        [Fact]
        public void Clear_Removes_All_Elements()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(3);
            Assert.Equal(3, set.Count);

            // Act
            set.Clear();

            // Assert
            Assert.Equal(0, set.Count);
            Assert.False(set.Contains(1));
            Assert.False(set.Contains(2));
            Assert.False(set.Contains(3));
        }

        [Fact]
        public void Clear_Preserves_Capacity()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>(32);
            for (int i = 0; i < 20; i++)
            {
                set.Add(i);
            }

            // Act
            set.Clear();

            // Assert - Можно снова добавить элементы
            Assert.True(set.Add(100));
            Assert.Equal(1, set.Count);
            Assert.True(set.Contains(100));
        }

        [Fact]
        public void ToString_Returns_Non_Empty_String()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();
            set.Add(100);
            set.Add(200);

            // Act
            var result = set.ToString();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("100", result);
            Assert.Contains("200", result);
        }

        [Fact]
        public void Complex_Operations_Work_Correctly()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<string>();

            // Act - Добавляем элементы
            Assert.True(set.Add("a"));
            Assert.True(set.Add("b"));
            Assert.True(set.Add("c"));
            Assert.True(set.Add("d"));

            // Assert - Проверяем добавление
            Assert.Equal(4, set.Count);
            Assert.True(set.Contains("a"));
            Assert.True(set.Contains("b"));
            Assert.True(set.Contains("c"));
            Assert.True(set.Contains("d"));

            // Act - Пытаемся добавить дубликат
            Assert.False(set.Add("b"));

            // Assert - Проверяем что Count не изменился
            Assert.Equal(4, set.Count);

            // Act - Удаляем элемент
            Assert.True(set.Remove("c"));

            // Assert - Проверяем удаление
            Assert.Equal(3, set.Count);
            Assert.False(set.Contains("c"));

            // Act - Добавляем новый элемент после удаления
            Assert.True(set.Add("e"));

            // Assert - Проверяем добавление нового элемента
            Assert.Equal(4, set.Count);
            Assert.True(set.Contains("e"));
        }

        [Fact]
        public void Stress_Test_With_Many_Elements()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();
            const int count = 1000;

            // Act - Добавляем много элементов
            for (int i = 0; i < count; i++)
            {
                Assert.True(set.Add(i));
            }

            // Assert - Проверяем что все элементы доступны
            Assert.Equal(count, set.Count);
            for (int i = 0; i < count; i++)
            {
                Assert.True(set.Contains(i));
            }

            // Act - Удаляем половину элементов
            for (int i = 0; i < count / 2; i++)
            {
                Assert.True(set.Remove(i));
            }

            // Assert - Проверяем оставшиеся элементы
            Assert.Equal(count / 2, set.Count);
            for (int i = count / 2; i < count; i++)
            {
                Assert.True(set.Contains(i));
            }

            // Act - Добавляем новые элементы
            for (int i = count; i < count + 500; i++)
            {
                Assert.True(set.Add(i));
            }

            // Assert - Проверяем все элементы
            Assert.Equal(count / 2 + 500, set.Count);
            for (int i = count / 2; i < count + 500; i++)
            {
                Assert.True(set.Contains(i));
            }
        }

        [Fact]
        public void Custom_Objects_Work_Correctly()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<Person>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 35);
            var person4 = new Person("Alice", 25); // Дубликат person1

            // Act
            Assert.True(set.Add(person1));
            Assert.True(set.Add(person2));
            Assert.True(set.Add(person3));
            Assert.False(set.Add(person4)); // Дубликат

            // Assert
            Assert.Equal(3, set.Count);
            Assert.True(set.Contains(person1));
            Assert.True(set.Contains(person2));
            Assert.True(set.Contains(person3));

            // Act - Обновляем объект
            var updatedPerson2 = new Person("Bob", 31); // Другой возраст - другой объект
            Assert.True(set.Add(updatedPerson2));

            // Assert
            Assert.Equal(4, set.Count); // Разные объекты
            Assert.True(set.Contains(person2));
            Assert.True(set.Contains(updatedPerson2));

            // Act - Удаляем
            Assert.True(set.Remove(person1));

            // Assert
            Assert.Equal(3, set.Count);
            Assert.False(set.Contains(person1));
            Assert.True(set.Contains(person2));
            Assert.True(set.Contains(person3));
            Assert.True(set.Contains(updatedPerson2));
        }

        [Fact]
        public void Strings_With_Same_Content_Are_Treated_As_Equal()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<string>();
            var str1 = "hello";
            var str2 = "hello"; // Такое же содержимое

            // Act
            Assert.True(set.Add(str1));
            Assert.False(set.Add(str2));

            // Assert
            Assert.Equal(1, set.Count);
            Assert.True(set.Contains("hello"));
        }

        [Fact]
        public void Integers_With_Same_Value_Are_Treated_As_Equal()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>();

            // Act
            Assert.True(set.Add(100));
            Assert.False(set.Add(100));
            Assert.True(set.Add(200));
            Assert.False(set.Add(200));

            // Assert
            Assert.Equal(2, set.Count);
            Assert.True(set.Contains(100));
            Assert.True(set.Contains(200));
        }

        [Fact]
        public void Large_Number_Of_Collisions_Handled_Correctly()
        {
            // Arrange
            // Создаем объекты с одинаковым хеш-кодом
            var set = new Set.HashSet.HashSet<BadHashObject>();
            const int collisionCount = 50;

            // Act
            for (int i = 0; i < collisionCount; i++)
            {
                Assert.True(set.Add(new BadHashObject(i)));
            }

            // Assert - Все элементы должны быть добавлены
            Assert.Equal(collisionCount, set.Count);

            // Проверяем что все элементы доступны
            for (int i = 0; i < collisionCount; i++)
            {
                Assert.True(set.Contains(new BadHashObject(i)));
            }

            // Удаляем половину
            for (int i = 0; i < collisionCount / 2; i++)
            {
                Assert.True(set.Remove(new BadHashObject(i)));
            }

            // Проверяем оставшиеся
            Assert.Equal(collisionCount / 2, set.Count);
            for (int i = collisionCount / 2; i < collisionCount; i++)
            {
                Assert.True(set.Contains(new BadHashObject(i)));
            }
        }

        [Fact]
        public void Performance_Test_With_Resizing()
        {
            // Arrange
            var set = new Set.HashSet.HashSet<int>(4); // Начинаем с малой емкости

            // Act - Добавляем много элементов, вызывая несколько ресайзов
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
            }

            // Assert
            Assert.Equal(100, set.Count);
            for (int i = 0; i < 100; i++)
            {
                Assert.True(set.Contains(i));
            }
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

        // Класс с плохой хеш-функцией для тестирования коллизий
        private class BadHashObject
        {
            public int Id { get; }

            public BadHashObject(int id)
            {
                Id = id;
            }

            public override bool Equals(object obj)
            {
                return obj is BadHashObject other && Id == other.Id;
            }

            // Все объекты возвращают один и тот же хеш-код
            public override int GetHashCode()
            {
                return 1; // Намеренно плохая хеш-функция
            }
        }
    }
}
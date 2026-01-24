namespace Disjoint_Set.Tests
{
    public class DisjointSetTests
    {
        [Fact]
        public void Constructor_Initializes_With_Elements()
        {
            // Arrange
            var elements = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            var ds = new DisjointSet<int>(elements);

            // Assert
            // Все элементы должны быть своими собственными родителями
            Assert.Equal(1, ds.Find(1));
            Assert.Equal(2, ds.Find(2));
            Assert.Equal(3, ds.Find(3));
            Assert.Equal(4, ds.Find(4));
            Assert.Equal(5, ds.Find(5));

            // Проверяем что элементы в разных множествах
            Assert.False(ds.SameSet(1, 2));
            Assert.False(ds.SameSet(2, 3));
            Assert.False(ds.SameSet(3, 4));
            Assert.False(ds.SameSet(4, 5));
        }

        [Fact]
        public void Constructor_With_Duplicate_Elements_Works()
        {
            // Arrange
            var elements = new List<int> { 1, 2, 2, 3, 3, 3 };

            // Act
            var ds = new DisjointSet<int>(elements);

            // Assert - Дубликаты игнорируются (Dictionary не позволяет дубликаты ключей)
            Assert.Equal(1, ds.Find(1));
            Assert.Equal(2, ds.Find(2));
            Assert.Equal(3, ds.Find(3));

            // Только 3 уникальных элемента
            Assert.Equal(3, ds.CountSets());
        }

        [Fact]
        public void Find_Non_Existent_Element_Throws_Exception()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3 });

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => ds.Find(4));
            Assert.Contains("no such element", exception.Message);
        }

        [Fact]
        public void Find_Returns_Root_With_Path_Compression()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3, 4, 5 });
            // Создаем цепочку: 1 → 2 → 3 → 4 → 5
            ds.Union(1, 2);
            ds.Union(2, 3);
            ds.Union(3, 4);
            ds.Union(4, 5);

            // Act
            var root1 = ds.Find(1);
            var root2 = ds.Find(2);
            var root3 = ds.Find(3);
            var root4 = ds.Find(4);
            var root5 = ds.Find(5);

            // Assert
            // Все должны возвращать один и тот же корень
            Assert.Equal(root1, root2);
            Assert.Equal(root1, root3);
            Assert.Equal(root1, root4);
            Assert.Equal(root1, root5);

            // После первого Find с path compression, следующие Find должны быть быстрее
            var root1Again = ds.Find(1);
            Assert.Equal(root1, root1Again);
        }

        [Fact]
        public void Union_Joins_Two_Separate_Sets()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3, 4 });

            // Act
            ds.Union(1, 2);
            ds.Union(3, 4);

            // Assert
            Assert.True(ds.SameSet(1, 2));
            Assert.True(ds.SameSet(3, 4));
            Assert.False(ds.SameSet(1, 3));
            Assert.False(ds.SameSet(2, 4));

            // После Union 1 и 2 должны иметь общий корень
            var root1 = ds.Find(1);
            var root2 = ds.Find(2);
            Assert.Equal(root1, root2);

            // Но корни для 1 и 3 должны быть разными
            var root3 = ds.Find(3);
            Assert.NotEqual(root1, root3);
        }

        [Fact]
        public void Union_Same_Element_Does_Nothing()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2 });

            // Act
            ds.Union(1, 1); // Объединение элемента с самим собой

            // Assert
            // Не должно быть ошибки
            Assert.Equal(1, ds.Find(1));
            Assert.Equal(2, ds.Find(2));
            Assert.False(ds.SameSet(1, 2));
        }

        [Fact]
        public void Union_Already_Connected_Elements_Does_Nothing()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3 });
            ds.Union(1, 2);

            // Act - Пытаемся объединить снова
            ds.Union(1, 2);
            ds.Union(2, 1); // В обратном порядке

            // Assert
            Assert.True(ds.SameSet(1, 2));
            Assert.False(ds.SameSet(1, 3));
            Assert.False(ds.SameSet(2, 3));
        }

        [Fact]
        public void Union_By_Rank_Works_Correctly()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3, 4, 5, 6 });

            // Строим два дерева разной высоты
            // Дерево 1: 1 ← 2 ← 3 (ранг 2)
            ds.Union(1, 2);
            ds.Union(1, 3);

            // Дерево 2: 4 ← 5 ← 6 (ранг 2)
            ds.Union(4, 5);
            ds.Union(4, 6);

            // Проверяем ранги корней
            // Теперь объединяем два дерева одинакового ранга
            ds.Union(1, 4);

            // Assert
            // Все элементы должны быть в одном множестве
            Assert.True(ds.SameSet(1, 4));
            Assert.True(ds.SameSet(2, 5));
            Assert.True(ds.SameSet(3, 6));

            // Все должны иметь один корень
            var root1 = ds.Find(1);
            var root2 = ds.Find(2);
            var root3 = ds.Find(3);
            var root4 = ds.Find(4);
            var root5 = ds.Find(5);
            var root6 = ds.Find(6);

            Assert.Equal(root1, root2);
            Assert.Equal(root1, root3);
            Assert.Equal(root1, root4);
            Assert.Equal(root1, root5);
            Assert.Equal(root1, root6);
        }

        [Fact]
        public void Union_Non_Existent_Element_Throws_Exception()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3 });

            // Act & Assert
            // Первый элемент существует, второй - нет
            Assert.Throws<InvalidOperationException>(() => ds.Union(1, 4));

            // Оба элемента не существуют
            Assert.Throws<InvalidOperationException>(() => ds.Union(4, 5));

            // Первый элемент не существует, второй - существует
            Assert.Throws<InvalidOperationException>(() => ds.Union(4, 1));
        }

        [Fact]
        public void SameSet_Returns_True_For_Connected_Elements()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3, 4 });
            ds.Union(1, 2);
            ds.Union(3, 4);
            ds.Union(2, 3); // Объединяем все

            // Act & Assert
            Assert.True(ds.SameSet(1, 2));
            Assert.True(ds.SameSet(1, 3));
            Assert.True(ds.SameSet(1, 4));
            Assert.True(ds.SameSet(2, 3));
            Assert.True(ds.SameSet(2, 4));
            Assert.True(ds.SameSet(3, 4));
        }

        [Fact]
        public void SameSet_Returns_False_For_Disconnected_Elements()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3, 4, 5 });
            ds.Union(1, 2);
            ds.Union(3, 4);
            // 5 остается отдельно

            // Act & Assert
            Assert.True(ds.SameSet(1, 2));
            Assert.True(ds.SameSet(3, 4));
            Assert.False(ds.SameSet(1, 3));
            Assert.False(ds.SameSet(1, 5));
            Assert.False(ds.SameSet(2, 3));
            Assert.False(ds.SameSet(2, 5));
            Assert.False(ds.SameSet(3, 5));
            Assert.False(ds.SameSet(4, 5));
        }

        [Fact]
        public void SameSet_Throws_Exception_For_Non_Existent_Elements()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2 });

            // Act & Assert
            // Оба элемента не существуют
            Assert.Throws<InvalidOperationException>(() => ds.SameSet(3, 4));

            // Первый существует, второй - нет
            Assert.Throws<InvalidOperationException>(() => ds.SameSet(1, 3));

            // Первый не существует, второй - существует
            Assert.Throws<InvalidOperationException>(() => ds.SameSet(3, 1));
        }

        [Fact]
        public void CountSets_Returns_Correct_Number_Of_Sets()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 });

            // Assert - Initially each element is its own set
            Assert.Equal(8, ds.CountSets());

            // Act - Union some elements
            ds.Union(1, 2); // Соединяем 1 и 2
            Assert.Equal(7, ds.CountSets()); // На 1 меньше

            ds.Union(3, 4); // Соединяем 3 и 4
            Assert.Equal(6, ds.CountSets()); // На 1 меньше

            ds.Union(5, 6); // Соединяем 5 и 6
            ds.Union(6, 7); // Соединяем 6 и 7 (5,6,7 вместе)
            Assert.Equal(4, ds.CountSets()); // 1-2, 3-4, 5-6-7, 8

            // Act - Connect separate groups
            ds.Union(2, 4); // Соединяем группу 1-2 с группой 3-4
            Assert.Equal(3, ds.CountSets()); // 1-2-3-4, 5-6-7, 8

            ds.Union(1, 7); // Соединяем все группы кроме 8
            Assert.Equal(2, ds.CountSets()); // 1-2-3-4-5-6-7, 8

            // Act - Connect last element
            ds.Union(8, 1); // Соединяем все
            Assert.Equal(1, ds.CountSets()); // Все в одном множестве
        }

        [Fact]
        public void CountSets_With_Empty_Set_Returns_Zero()
        {
            // Arrange
            var ds = new DisjointSet<int>(new List<int>());

            // Act & Assert
            Assert.Equal(0, ds.CountSets());
        }

        [Fact]
        public void Complex_Scenario_Works_Correctly()
        {
            // Arrange
            var elements = Enumerable.Range(1, 10).ToList();
            var ds = new DisjointSet<int>(elements);

            // Phase 1: Create some initial sets
            ds.Union(1, 2);
            ds.Union(3, 4);
            ds.Union(5, 6);
            ds.Union(7, 8);
            // Sets: {1,2}, {3,4}, {5,6}, {7,8}, {9}, {10}

            // Assert Phase 1
            Assert.Equal(6, ds.CountSets());
            Assert.True(ds.SameSet(1, 2));
            Assert.True(ds.SameSet(3, 4));
            Assert.True(ds.SameSet(5, 6));
            Assert.True(ds.SameSet(7, 8));
            Assert.False(ds.SameSet(1, 3));
            Assert.False(ds.SameSet(5, 9));
            Assert.False(ds.SameSet(7, 10));

            // Phase 2: Merge some sets
            ds.Union(2, 3); // Merge {1,2} with {3,4}
            ds.Union(6, 7); // Merge {5,6} with {7,8}
                            // Sets: {1,2,3,4}, {5,6,7,8}, {9}, {10}

            // Assert Phase 2
            Assert.Equal(4, ds.CountSets());
            Assert.True(ds.SameSet(1, 4)); // 1 и 4 теперь в одном множестве
            Assert.True(ds.SameSet(5, 8)); // 5 и 8 теперь в одном множестве
            Assert.False(ds.SameSet(1, 5)); // Разные множества
            Assert.False(ds.SameSet(4, 9)); // Разные множества

            // Phase 3: Merge all
            ds.Union(4, 8); // Merge {1,2,3,4} with {5,6,7,8}
            ds.Union(9, 10); // Merge {9} with {10}
            ds.Union(1, 9); // Merge everything
                            // Sets: {1,2,3,4,5,6,7,8,9,10}

            // Assert Phase 3
            Assert.Equal(1, ds.CountSets());
            Assert.True(ds.SameSet(1, 10)); // Все в одном множестве
            Assert.True(ds.SameSet(2, 9));
            Assert.True(ds.SameSet(3, 8));
            Assert.True(ds.SameSet(4, 7));
            Assert.True(ds.SameSet(5, 6));

            // Phase 4: Try to union already connected elements
            ds.Union(1, 2);
            ds.Union(3, 4);
            ds.Union(5, 6);
            ds.Union(7, 8);
            ds.Union(9, 10);

            // Assert Phase 4 - Nothing should change
            Assert.Equal(1, ds.CountSets());
            Assert.True(ds.SameSet(1, 10));
        }

        [Fact]
        public void DisjointSet_With_String_Elements_Works_Correctly()
        {
            // Arrange
            var elements = new List<string> { "A", "B", "C", "D", "E", "F" };
            var ds = new DisjointSet<string>(elements);

            // Act
            ds.Union("A", "B");
            ds.Union("C", "D");
            ds.Union("E", "F");
            ds.Union("B", "D"); // Connect A-B with C-D
            ds.Union("D", "E"); // Connect all

            // Assert
            Assert.Equal(1, ds.CountSets());
            Assert.True(ds.SameSet("A", "F"));
            Assert.True(ds.SameSet("B", "E"));
            Assert.True(ds.SameSet("C", "D"));

            // Все должны иметь один корень
            var rootA = ds.Find("A");
            var rootB = ds.Find("B");
            var rootC = ds.Find("C");
            var rootD = ds.Find("D");
            var rootE = ds.Find("E");
            var rootF = ds.Find("F");

            Assert.Equal(rootA, rootB);
            Assert.Equal(rootA, rootC);
            Assert.Equal(rootA, rootD);
            Assert.Equal(rootA, rootE);
            Assert.Equal(rootA, rootF);
        }

        [Fact]
        public void DisjointSet_With_Custom_Objects_Works_Correctly()
        {
            // Arrange
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 35);
            var person4 = new Person("David", 40);
            var person5 = new Person("Eve", 45);

            var elements = new List<Person> { person1, person2, person3, person4, person5 };
            var ds = new DisjointSet<Person>(elements);

            // Act
            ds.Union(person1, person2);
            ds.Union(person3, person4);
            ds.Union(person2, person4); // Connect all except person5
            ds.Union(person1, person5); // Connect all

            // Assert
            Assert.Equal(1, ds.CountSets());
            Assert.True(ds.SameSet(person1, person5));
            Assert.True(ds.SameSet(person2, person4));
            Assert.True(ds.SameSet(person3, person5));

            // Все должны иметь один корень
            var root1 = ds.Find(person1);
            var root2 = ds.Find(person2);
            var root3 = ds.Find(person3);
            var root4 = ds.Find(person4);
            var root5 = ds.Find(person5);

            Assert.Equal(root1, root2);
            Assert.Equal(root1, root3);
            Assert.Equal(root1, root4);
            Assert.Equal(root1, root5);
        }

        [Fact]
        public void Path_Compression_Optimizes_Future_Find_Calls()
        {
            // Arrange
            var n = 100;
            var elements = Enumerable.Range(1, n).ToList();
            var ds = new DisjointSet<int>(elements);

            // Создаем длинную цепочку: 1 ← 2 ← 3 ← ... ← n
            for (int i = 1; i < n; i++)
            {
                ds.Union(i, i + 1);
            }

            // Act - Первый Find для элемента 1 должен пройти всю цепочку
            var root1 = ds.Find(1);

            // Assert - Все элементы должны указывать на корень после path compression
            for (int i = 2; i <= n; i++)
            {
                var root = ds.Find(i);
                Assert.Equal(root1, root);
            }

            // Последующие Find должны быть быстрыми благодаря path compression
            var root1Again = ds.Find(1);
            Assert.Equal(root1, root1Again);

            var rootMiddle = ds.Find(n / 2);
            Assert.Equal(root1, rootMiddle);
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
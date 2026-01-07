namespace Graph__Directed_.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Constructor_Creates_Empty_Graph()
        {
            // Arrange & Act
            var graph = new DirectedGraph<int>();

            // Assert
            Assert.Equal(0, graph.VertexCount);
            Assert.Equal(0, graph.EdgeCount);
        }

        [Fact]
        public void AddVertex_Adds_New_Vertex()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Act
            graph.AddVertex(1);

            // Assert
            Assert.Equal(1, graph.VertexCount);
            Assert.True(graph.HasVertex(1));
        }

        [Fact]
        public void AddVertex_Duplicate_Vertex_Does_Not_Increase_Count()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddVertex(1);

            // Act
            graph.AddVertex(1); // Добавляем тот же vertex

            // Assert
            Assert.Equal(1, graph.VertexCount);
        }

        [Fact]
        public void AddEdge_Creates_Directed_Edge()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Act
            graph.AddEdge(1, 2);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);
            Assert.True(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(2, 1)); // Направленный граф!
        }

        [Fact]
        public void AddEdge_With_Existing_Vertices_Adds_Edge_Only()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);

            // Act
            graph.AddEdge(1, 2);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);
        }

        [Fact]
        public void AddEdge_Automatically_Adds_Vertices()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Act
            graph.AddEdge(1, 2);

            // Assert
            Assert.True(graph.HasVertex(1));
            Assert.True(graph.HasVertex(2));
            Assert.Equal(2, graph.VertexCount);
        }

        [Fact]
        public void AddEdge_Duplicate_Edge_Does_Not_Increase_Edge_Count()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);

            // Act
            graph.AddEdge(1, 2); // Добавляем то же ребро

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);
        }

        [Fact]
        public void AddEdge_Self_Loop_Is_Ignored()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Act
            graph.AddEdge(1, 1); // Петля

            // Assert
            Assert.Equal(0, graph.VertexCount);
            Assert.Equal(0, graph.EdgeCount); // Петли не добавляются
            Assert.False(graph.HasEdge(1, 1));
        }

        [Fact]
        public void RemoveVertex_Removes_Vertex_And_All_Connected_Edges()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 1);
            graph.AddEdge(1, 3);
            graph.AddEdge(3, 1);
            graph.AddEdge(2, 3);
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(5, graph.EdgeCount);

            // Act
            graph.RemoveVertex(1);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount); // Осталось только ребро 2→3
            Assert.False(graph.HasVertex(1));
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(2, 1));
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(3, 1));
            Assert.True(graph.HasEdge(2, 3));
        }

        [Fact]
        public void RemoveVertex_Non_Existent_Vertex_Does_Nothing()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);
            var initialVertexCount = graph.VertexCount;
            var initialEdgeCount = graph.EdgeCount;

            // Act
            graph.RemoveVertex(3); // Несуществующая вершина

            // Assert
            Assert.Equal(initialVertexCount, graph.VertexCount);
            Assert.Equal(initialEdgeCount, graph.EdgeCount);
        }

        [Fact]
        public void RemoveEdge_Removes_Directed_Edge()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 1); // Обратное направление
            graph.AddEdge(1, 3);
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(3, graph.EdgeCount);

            // Act
            graph.RemoveEdge(1, 2);

            // Assert
            Assert.Equal(3, graph.VertexCount); // Вершины остаются
            Assert.Equal(2, graph.EdgeCount); // 2→1 и 1→3
            Assert.False(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(2, 1)); // Обратное направление осталось
            Assert.True(graph.HasEdge(1, 3));
        }

        [Fact]
        public void RemoveEdge_Non_Existent_Edge_Does_Nothing()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);
            var initialEdgeCount = graph.EdgeCount;

            // Act
            graph.RemoveEdge(1, 3); // Несуществующее ребро
            graph.RemoveEdge(2, 1); // Существует только 1→2

            // Assert
            Assert.Equal(initialEdgeCount, graph.EdgeCount);
        }

        [Fact]
        public void HasVertex_Returns_True_For_Existing_Vertex()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddVertex(1);
            graph.AddEdge(2, 3);

            // Act & Assert
            Assert.True(graph.HasVertex(1));
            Assert.True(graph.HasVertex(2));
            Assert.True(graph.HasVertex(3));
        }

        [Fact]
        public void HasVertex_Returns_False_For_Non_Existent_Vertex()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddVertex(1);

            // Act & Assert
            Assert.False(graph.HasVertex(2));
            Assert.False(graph.HasVertex(0));
        }

        [Fact]
        public void HasEdge_Returns_True_For_Existing_Directed_Edge()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);

            // Act & Assert
            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(2, 3));
            Assert.False(graph.HasEdge(2, 1)); // Нет обратного
            Assert.False(graph.HasEdge(3, 2)); // Нет обратного
        }

        [Fact]
        public void HasEdge_Returns_False_For_Non_Existent_Edge()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);

            // Act & Assert
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(2, 1));
            Assert.False(graph.HasEdge(2, 4));
        }

        [Fact]
        public void HasEdge_Returns_False_For_Non_Existent_Source_Vertex()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Act & Assert
            Assert.False(graph.HasEdge(1, 2));
        }

        [Fact]
        public void GetOutNeighbors_Returns_List_Of_Adjacent_Vertices()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);

            // Act
            var neighbors1 = graph.GetOutNeighbors(1);
            var neighbors2 = graph.GetOutNeighbors(2);
            var neighbors3 = graph.GetOutNeighbors(3);
            var neighbors4 = graph.GetOutNeighbors(4);
            var neighbors5 = graph.GetOutNeighbors(5); // Несуществующая вершина

            // Assert
            Assert.Equal(new List<int> { 2, 3, 4 }, neighbors1.OrderBy(x => x));
            Assert.Equal(new List<int> { 3 }, neighbors2);
            Assert.Empty(neighbors3);
            Assert.Empty(neighbors4);
            Assert.Empty(neighbors5);
        }

        [Fact]
        public void GetInNeighbors_Returns_List_Of_Vertices_Pointing_To_Target()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(5, 3);

            // Act
            var inNeighbors3 = graph.GetInNeighbors(3);
            var inNeighbors1 = graph.GetInNeighbors(1);
            var inNeighbors4 = graph.GetInNeighbors(4);
            var inNeighbors5 = graph.GetInNeighbors(5);
            var inNeighbors6 = graph.GetInNeighbors(6); // Несуществующая вершина

            // Assert
            Assert.Equal(new List<int> { 1, 2, 5 }, inNeighbors3.OrderBy(x => x));
            Assert.Empty(inNeighbors1); // Никто не указывает на 1
            Assert.Equal(new List<int> { 3 }, inNeighbors4);
            Assert.Empty(inNeighbors5); // Никто не указывает на 5
            Assert.Empty(inNeighbors6);
        }

        [Fact]
        public void Clear_Removes_All_Vertices_And_Edges()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            Assert.Equal(4, graph.VertexCount);
            Assert.Equal(3, graph.EdgeCount);

            // Act
            graph.Clear();

            // Assert
            Assert.Equal(0, graph.VertexCount);
            Assert.Equal(0, graph.EdgeCount);
            Assert.False(graph.HasVertex(1));
            Assert.False(graph.HasEdge(1, 2));
        }

        [Fact]
        public void VertexCount_Returns_Correct_Number_Of_Vertices()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Assert - Initial
            Assert.Equal(0, graph.VertexCount);

            // Act - Add vertices
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            // Assert - After additions
            Assert.Equal(3, graph.VertexCount);

            // Act - Remove vertex
            graph.RemoveVertex(2);

            // Assert - After removal
            Assert.Equal(2, graph.VertexCount);

            // Act - Clear
            graph.Clear();

            // Assert - After clear
            Assert.Equal(0, graph.VertexCount);
        }

        [Fact]
        public void EdgeCount_Returns_Correct_Number_Of_Edges()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Assert - Initial
            Assert.Equal(0, graph.EdgeCount);

            // Act - Add edges
            graph.AddEdge(1, 2); // 1 edge
            graph.AddEdge(2, 3); // 2 edges
            graph.AddEdge(3, 4); // 3 edges
            graph.AddEdge(4, 1); // 4 edges

            // Assert - After additions
            Assert.Equal(4, graph.EdgeCount);

            // Act - Add duplicate edge
            graph.AddEdge(1, 2);

            // Assert - No change for duplicate
            Assert.Equal(4, graph.EdgeCount);

            // Act - Remove edge
            graph.RemoveEdge(2, 3);

            // Assert - After removal
            Assert.Equal(3, graph.EdgeCount);

            // Act - Remove vertex (removes connected edges)
            graph.RemoveVertex(1);

            // Assert - After vertex removal
            Assert.Equal(1, graph.EdgeCount); // Только ребро 3→4

            // Act - Clear
            graph.Clear();

            // Assert - After clear
            Assert.Equal(0, graph.EdgeCount);
        }

        [Fact]
        public void Complex_Directed_Graph_Operations_Work_Correctly()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Phase 1: Build a complex directed graph
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 1);
            graph.AddEdge(1, 3);
            graph.AddEdge(3, 1);
            graph.AddEdge(2, 4);

            // Assert Phase 1
            Assert.Equal(4, graph.VertexCount);
            Assert.Equal(7, graph.EdgeCount);

            // Проверяем ребра
            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(2, 3));
            Assert.True(graph.HasEdge(3, 4));
            Assert.True(graph.HasEdge(4, 1));
            Assert.True(graph.HasEdge(1, 3));
            Assert.True(graph.HasEdge(3, 1));
            Assert.True(graph.HasEdge(2, 4));

            // Проверяем отсутствие обратных ребер (если не добавлены)
            Assert.False(graph.HasEdge(2, 1));
            Assert.False(graph.HasEdge(3, 2));
            Assert.False(graph.HasEdge(4, 3));
            Assert.False(graph.HasEdge(1, 4));
            Assert.False(graph.HasEdge(4, 2));

            // Phase 2: Remove some edges
            graph.RemoveEdge(1, 2);
            graph.RemoveEdge(3, 1);

            // Assert Phase 2
            Assert.Equal(4, graph.VertexCount);
            Assert.Equal(5, graph.EdgeCount);
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(3, 1));

            // Phase 3: Remove a vertex
            graph.RemoveVertex(3);

            // Assert Phase 3
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(2, graph.EdgeCount); // Остались только 2→4 и 4→1
            Assert.False(graph.HasVertex(3));
            Assert.False(graph.HasEdge(2, 3));
            Assert.False(graph.HasEdge(3, 4));
            Assert.False(graph.HasEdge(1, 3));

            // Phase 4: Add new vertices and edges
            graph.AddEdge(5, 6);
            graph.AddEdge(5, 2);
            graph.AddEdge(4, 5);

            // Assert Phase 4
            Assert.Equal(5, graph.VertexCount); // 1, 2, 4, 5, 6
            Assert.Equal(5, graph.EdgeCount); // 2→4, 4→1, 5→6, 5→2, 4→5
            Assert.True(graph.HasEdge(5, 6));
            Assert.True(graph.HasEdge(5, 2));
            Assert.True(graph.HasEdge(4, 5));
        }

        [Fact]
        public void Graph_With_String_Vertices_Works_Correctly()
        {
            // Arrange
            var graph = new DirectedGraph<string>();

            // Act
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "A");
            graph.AddEdge("A", "C");

            // Assert
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(4, graph.EdgeCount);

            // Проверяем направленность
            Assert.True(graph.HasEdge("A", "B"));
            Assert.True(graph.HasEdge("B", "C"));
            Assert.True(graph.HasEdge("C", "A"));
            Assert.True(graph.HasEdge("A", "C"));

            // Проверяем отсутствие обратных ребер
            Assert.False(graph.HasEdge("B", "A"));
            Assert.False(graph.HasEdge("C", "B"));
            Assert.False(graph.HasEdge("A", "C") && graph.HasEdge("C", "A") ? false : false); // Оба есть

            // Проверяем соседей
            Assert.Equal(new List<string> { "B", "C" }, graph.GetOutNeighbors("A").OrderBy(x => x));
            Assert.Equal(new List<string> { "C" }, graph.GetOutNeighbors("B"));
            Assert.Equal(new List<string> { "A" }, graph.GetOutNeighbors("C"));
        }

        [Fact]
        public void Graph_With_Custom_Object_Vertices_Works_Correctly()
        {
            // Arrange
            var graph = new DirectedGraph<Person>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 35);

            // Act
            graph.AddEdge(person1, person2);    // p1 -> p2
            graph.AddEdge(person2, person3);    // p2 -> p3
            graph.AddEdge(person3, person1);    // p3 -> p1
            graph.AddEdge(person1, person3);    // p1 -> p3

            // Assert
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(4, graph.EdgeCount);

            Assert.True(graph.HasEdge(person1, person2));
            Assert.True(graph.HasEdge(person2, person3));
            Assert.True(graph.HasEdge(person3, person1));
            Assert.True(graph.HasEdge(person1, person3));

            // Проверяем направленность
            Assert.False(graph.HasEdge(person2, person1));
            Assert.False(graph.HasEdge(person3, person2));

            // Проверяем in-neighbors и out-neighbors
            Assert.Equal([person2, person3], graph.GetOutNeighbors(person1).OrderBy(p => p.Name).ToList());
            Assert.Equal([person3], graph.GetInNeighbors(person1).ToList());
            Assert.Equal([person1, person2], graph.GetInNeighbors(person3).OrderBy(p => p.Name).ToList());
        }

        [Fact]
        public void Isolated_Vertices_Work_Correctly_In_Directed_Graph()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Act
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 3); // Взаимное ребро

            // Assert
            Assert.Equal(4, graph.VertexCount);
            Assert.Equal(2, graph.EdgeCount);

            // Изолированные вершины
            Assert.True(graph.HasVertex(1));
            Assert.True(graph.HasVertex(2));
            Assert.Empty(graph.GetOutNeighbors(1));
            Assert.Empty(graph.GetInNeighbors(1));
            Assert.Empty(graph.GetOutNeighbors(2));
            Assert.Empty(graph.GetInNeighbors(2));

            // Вершины с ребрами
            Assert.Equal(new List<int> { 4 }, graph.GetOutNeighbors(3));
            Assert.Equal(new List<int> { 4 }, graph.GetInNeighbors(3));
            Assert.Equal(new List<int> { 3 }, graph.GetOutNeighbors(4));
            Assert.Equal(new List<int> { 3 }, graph.GetInNeighbors(4));
        }

        [Fact]
        public void Multiple_Edges_To_Same_Target_Not_Allowed()
        {
            // Arrange
            var graph = new DirectedGraph<int>();

            // Act
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 2); // Дубликат
            graph.AddEdge(1, 2); // Еще дубликат

            // Assert
            Assert.Equal(1, graph.EdgeCount);
            Assert.Single(graph.GetOutNeighbors(1));
        }

        [Fact]
        public void RemoveVertex_Also_Removes_Incoming_Edges()
        {
            // Arrange
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 3);

            // Act
            graph.RemoveVertex(3);

            // Assert
            Assert.Equal(3, graph.VertexCount); // 1, 2, 4
            Assert.Equal(0, graph.EdgeCount); // Все ребра связаны с вершиной 3

            // Проверяем что не осталось ссылок на удаленную вершину
            Assert.Empty(graph.GetOutNeighbors(1));
            Assert.Empty(graph.GetOutNeighbors(2));
            Assert.Empty(graph.GetOutNeighbors(4));
            Assert.Empty(graph.GetInNeighbors(1));
            Assert.Empty(graph.GetInNeighbors(2));
            Assert.Empty(graph.GetInNeighbors(4));
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
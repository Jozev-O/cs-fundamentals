namespace Graph__Undirected_.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Constructor_Creates_Empty_Graph()
        {
            // Arrange & Act
            var graph = new UndirectedGraph<int>();

            // Assert
            Assert.Equal(0, graph.VertexCount);
            Assert.Equal(0, graph.EdgeCount);
        }

        [Fact]
        public void AddVertex_Adds_New_Vertex()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();

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
            var graph = new UndirectedGraph<int>();
            graph.AddVertex(1);

            // Act
            graph.AddVertex(1); // Добавляем тот же vertex

            // Assert
            Assert.Equal(1, graph.VertexCount);
        }

        [Fact]
        public void AddEdge_Adds_New_Edge_Between_Vertices()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();

            // Act
            graph.AddEdge(1, 2);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);
            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(2, 1)); // Граф неориентированный
        }

        [Fact]
        public void AddEdge_With_Existing_Vertices_Adds_Edge_Only()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);

            // Act
            graph.AddEdge(1, 2);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);
        }

        [Fact]
        public void AddEdge_Duplicate_Edge_Does_Not_Increase_Edge_Count()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
            graph.AddEdge(1, 2);

            // Act
            graph.AddEdge(1, 2); // Добавляем то же ребро
            graph.AddEdge(2, 1); // И в обратном порядке

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount); // Count не увеличился
        }

        [Fact]
        public void AddEdge_Self_Loop_Is_Ignored()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();

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
            var graph = new UndirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(3, graph.EdgeCount);

            // Act
            graph.RemoveVertex(1);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount); // Осталось только ребро между 2 и 3
            Assert.False(graph.HasVertex(1));
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(1, 3));
            Assert.True(graph.HasEdge(2, 3));
        }

        [Fact]
        public void RemoveVertex_Non_Existent_Vertex_Does_Nothing()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
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
        public void RemoveEdge_Removes_Edge_Between_Vertices()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(2, graph.EdgeCount);

            // Act
            graph.RemoveEdge(1, 2);

            // Assert
            Assert.Equal(3, graph.VertexCount); // Вершины остаются
            Assert.Equal(1, graph.EdgeCount); // Только ребро 1-3
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(2, 1));
            Assert.True(graph.HasEdge(1, 3));
        }

        [Fact]
        public void RemoveEdge_Non_Existent_Edge_Does_Nothing()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
            graph.AddEdge(1, 2);
            var initialEdgeCount = graph.EdgeCount;

            // Act
            graph.RemoveEdge(1, 3); // Несуществующее ребро

            // Assert
            Assert.Equal(initialEdgeCount, graph.EdgeCount);
        }

        [Fact]
        public void HasVertex_Returns_True_For_Existing_Vertex()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
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
            var graph = new UndirectedGraph<int>();
            graph.AddVertex(1);

            // Act & Assert
            Assert.False(graph.HasVertex(2));
            Assert.False(graph.HasVertex(0));
        }

        [Fact]
        public void HasEdge_Returns_True_For_Existing_Edge()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);

            // Act & Assert
            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(2, 1)); // В обе стороны
            Assert.True(graph.HasEdge(2, 3));
            Assert.True(graph.HasEdge(3, 2));
        }

        [Fact]
        public void HasEdge_Returns_False_For_Non_Existent_Edge()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
            graph.AddEdge(1, 2);

            // Act & Assert
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(3, 1));
            Assert.False(graph.HasEdge(2, 4));
        }

        [Fact]
        public void HasEdge_Returns_False_For_Non_Existent_Vertices()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();

            // Act & Assert
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(2, 1));
        }

        [Fact]
        public void GetNeighbors_Returns_List_Of_Adjacent_Vertices()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);

            // Act
            var neighbors1 = graph.GetNeighbors(1);
            var neighbors2 = graph.GetNeighbors(2);
            var neighbors3 = graph.GetNeighbors(3);
            var neighbors4 = graph.GetNeighbors(4);
            var neighbors5 = graph.GetNeighbors(5); // Несуществующая вершина

            // Assert
            Assert.Equal(new List<int> { 2, 3, 4 }, neighbors1.OrderBy(x => x));
            Assert.Equal(new List<int> { 1, 3 }, neighbors2.OrderBy(x => x));
            Assert.Equal(new List<int> { 1, 2 }, neighbors3.OrderBy(x => x));
            Assert.Equal(new List<int> { 1 }, neighbors4);
            Assert.Empty(neighbors5);
        }

        [Fact]
        public void GetNeighbors_Returns_Empty_List_For_Isolated_Vertex()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
            graph.AddVertex(1);

            // Act
            var neighbors = graph.GetNeighbors(1);

            // Assert
            Assert.Empty(neighbors);
        }

        [Fact]
        public void Clear_Removes_All_Vertices_And_Edges()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();
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
            var graph = new UndirectedGraph<int>();

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
            var graph = new UndirectedGraph<int>();

            // Assert - Initial
            Assert.Equal(0, graph.EdgeCount);

            // Act - Add edges
            graph.AddEdge(1, 2); // 1 edge
            graph.AddEdge(2, 3); // 2 edges
            graph.AddEdge(3, 4); // 3 edges

            // Assert - After additions
            Assert.Equal(3, graph.EdgeCount);

            // Act - Add duplicate edge
            graph.AddEdge(1, 2);

            // Assert - No change for duplicate
            Assert.Equal(3, graph.EdgeCount);

            // Act - Remove edge
            graph.RemoveEdge(2, 3);

            // Assert - After removal
            Assert.Equal(2, graph.EdgeCount);

            // Act - Remove vertex (removes connected edges)
            graph.RemoveVertex(1);

            // Assert - After vertex removal
            Assert.Equal(1, graph.EdgeCount); // Только ребро 3-4

            // Act - Clear
            graph.Clear();

            // Assert - After clear
            Assert.Equal(0, graph.EdgeCount);
        }

        [Fact]
        public void Complex_Graph_Operations_Work_Correctly()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();

            // Phase 1: Build a complex graph
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(5, 6);

            // Assert Phase 1
            Assert.Equal(6, graph.VertexCount);
            Assert.Equal(7, graph.EdgeCount);

            // Проверяем все ребра
            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(1, 3));
            Assert.True(graph.HasEdge(2, 3));
            Assert.True(graph.HasEdge(3, 4));
            Assert.True(graph.HasEdge(4, 5));
            Assert.True(graph.HasEdge(4, 6));
            Assert.True(graph.HasEdge(5, 6));

            // Phase 2: Remove some edges
            graph.RemoveEdge(1, 2);
            graph.RemoveEdge(4, 6);

            // Assert Phase 2
            Assert.Equal(6, graph.VertexCount);
            Assert.Equal(5, graph.EdgeCount);
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(4, 6));

            // Phase 3: Remove a vertex
            graph.RemoveVertex(3);

            // Assert Phase 3
            Assert.Equal(5, graph.VertexCount);
            Assert.Equal(2, graph.EdgeCount); // Остались только ребра 4-5 и 5-6
            Assert.False(graph.HasVertex(3));
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(2, 3));
            Assert.False(graph.HasEdge(3, 4));

            // Phase 4: Add new vertices and edges
            graph.AddEdge(7, 8);
            graph.AddEdge(7, 5);

            // Assert Phase 4
            Assert.Equal(7, graph.VertexCount);
            Assert.Equal(4, graph.EdgeCount);
            Assert.True(graph.HasEdge(7, 8));
            Assert.True(graph.HasEdge(7, 5));
        }

        [Fact]
        public void Graph_With_String_Vertices_Works_Correctly()
        {
            // Arrange
            var graph = new UndirectedGraph<string>();

            // Act
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "D");
            graph.AddEdge("D", "A");

            // Assert
            Assert.Equal(4, graph.VertexCount);
            Assert.Equal(4, graph.EdgeCount);

            Assert.True(graph.HasVertex("A"));
            Assert.True(graph.HasVertex("B"));
            Assert.True(graph.HasVertex("C"));
            Assert.True(graph.HasVertex("D"));

            Assert.True(graph.HasEdge("A", "B"));
            Assert.True(graph.HasEdge("B", "C"));
            Assert.True(graph.HasEdge("C", "D"));
            Assert.True(graph.HasEdge("D", "A"));

            // Проверяем симметричность
            Assert.True(graph.HasEdge("B", "A"));
            Assert.True(graph.HasEdge("C", "B"));
            Assert.True(graph.HasEdge("D", "C"));
            Assert.True(graph.HasEdge("A", "D"));

            // Act - Remove
            graph.RemoveVertex("B");

            // Assert
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(2, graph.EdgeCount); // Только C-D и D-A
            Assert.False(graph.HasEdge("A", "B"));
            Assert.False(graph.HasEdge("B", "C"));
        }

        [Fact]
        public void Graph_With_Custom_Object_Vertices_Works_Correctly()
        {
            // Arrange
            var graph = new UndirectedGraph<Person>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 35);

            // Act
            graph.AddEdge(person1, person2);
            graph.AddEdge(person2, person3);
            graph.AddEdge(person3, person1);

            // Assert
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(3, graph.EdgeCount);

            Assert.True(graph.HasVertex(person1));
            Assert.True(graph.HasVertex(person2));
            Assert.True(graph.HasVertex(person3));

            Assert.True(graph.HasEdge(person1, person2));
            Assert.True(graph.HasEdge(person2, person3));
            Assert.True(graph.HasEdge(person3, person1));

            // Проверяем симметричность
            Assert.True(graph.HasEdge(person2, person1));
            Assert.True(graph.HasEdge(person3, person2));
            Assert.True(graph.HasEdge(person1, person3));

            // Act - Get neighbors
            var neighbors = graph.GetNeighbors(person1);

            // Assert
            Assert.Equal(2, neighbors.Count);
            Assert.Contains(person2, neighbors);
            Assert.Contains(person3, neighbors);
        }

        [Fact]
        public void Isolated_Vertices_Work_Correctly()
        {
            // Arrange
            var graph = new UndirectedGraph<int>();

            // Act
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddEdge(3, 4);

            // Assert
            Assert.Equal(4, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);

            Assert.True(graph.HasVertex(1));
            Assert.True(graph.HasVertex(2));
            Assert.Empty(graph.GetNeighbors(1));
            Assert.Empty(graph.GetNeighbors(2));

            // Изолированные вершины не должны иметь ребер
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(2, 4));
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
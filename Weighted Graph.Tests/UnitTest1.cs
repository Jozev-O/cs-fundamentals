namespace Weighted_Graph.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Constructor_Creates_Empty_Graph()
        {
            // Arrange & Act
            var graph = new WeightedGraph<int>();

            // Assert
            Assert.Equal(0, graph.VertexCount);
            Assert.Equal(0, graph.EdgeCount);
        }

        [Fact]
        public void AddVertex_Adds_New_Vertex()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act
            graph.AddVertex(1);

            // Assert
            Assert.Equal(1, graph.VertexCount);
            Assert.True(graph.HasVertex(1));
            Assert.Empty(graph.GetNeighbors(1));
        }

        [Fact]
        public void AddVertex_Duplicate_Vertex_Does_Not_Increase_Count()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddVertex(1);

            // Act
            graph.AddVertex(1); // Добавляем тот же vertex

            // Assert
            Assert.Equal(1, graph.VertexCount);
        }

        [Fact]
        public void AddEdge_Creates_Weighted_Undirected_Edge()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act
            graph.AddEdge(1, 2, 5.5);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);
            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(2, 1)); // Неориентированный граф
            Assert.Equal(5.5, graph.GetEdgeWeight(1, 2));
            Assert.Equal(5.5, graph.GetEdgeWeight(2, 1));
        }

        [Fact]
        public void AddEdge_With_Existing_Vertices_Adds_Edge_Only()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);

            // Act
            graph.AddEdge(1, 2, 3.0);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);
            Assert.Equal(3.0, graph.GetEdgeWeight(1, 2));
        }

        [Fact]
        public void AddEdge_Automatically_Adds_Vertices()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act
            graph.AddEdge(1, 2, 2.5);

            // Assert
            Assert.True(graph.HasVertex(1));
            Assert.True(graph.HasVertex(2));
            Assert.Equal(2, graph.VertexCount);
        }

        [Fact]
        public void AddEdge_Self_Loop_Is_Ignored()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act
            graph.AddEdge(1, 1, 5.0); // Петля

            // Assert
            Assert.Equal(0, graph.VertexCount);
            Assert.Equal(0, graph.EdgeCount); // Петли не добавляются
            Assert.False(graph.HasEdge(1, 1));
        }

        [Fact]
        public void RemoveVertex_Removes_Vertex_And_All_Connected_Edges()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);
            graph.AddEdge(1, 3, 2.0);
            graph.AddEdge(2, 3, 3.0);
            graph.AddEdge(3, 4, 4.0);
            Assert.Equal(4, graph.VertexCount);
            Assert.Equal(4, graph.EdgeCount);

            // Act
            graph.RemoveVertex(1);

            // Assert
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(2, graph.EdgeCount); // Остались только ребра 2-3 и 3-4
            Assert.False(graph.HasVertex(1));
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(1, 3));
            Assert.True(graph.HasEdge(2, 3));
            Assert.True(graph.HasEdge(3, 4));
        }

        [Fact]
        public void RemoveVertex_Non_Existent_Vertex_Does_Nothing()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);
            var initialVertexCount = graph.VertexCount;
            var initialEdgeCount = graph.EdgeCount;

            // Act
            graph.RemoveVertex(3); // Несуществующая вершина

            // Assert
            Assert.Equal(initialVertexCount, graph.VertexCount);
            Assert.Equal(initialEdgeCount, graph.EdgeCount);
        }

        [Fact]
        public void RemoveEdge_Removes_Undirected_Edge()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);
            graph.AddEdge(1, 3, 2.0);
            graph.AddEdge(2, 3, 3.0);
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(3, graph.EdgeCount);

            // Act
            graph.RemoveEdge(1, 2);

            // Assert
            Assert.Equal(3, graph.VertexCount); // Вершины остаются
            Assert.Equal(2, graph.EdgeCount); // Только ребра 1-3 и 2-3
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(2, 1));
            Assert.True(graph.HasEdge(1, 3));
            Assert.True(graph.HasEdge(2, 3));
        }

        [Fact]
        public void RemoveEdge_Non_Existent_Edge_Does_Nothing()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);
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
            var graph = new WeightedGraph<int>();
            graph.AddVertex(1);
            graph.AddEdge(2, 3, 1.0);

            // Act & Assert
            Assert.True(graph.HasVertex(1));
            Assert.True(graph.HasVertex(2));
            Assert.True(graph.HasVertex(3));
        }

        [Fact]
        public void HasVertex_Returns_False_For_Non_Existent_Vertex()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddVertex(1);

            // Act & Assert
            Assert.False(graph.HasVertex(2));
            Assert.False(graph.HasVertex(0));
        }

        [Fact]
        public void HasEdge_Returns_True_For_Existing_Edge()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);
            graph.AddEdge(2, 3, 2.0);

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
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);

            // Act & Assert
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(3, 1));
            Assert.False(graph.HasEdge(2, 4));
        }

        [Fact]
        public void HasEdge_Returns_False_For_Non_Existent_Vertices()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act & Assert
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(2, 1));
        }

        [Fact]
        public void GetEdgeWeight_Returns_Correct_Weight()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 3.14);
            graph.AddEdge(2, 3, 2.71);

            // Act & Assert
            Assert.Equal(3.14, graph.GetEdgeWeight(1, 2), 3);
            Assert.Equal(3.14, graph.GetEdgeWeight(2, 1), 3);
            Assert.Equal(2.71, graph.GetEdgeWeight(2, 3), 3);
            Assert.Equal(2.71, graph.GetEdgeWeight(3, 2), 3);
        }

        [Fact]
        public void GetEdgeWeight_Throws_Exception_For_Non_Existent_Edge()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => graph.GetEdgeWeight(1, 3));
            Assert.Throws<NullReferenceException>(() => graph.GetEdgeWeight(3, 1));
        }

        [Fact]
        public void GetEdgeWeight_Throws_Exception_For_Non_Existent_Vertex()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => graph.GetEdgeWeight(1, 2));
        }

        [Fact]
        public void GetNeighbors_Returns_List_Of_Adjacent_Vertices_With_Weights()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);
            graph.AddEdge(1, 3, 2.0);
            graph.AddEdge(1, 4, 3.0);
            graph.AddEdge(2, 3, 4.0);

            // Act
            var neighbors1 = graph.GetNeighbors(1);
            var neighbors2 = graph.GetNeighbors(2);
            var neighbors3 = graph.GetNeighbors(3);
            var neighbors4 = graph.GetNeighbors(4);
            var neighbors5 = graph.GetNeighbors(5); // Несуществующая вершина

            // Assert
            Assert.Equal(3, neighbors1.Count);
            Assert.Equal(2, neighbors2.Count);
            Assert.Equal(2, neighbors3.Count);
            Assert.Equal(1, neighbors4.Count);
            Assert.Empty(neighbors5);

            // Проверяем веса
            var edge12 = neighbors1.Find(pair => pair.Key.Equals(2));
            Assert.NotNull(edge12);
            Assert.Equal(1.0, edge12.Value, 3);

            var edge13 = neighbors1.Find(pair => pair.Key.Equals(3));
            Assert.NotNull(edge13);
            Assert.Equal(2.0, edge13.Value, 3);

            var edge14 = neighbors1.Find(pair => pair.Key.Equals(4));
            Assert.NotNull(edge14);
            Assert.Equal(3.0, edge14.Value, 3);
        }

        [Fact]
        public void Clear_Removes_All_Vertices_And_Edges()
        {
            // Arrange
            var graph = new WeightedGraph<int>();
            graph.AddEdge(1, 2, 1.0);
            graph.AddEdge(2, 3, 2.0);
            graph.AddEdge(3, 4, 3.0);
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
            var graph = new WeightedGraph<int>();

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
            var graph = new WeightedGraph<int>();

            // Assert - Initial
            Assert.Equal(0, graph.EdgeCount);

            // Act - Add edges
            graph.AddEdge(1, 2, 1.0); // 1 edge
            graph.AddEdge(2, 3, 2.0); // 2 edges
            graph.AddEdge(3, 4, 3.0); // 3 edges

            // Assert - After additions
            Assert.Equal(3, graph.EdgeCount);

            // Act - Add duplicate edge with different weight
            graph.AddEdge(1, 2, 5.0);

            // Assert - Edge count increases because we allow parallel edges
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
        public void Graph_With_String_Vertices_Works_Correctly()
        {
            // Arrange
            var graph = new WeightedGraph<string>();

            // Act
            graph.AddEdge("A", "B", 1.2);
            graph.AddEdge("B", "C", 2.3);
            graph.AddEdge("C", "D", 3.4);
            graph.AddEdge("D", "A", 4.5);

            // Assert
            Assert.Equal(4, graph.VertexCount);
            Assert.Equal(4, graph.EdgeCount);

            Assert.Equal(1.2, graph.GetEdgeWeight("A", "B"), 3);
            Assert.Equal(1.2, graph.GetEdgeWeight("B", "A"), 3);
            Assert.Equal(2.3, graph.GetEdgeWeight("B", "C"), 3);
            Assert.Equal(2.3, graph.GetEdgeWeight("C", "B"), 3);

            // Проверяем симметричность
            Assert.True(graph.HasEdge("A", "B"));
            Assert.True(graph.HasEdge("B", "A"));
            Assert.True(graph.HasEdge("B", "C"));
            Assert.True(graph.HasEdge("C", "B"));

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
            var graph = new WeightedGraph<Person>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 35);

            // Act
            graph.AddEdge(person1, person2, 1.5);
            graph.AddEdge(person2, person3, 2.5);
            graph.AddEdge(person3, person1, 3.5);

            // Assert
            Assert.Equal(3, graph.VertexCount);
            Assert.Equal(3, graph.EdgeCount);

            Assert.Equal(1.5, graph.GetEdgeWeight(person1, person2), 3);
            Assert.Equal(2.5, graph.GetEdgeWeight(person2, person3), 3);
            Assert.Equal(3.5, graph.GetEdgeWeight(person3, person1), 3);

            // Проверяем симметричность
            Assert.Equal(1.5, graph.GetEdgeWeight(person2, person1), 3);
            Assert.Equal(2.5, graph.GetEdgeWeight(person3, person2), 3);
            Assert.Equal(3.5, graph.GetEdgeWeight(person1, person3), 3);

            // Проверяем соседей
            var neighbors = graph.GetNeighbors(person1);
            Assert.Equal(2, neighbors.Count);
            Assert.Contains(neighbors, pair => pair.Key.Equals(person2) && Math.Abs(pair.Value - 1.5) < 0.001);
            Assert.Contains(neighbors, pair => pair.Key.Equals(person3) && Math.Abs(pair.Value - 3.5) < 0.001);
        }

        [Fact]
        public void Isolated_Vertices_Work_Correctly()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddEdge(3, 4, 1.0);

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

        [Fact]
        public void Negative_Weights_Are_Allowed()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act
            graph.AddEdge(1, 2, -5.0);
            graph.AddEdge(2, 3, 0.0);
            graph.AddEdge(3, 4, -0.5);

            // Assert
            Assert.Equal(-5.0, graph.GetEdgeWeight(1, 2), 3);
            Assert.Equal(0.0, graph.GetEdgeWeight(2, 3), 3);
            Assert.Equal(-0.5, graph.GetEdgeWeight(3, 4), 3);

            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(2, 3));
            Assert.True(graph.HasEdge(3, 4));
        }

        [Fact]
        public void Large_Weights_Are_Handled_Correctly()
        {
            // Arrange
            var graph = new WeightedGraph<int>();

            // Act
            graph.AddEdge(1, 2, double.MaxValue);
            graph.AddEdge(2, 3, double.MinValue);
            graph.AddEdge(3, 4, double.Epsilon);

            // Assert
            Assert.Equal(double.MaxValue, graph.GetEdgeWeight(1, 2));
            Assert.Equal(double.MinValue, graph.GetEdgeWeight(2, 3));
            Assert.Equal(double.Epsilon, graph.GetEdgeWeight(3, 4));
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
namespace AdjacencyMatrixGraph.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Constructor_Creates_Empty_Graph()
        {
            // Arrange & Act
            var graph = new MatrixGraph<int>();

            // Assert
            Assert.Equal(0, graph.VertexCount);
        }

        [Fact]
        public void AddVertex_Adds_New_Vertex()
        {
            // Arrange
            var graph = new MatrixGraph<int>();

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
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);

            // Act
            graph.AddVertex(1); // Добавляем тот же vertex

            // Assert
            Assert.Equal(1, graph.VertexCount);
        }

        [Fact]
        public void AddEdge_Creates_Undirected_Edge()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);

            // Act
            graph.AddEdge(1, 2);

            // Assert
            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(2, 1)); // Неориентированный граф
        }

        [Fact]
        public void AddEdge_Self_Loop_Is_Allowed()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);

            // Act
            graph.AddEdge(1, 1);

            // Assert
            // В матрице смежности петля создаст 1 на диагонали
            Assert.True(graph.HasEdge(1, 1));
        }

        [Fact]
        public void RemoveVertex_Removes_Vertex_And_All_Connected_Edges()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            Assert.Equal(3, graph.VertexCount);

            // Act
            graph.RemoveVertex(1);

            // Assert
            Assert.Equal(2, graph.VertexCount);
            Assert.False(graph.HasVertex(1));
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(1, 3));
            Assert.True(graph.HasEdge(2, 3)); // Ребро между оставшимися вершинами должно сохраниться
        }

        [Fact]
        public void RemoveEdge_Removes_Undirected_Edge()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);

            // Act
            graph.RemoveEdge(1, 2);

            // Assert
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(2, 1)); // В обе стороны
            Assert.True(graph.HasEdge(1, 3));
            Assert.True(graph.HasEdge(2, 3));
        }

        [Fact]
        public void RemoveEdge_Non_Existent_Edge_Does_Nothing()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            // Act - Удаляем несуществующее ребро
            graph.RemoveEdge(1, 3); // Между 1 и 3 нет ребра

            // Assert - Не должно быть исключения, граф не должен измениться
            Assert.Equal(3, graph.VertexCount);
        }

        [Fact]
        public void HasVertex_Returns_True_For_Existing_Vertex()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            // Act & Assert
            Assert.True(graph.HasVertex(1));
            Assert.True(graph.HasVertex(2));
            Assert.True(graph.HasVertex(3));
        }

        [Fact]
        public void HasVertex_Returns_False_For_Non_Existent_Vertex()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);

            // Act & Assert
            Assert.False(graph.HasVertex(2));
            Assert.False(graph.HasVertex(0));
        }

        [Fact]
        public void HasEdge_Returns_True_For_Existing_Edge()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
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
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2);

            // Act & Assert
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(3, 1));
        }

        [Fact]
        public void HasEdge_Throws_Exception_For_Non_Existent_Vertices()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);

            // Act & Assert
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(2, 1));
        }

        [Fact]
        public void GetNeighbors_Returns_List_Of_Adjacent_Vertices()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);

            // Act
            var neighbors1 = graph.GetNeighbors(1);
            var neighbors2 = graph.GetNeighbors(2);
            var neighbors3 = graph.GetNeighbors(3);
            var neighbors4 = graph.GetNeighbors(4);

            // Assert
            Assert.Equal(new List<int> { 2, 3, 4 }, neighbors1.OrderBy(x => x));
            Assert.Equal(new List<int> { 1, 3 }, neighbors2.OrderBy(x => x));
            Assert.Equal(new List<int> { 1, 2 }, neighbors3.OrderBy(x => x));
            Assert.Equal(new List<int> { 1 }, neighbors4);
        }

        [Fact]
        public void GetNeighbors_Returns_Empty_List_For_Isolated_Vertex()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);

            // Act
            var neighbors = graph.GetNeighbors(1);

            // Assert
            Assert.Empty(neighbors);
        }

        [Fact]
        public void GetNeighbors_Throws_Exception_For_Non_Existent_Vertex()
        {
            // Arrange
            var graph = new MatrixGraph<int>();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => graph.GetNeighbors(1));
            Assert.Contains("not found", exception.Message);
        }

        [Fact]
        public void Clear_Removes_All_Vertices_And_Edges()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);

            // Act
            graph.Clear();

            // Assert
            Assert.Equal(0, graph.VertexCount);
            Assert.False(graph.HasVertex(1));
            Assert.False(graph.HasVertex(2));
            Assert.False(graph.HasVertex(3));
        }

        [Fact]
        public void VertexCount_Returns_Correct_Number_Of_Vertices()
        {
            // Arrange
            var graph = new MatrixGraph<int>();

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
        public void Complex_Graph_Operations_Work_Correctly()
        {
            // Arrange
            var graph = new MatrixGraph<int>();

            // Phase 1: Build a complex graph
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddVertex(6);

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(5, 6);

            // Assert Phase 1
            Assert.Equal(6, graph.VertexCount);

            // Проверяем все ребра
            Assert.True(graph.HasEdge(1, 2));
            Assert.True(graph.HasEdge(1, 3));
            Assert.True(graph.HasEdge(2, 3));
            Assert.True(graph.HasEdge(3, 4));
            Assert.True(graph.HasEdge(4, 5));
            Assert.True(graph.HasEdge(4, 6));
            Assert.True(graph.HasEdge(5, 6));

            // Проверяем симметричность
            Assert.True(graph.HasEdge(2, 1));
            Assert.True(graph.HasEdge(3, 1));
            Assert.True(graph.HasEdge(3, 2));
            Assert.True(graph.HasEdge(4, 3));
            Assert.True(graph.HasEdge(5, 4));
            Assert.True(graph.HasEdge(6, 4));
            Assert.True(graph.HasEdge(6, 5));

            // Phase 2: Remove some edges
            graph.RemoveEdge(1, 2);
            graph.RemoveEdge(4, 6);

            // Assert Phase 2
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(4, 6));

            // Phase 3: Remove a vertex
            graph.RemoveVertex(3);

            // Assert Phase 3
            Assert.Equal(5, graph.VertexCount);
            Assert.False(graph.HasVertex(3));
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(2, 3));
            Assert.False(graph.HasEdge(3, 4));

            // Phase 4: Add new vertices and edges
            graph.AddVertex(7);
            graph.AddVertex(8);
            graph.AddEdge(7, 8);
            graph.AddEdge(7, 5);

            // Assert Phase 4
            Assert.Equal(7, graph.VertexCount);
            Assert.True(graph.HasEdge(7, 8));
            Assert.True(graph.HasEdge(7, 5));
        }

        [Fact]
        public void Graph_With_String_Vertices_Works_Correctly()
        {
            // Arrange
            var graph = new MatrixGraph<string>();

            // Act
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");

            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "D");
            graph.AddEdge("D", "A");

            // Assert
            Assert.Equal(4, graph.VertexCount);

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
            Assert.False(graph.HasEdge("A", "B"));
            Assert.False(graph.HasEdge("B", "C"));
        }

        [Fact]
        public void Graph_With_Custom_Object_Vertices_Works_Correctly()
        {
            // Arrange
            var graph = new MatrixGraph<Person>();
            var person1 = new Person("Alice", 25);
            var person2 = new Person("Bob", 30);
            var person3 = new Person("Charlie", 35);

            // Act
            graph.AddVertex(person1);
            graph.AddVertex(person2);
            graph.AddVertex(person3);

            graph.AddEdge(person1, person2);
            graph.AddEdge(person2, person3);
            graph.AddEdge(person3, person1);

            // Assert
            Assert.Equal(3, graph.VertexCount);

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
            var graph = new MatrixGraph<int>();

            // Act
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);

            // Добавляем ребра только между 3 и 4
            graph.AddEdge(3, 4);

            // Assert
            Assert.Equal(4, graph.VertexCount);

            // Изолированные вершины
            Assert.True(graph.HasVertex(1));
            Assert.True(graph.HasVertex(2));
            Assert.Empty(graph.GetNeighbors(1));
            Assert.Empty(graph.GetNeighbors(2));

            // Вершины с ребрами
            Assert.Equal(new List<int> { 4 }, graph.GetNeighbors(3));
            Assert.Equal(new List<int> { 3 }, graph.GetNeighbors(4));

            // Изолированные вершины не должны иметь ребер
            Assert.False(graph.HasEdge(1, 2));
            Assert.False(graph.HasEdge(1, 3));
            Assert.False(graph.HasEdge(2, 4));
        }

        [Fact]
        public void Matrix_Resize_Works_Correctly()
        {
            // Arrange
            var graph = new MatrixGraph<int>();

            // Act - Добавляем много вершин (больше чем InitialCapacity)
            for (int i = 0; i < 20; i++)
            {
                graph.AddVertex(i);
            }

            // Assert
            Assert.Equal(20, graph.VertexCount);

            // Добавляем ребра
            for (int i = 0; i < 10; i++)
            {
                graph.AddEdge(i, i + 1);
            }

            // Проверяем ребра
            for (int i = 0; i < 10; i++)
            {
                Assert.True(graph.HasEdge(i, i + 1));
                Assert.True(graph.HasEdge(i + 1, i));
            }
        }

        [Fact]
        public void RemoveVertex_Preserves_Matrix_Integrity()
        {
            // Arrange
            var graph = new MatrixGraph<int>();
            for (int i = 0; i < 5; i++)
            {
                graph.AddVertex(i);
            }

            // Создаем полный граф K5
            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    graph.AddEdge(i, j);
                }
            }

            // Act - Удаляем вершину 2
            graph.RemoveVertex(2);

            // Assert
            Assert.Equal(4, graph.VertexCount);

            // Проверяем что оставшиеся вершины 0,1,3,4 имеют правильные связи
            // Вершина 0 должна быть соединена с 1,3,4
            var neighbors0 = graph.GetNeighbors(0);
            Assert.Equal(3, neighbors0.Count);
            Assert.Contains(1, neighbors0);
            Assert.Contains(3, neighbors0);
            Assert.Contains(4, neighbors0);

            // Вершина 1 должна быть соединена с 0,3,4
            var neighbors1 = graph.GetNeighbors(1);
            Assert.Equal(3, neighbors1.Count);
            Assert.Contains(0, neighbors1);
            Assert.Contains(3, neighbors1);
            Assert.Contains(4, neighbors1);

            // Вершина 3 должна быть соединена с 0,1,4
            var neighbors3 = graph.GetNeighbors(3);
            Assert.Equal(3, neighbors3.Count);
            Assert.Contains(0, neighbors3);
            Assert.Contains(1, neighbors3);
            Assert.Contains(4, neighbors3);

            // Вершина 4 должна быть соединена с 0,1,3
            var neighbors4 = graph.GetNeighbors(4);
            Assert.Equal(3, neighbors4.Count);
            Assert.Contains(0, neighbors4);
            Assert.Contains(1, neighbors4);
            Assert.Contains(3, neighbors4);
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
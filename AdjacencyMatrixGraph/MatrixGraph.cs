namespace AdjacencyMatrixGraph
{
    public class MatrixGraph<T>
    {
        private const int InitialCapacity = 16;
        private const string VERTEX_NOT_FOUND_MESSAGE = "Vertex not found in the graph.";


        /// <summary>
        /// Список вершин(List<T>), 
        /// хранит порядок вершин для индексации в матрице(индекс вершины = позиция в списке).
        /// </summary>
        private IList<T> vertices { get; set; }

        /// <summary>
        /// 2D массив целых(int[][]), 
        /// размер V x V, где V — количество вершин; 
        /// matrix[i][j] = 1 если ребро от vertices[i] к vertices[j], 
        /// иначе 0 (симметрично для undirected).
        /// </summary>
        private int[,] matrix { get; set; }

        /// <summary>
        /// Возвращает размер vertices.
        /// </summary>
        public int VertexCount => vertices.Count;

        /// <summary>
        /// Подсчитывает количество 1 в матрице / 2 (для undirected).
        /// </summary>
        public int EdgeCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 1)
                        {
                            count++;
                        }
                    }
                }
                return count / 2;
            }
        }

        /// <summary>
        /// Инициализирует пустой граф, создаёт пустой vertices и matrix = new int[0][].
        /// </summary>
        public MatrixGraph()
        {
            vertices = new List<T>();
            matrix = new int[InitialCapacity, InitialCapacity];
        }

        /// <summary>
        /// Добавляет вершину в vertices, расширяет matrix до(V+1) x(V+1),
        /// инициализирует новую строку/столбец нулями.
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(T vertex)
        {
            if (vertices.Contains(vertex))
            {
                return; // Вершина уже существует
            }
            vertices.Add(vertex);
            if (matrix.GetLength(0) < VertexCount)
            {
                var newV = VertexCount << 1;
                var newM = CopyMatrix(matrix, newV);

                matrix = newM;
            }
        }

        /// <summary>
        /// Находит индексы вершин, 
        /// устанавливает matrix[index1][index2] = 1 
        /// и matrix[index2][index1] = 1(если undirected и не самопетля).
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        public void AddEdge(T vertex1, T vertex2)
        {
            if (vertex1.Equals(vertex2))
            {
                return;
            }

            var vertex1Index = vertices.IndexOf(vertex1);
            var vertex2Index = vertices.IndexOf(vertex2);

            if (vertex1Index == -1 || vertex2Index == -1)
            {
                AddVertex(vertex1);
                AddVertex(vertex2);
            }

            matrix[vertex1Index, vertex2Index] = 1;
            matrix[vertex2Index, vertex1Index] = 1;
        }

        /// <summary>
        /// Находит индекс, удаляет вершину из vertices, сжимает matrix(удаляет строку/столбец по индексу).
        /// </summary>
        /// <param name="vertex"></param>
        public void RemoveVertex(T vertex)
        {
            var verIndex = vertices.IndexOf(vertex);

            if (verIndex == -1) return;

            vertices.RemoveAt(verIndex);

            int[,] newMatrix = new int[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];

            for (int i = 0, n = 0; n < matrix.GetLength(0); n++)
            {
                if (n == verIndex) continue;
                for (int j = 0, m = 0; m < matrix.GetLength(1); m++)
                {
                    if (m == verIndex) continue;
                    newMatrix[i, j] = matrix[n, m];
                    j++;
                }
                i++;
            }
            matrix = newMatrix;
        }

        /// <summary>
        /// Находит индексы, устанавливает matrix[index1][index2] = 0 и matrix[index2][index1] = 0.
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        public void RemoveEdge(T vertex1, T vertex2)
        {
            var index1 = vertices.IndexOf(vertex1);
            var index2 = vertices.IndexOf(vertex2);

            if (index1 == -1 || index2 == -1) return;

            matrix[index1, index2] = 0;
            matrix[index2, index1] = 0;
        }

        /// <summary>
        /// Проверяет наличие в vertices.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool HasVertex(T vertex)
        {
            return vertices.Contains(vertex);
        }

        /// <summary>
        /// Находит индексы, проверяет matrix[index1][index2] == 1.
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        public bool HasEdge(T vertex1, T vertex2)
        {
            var index1 = vertices.IndexOf(vertex1);
            var index2 = vertices.IndexOf(vertex2);

            if (index1 == -1 || index2 == -1) return false;

            return matrix[index1, index2] == 1;
        }

        /// <summary>
        /// Находит индекс, проходит по строке matrix[index], собирает вершины где 1.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<T> GetNeighbors(T vertex)
        {
            var verIndex = vertices.IndexOf(vertex);
            if (verIndex == -1)
                throw new ArgumentException(VERTEX_NOT_FOUND_MESSAGE);

            var neighbors = new List<T>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[verIndex, i] == 1)
                {
                    neighbors.Add(vertices[i]);
                }
            }
            return neighbors;
        }

        /// <summary>
        /// Очищает vertices и matrix.
        /// </summary>
        public void Clear()
        {
            vertices.Clear();
            matrix = new int[InitialCapacity, InitialCapacity];
        }

        private void ResizeMatrix()
        {
            int newSize = matrix.GetLength(0) * 2;
            int[,] newMatrix = new int[newSize, newSize];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }
            matrix = newMatrix;
        }

        private static int[,] CopyMatrix(int[,] source, int V)
        {
            var destination = new int[V, V];
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    destination[i, j] = source[i, j];
                }
            }
            return destination;
        }
    }
}

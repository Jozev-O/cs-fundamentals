namespace Graph__Undirected_
{
    public class UndirectedGraph<T> where T : notnull
    {
        /// <summary>
        /// Коллекция вершин
        /// </summary>
        private HashSet<T> vertices { get; set; }

        /// <summary>
        /// Словарь соседей(Dictionary<T, List<T>>), 
        /// где ключ — вершина, значение — список смежных вершин(без дубликатов).
        /// </summary>
        private Dictionary<T, IList<T>> adjacencyList { get; set; }

        /// <summary>
        /// Возвращает количество вершин.
        /// </summary>
        public int VertexCount => vertices.Count;

        /// <summary>
        /// Возвращает общее количество рёбер.
        /// </summary>
        public int EdgeCount => adjacencyList.Values.Sum(neighbors => neighbors.Count) / 2;

        /// <summary>
        /// Инициализирует пустой граф, создаёт vertices и adjacencyList.
        /// </summary>
        public UndirectedGraph()
        {
            vertices = new HashSet<T>();
            adjacencyList = new Dictionary<T, IList<T>>();
        }

        /// <summary>
        /// Добавляет вершину, если её нет; инициализирует пустой список соседей.
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(T vertex)
        {
            vertices.Add(vertex);
            if (!adjacencyList.ContainsKey(vertex))
            {
                adjacencyList[vertex] = new List<T>();
            }
        }

        /// <summary>
        /// Добавляет undirected ребро между вершинами(если они существуют или добавляет их); 
        /// добавляет vertex2 в список vertex1 и наоборот, без самопетель.
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        public void AddEdge(T vertex1, T vertex2)
        {
            if (vertex1.Equals(vertex2))
            {
                return; // No self-loops
            }
            AddVertex(vertex1);
            AddVertex(vertex2);

            if (!adjacencyList[vertex1].Contains(vertex2))
            {
                adjacencyList[vertex1].Add(vertex2);
            }
            if (!adjacencyList[vertex2].Contains(vertex1))
            {
                adjacencyList[vertex2].Add(vertex1);
            }
        }

        /// <summary>
        /// Удаляет вершину и все рёбра от/к ней(обновляет списки соседей у других вершин).
        /// </summary>
        /// <param name="vertex"></param>
        public void RemoveVertex(T vertex)
        {
            vertices.Remove(vertex);
            if (adjacencyList.ContainsKey(vertex))
            {
                foreach (var neighbor in adjacencyList[vertex])
                {
                    adjacencyList[neighbor].Remove(vertex);
                }
                adjacencyList.Remove(vertex);
            }
        }

        /// <summary>
        /// Удаляет ребро между вершинами, если существует(удаляет из списков друг друга).
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        public void RemoveEdge(T vertex1, T vertex2)
        {
            if (adjacencyList.ContainsKey(vertex1))
            {
                adjacencyList[vertex1].Remove(vertex2);
            }
            if (adjacencyList.ContainsKey(vertex2))
            {
                adjacencyList[vertex2].Remove(vertex1);
            }
        }

        /// <summary>
        ///  Проверяет наличие вершины.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool HasVertex(T vertex)
        {
            return vertices.Contains(vertex);
        }

        /// <summary>
        /// Проверяет наличие ребра.
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        public bool HasEdge(T vertex1, T vertex2)
        {
            return adjacencyList.ContainsKey(vertex1) && adjacencyList[vertex1].Contains(vertex2);
        }

        /// <summary>
        /// Возвращает список соседей вершины(или пустой, если нет).
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<T> GetNeighbors(T vertex)
        {
            var neighbors = new List<T>();
            if (adjacencyList.TryGetValue(vertex, out IList<T>? value))
            {
                neighbors.AddRange(value);
            }
            return neighbors;
        }

        /// <summary>
        /// Очищает вершины и adjacencyList.
        /// </summary>
        public void Clear()
        {
            vertices.Clear();
            adjacencyList.Clear();
        }
    }
}
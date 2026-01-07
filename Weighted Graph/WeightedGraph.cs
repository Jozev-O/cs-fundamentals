namespace Weighted_Graph
{
    public class WeightedGraph<T> where T : notnull
    {
        /// <summary>
        /// Коллекция уникальных вершин.
        /// </summary>
        private HashSet<T> vertices { get; set; }

        /// <summary>
        /// Словарь соседей с весами, 
        /// где ключ — вершина, значение — список пар(соседняя вершина, вес ребра).
        /// </summary>
        private Dictionary<T, List<KeyValuePair<T, double>>> adjacencyList { get; set; }

        /// <summary>
        /// Возвращает количество вершин.
        /// </summary>
        public int VertexCount => vertices.Count;

        /// <summary>
        /// Вычисляет количество рёбер(сумма длин списков / 2 если undirected).
        /// </summary>
        public int EdgeCount => adjacencyList.Values.Sum(neighbors => neighbors.Count) / 2;

        /// <summary>
        /// Инициализирует пустой граф, создаёт vertices и adjacencyList.
        /// </summary>
        public WeightedGraph()
        {
            vertices = new HashSet<T>();
            adjacencyList = new Dictionary<T, List<KeyValuePair<T, double>>>();
        }

        /// <summary>
        /// Добавляет вершину, если её нет; инициализирует пустой список в adjacencyList.
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(T vertex)
        {
            if (vertices.Add(vertex))
            {
                adjacencyList[vertex] = new List<KeyValuePair<T, double>>();
            }
        }

        /// <summary>
        /// Добавляет вершины, если нужно;
        /// добавляет пару(vertex2, weight) в список vertex1 и(vertex1, weight) 
        /// в список vertex2(если undirected); без самопетель, обновляет вес если ребро существует.
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <param name="weight"></param>
        public void AddEdge(T vertex1, T vertex2, double weight)
        {
            if (vertex1.Equals(vertex2))
            {
                return; // No self-loops allowed
            }
            AddVertex(vertex1);
            AddVertex(vertex2);

            if (HasEdge(vertex1, vertex2))
            {
                RemoveEdge(vertex1, vertex2);
            }

            adjacencyList[vertex1].Add(new KeyValuePair<T, double>(vertex2, weight));
            adjacencyList[vertex2].Add(new KeyValuePair<T, double>(vertex1, weight));
        }

        /// <summary>
        /// Удаляет вершину из vertices и adjacencyList; 
        /// удаляет все упоминания vertex из списков других вершин.
        /// </summary>
        /// <param name="vertex"></param>
        public void RemoveVertex(T vertex)
        {
            vertices.Remove(vertex);
            if (adjacencyList.Remove(vertex))
            {
                foreach (var neighbors in adjacencyList.Values)
                {
                    neighbors.RemoveAll(pair => pair.Key.Equals(vertex));
                }
            }
        }

        /// <summary>
        /// Удаляет пару с vertex2 из списка vertex1 и с vertex1 из списка vertex2(если undirected).
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        public void RemoveEdge(T vertex1, T vertex2)
        {
            if (adjacencyList.ContainsKey(vertex1))
            {
                adjacencyList[vertex1].RemoveAll(pair => pair.Key.Equals(vertex2));
            }
            if (adjacencyList.ContainsKey(vertex2))
            {
                adjacencyList[vertex2].RemoveAll(pair => pair.Key.Equals(vertex1));
            }
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
        /// Проверяет наличие пары с vertex2 в списке vertex1.
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        public bool HasEdge(T vertex1, T vertex2)
        {
            return adjacencyList.ContainsKey(vertex1) &&
                   adjacencyList[vertex1].Exists(pair => pair.Key.Equals(vertex2));
        }

        /// <summary>
        /// Возвращает вес ребра между вершинами или бросает exception если ребра нет.
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        public double GetEdgeWeight(T vertex1, T vertex2)
        {
            if (!HasEdge(vertex1, vertex2))
            {
                throw new InvalidOperationException("Edge does not exist.");
            }
            return adjacencyList[vertex1]
                .Find(pair => pair.Key.Equals(vertex2)).Value;
        }

        /// <summary>
        /// Возвращает список соседей с весами(копию или пустой, если вершина не существует).
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<KeyValuePair<T, double>> GetNeighbors(T vertex)
        {
            return adjacencyList.ContainsKey(vertex)
                ? new List<KeyValuePair<T, double>>(adjacencyList[vertex])
                : new List<KeyValuePair<T, double>>();
        }

        /// <summary>
        /// Очищает vertices и adjacencyList.
        /// </summary>
        public void Clear()
        {
            vertices.Clear();
            adjacencyList.Clear();
        }
    }
}

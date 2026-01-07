namespace Graph__Directed_
{
    public class DirectedGraph<T> where T : notnull
    {
        /// <summary>
        /// Коллекция уникальных вершин, для проверки наличия и подсчёта.
        /// </summary>
        private HashSet<T> vertices { get; set; }

        /// <summary>
        /// Словарь исходящих соседей, 
        /// где ключ — вершина, значение — список вершин, к которым ведут рёбра от неё(без дубликатов рёбер).
        /// </summary>
        private Dictionary<T, IList<T>> adjacencyList { get; set; }

        /// <summary>
        /// Возвращает размер vertices.
        /// </summary>
        public int VertexCount => vertices.Count;

        /// <summary>
        /// Вычисляет общее количество рёбер(сумма длин всех списков в adjacencyList).
        /// </summary>
        public int EdgeCount => adjacencyList.Values.Sum(neighbors => neighbors.Count);

        /// <summary>
        /// Инициализирует пустой граф, создаёт пустые vertices и adjacencyList.
        /// </summary>
        public DirectedGraph()
        {
            vertices = new HashSet<T>();
            adjacencyList = new Dictionary<T, IList<T>>();
        }

        /// <summary>
        /// Добавляет вершину в vertices, если нет; создаёт пустой список в adjacencyList.
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(T vertex)
        {
            if (vertices.Add(vertex))
            {
                adjacencyList[vertex] = new List<T>();
            }
        }

        /// <summary>
        /// Добавляет вершины, если нужно; добавляет target в список source(если не существует и не самопетля).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void AddEdge(T source, T target)
        {
            if (source.Equals(target))
            {
                return; // Игнорируем самопетли
            }
            AddVertex(source);
            AddVertex(target);
            if (!adjacencyList[source].Contains(target))
            {
                adjacencyList[source].Add(target);
            }
        }

        /// <summary>
        /// Удаляет вершину из vertices и adjacencyList;
        /// удаляет все входящие рёбра к ней из списков других вершин 
        /// (проходит по всем спискам и удаляет упоминания vertex).
        /// </summary>
        /// <param name="vertex"></param>
        public void RemoveVertex(T vertex)
        {
            vertices.Remove(vertex);
            if (adjacencyList.Remove(vertex))
            {
                foreach (var neighbors in adjacencyList.Values)
                {
                    neighbors.Remove(vertex);
                }
            }
        }

        /// <summary>
        /// Удаляет target из списка source, если существует.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void RemoveEdge(T source, T target)
        {
            if (adjacencyList.ContainsKey(source))
            {
                adjacencyList[source].Remove(target);
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
        /// Проверяет наличие target в списке source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool HasEdge(T source, T target)
        {
            return adjacencyList.ContainsKey(source) && adjacencyList[source].Contains(target);
        }

        /// <summary>
        /// Возвращает список исходящих соседей(копию или пустой).
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public IList<T> GetOutNeighbors(T vertex)
        {
            return adjacencyList.ContainsKey(vertex) ? [.. adjacencyList[vertex]] : new List<T>();
        }

        /// <summary>
        /// Возвращает список входящих соседей(проходит по всем спискам и собирает вершины, где vertex в их списке).
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<T> GetInNeighbors(T vertex)
        {
            return adjacencyList
                .Where(kvp => kvp.Value.Contains(vertex))
                .Select(kvp => kvp.Key)
                .ToList();
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

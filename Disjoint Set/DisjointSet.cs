namespace Disjoint_Set
{
    public class DisjointSet<T>
    {
        /// <summary>
        /// Словарь(Dictionary<T, T>), 
        /// где ключ — элемент, 
        /// значение — родитель в дереве множества (изначально сам элемент).
        /// </summary>
        private Dictionary<T, T> parent { get; set; } = [];

        /// <summary>
        /// Словарь(Dictionary<T, int>), 
        /// для рангов деревьев (используется в union by rank для баланса).
        /// </summary>
        private Dictionary<T, int> rank { get; set; } = [];

        /// <summary>
        /// Инициализирует структуру с элементами,
        /// устанавливает parent на себя, rank в 0.
        /// </summary>
        /// <param name="elements"></param>
        public DisjointSet(IEnumerable<T> elements)
        {
            var set = new HashSet<T>();
            foreach (var item in elements)
            {
                set.Add(item);
            }
            foreach (var element in set)
            {
                parent.Add(element, element);
                rank.Add(element, 0);
            }
        }

        /// <summary>
        /// Находит корень множества для элемента с path compression(сжимает путь, устанавливая родителей напрямую к корню для ускорения будущих поисков); возвращает корень или бросает exception если элемент не в структуре.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T Find(T element)
        {
            if (!parent.ContainsKey(element))
            {
                throw new InvalidOperationException("no such element in set");
            }

            T current = element;
            while (!parent[current].Equals(current))
            {
                current = parent[current];
            }

            T root = current;
            current = element;
            while (!parent[current].Equals(current))
            {
                T next = parent[current];
                parent[current] = root;
                current = next;
            }

            return root;
        }

        /// <summary>
        /// Объединяет множества элементов: находит корни, связывает меньшее дерево к большему по rank/size для баланса; если корни одинаковы — ничего не делает.
        /// </summary>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        public void Union(T element1, T element2)
        {

            T root1 = Find(element1);
            T root2 = Find(element2);

            if (root1.Equals(root2))
            {
                return;
            }
            if (rank[root1] < rank[root2])
            {
                parent[root1] = root2;
            }
            else if (rank[root1] > rank[root2])
            {
                parent[root2] = root1;
            }
            else
            {
                parent[root2] = root1;
                rank[root1] += 1;
            }
        }

        /// <summary>
        /// Проверяет, принадлежат ли элементы одному множеству (сравнивает корни через Find).
        /// </summary>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        /// <returns></returns>
        public bool SameSet(T element1, T element2)
        {
            return Find(element1).Equals(Find(element2));
        }

        /// <summary>
        /// Возвращает количество отдельных множеств (подсчёт уникальных корней).
        /// </summary>
        /// <returns></returns>
        public int CountSets()
        {
            var unique_roots = new HashSet<T>();
            foreach (var element in parent.Keys)
                unique_roots.Add(Find(element));
            return unique_roots.Count;
        }
    }
}

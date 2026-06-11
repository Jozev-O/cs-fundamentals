namespace SkipList
{
    public class SkipList<T> where T : IComparable<T>
    {
        /// <summary>
        /// Специальный узел-заголовок(Node с value = default(T), 
        /// forward — массив ссылок на следующий узел на каждом уровне).
        /// </summary>
        private Node<T> _head;

        /// <summary>
        /// Текущий максимальный уровень в списке(начинается с 0).
        /// </summary>
        private int _level = 0;

        /// <summary>
        ///  Максимально разрешённый уровень(обычно 16 или 32).
        /// </summary>
        private int _maxLevel = 32;

        /// <summary>
        /// Вероятность поднятия узла на следующий уровень(стандартно 0.5).
        /// </summary>
        private double _probability = 0.5;

        /// <summary>
        /// Количество реальных элементов(не считая head).
        /// </summary>
        private int _count = 0;

        /// <summary>
        /// Инициализирует head с forward-массивом размера maxLevel+1, level = 0, count = 0.
        /// </summary>
        /// <param name="probability"></param>
        /// <param name="maxLevel"></param>
        public SkipList(double probability = 0.5, int maxLevel = 32)
        {
            _head = new Node<T>();
            _head.Forward = new List<T>(_maxLevel + 1);
        }

        /// <summary>
        /// Создаёт обычный узел с forward - массивом размера level+1.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public Node<T> CreateNode(T value, int level)
        {
            var newNode = new Node<T>();
            newNode.Value = value;
            newNode.Forward = new List<T>(level + 1);
            return newNode;
        }

        /// <summary>
        /// Генерирует уровень нового узла(начинает с 0, 
        /// пока Random.NextDouble() < probability и не достигнут maxLevel).
        /// </summary>
        /// <returns></returns>
        public int RandomLevel()
        {
            return 0;
        }

        /// <summary>
        /// Ищет позицию вставки(как в поиске), 
        /// создаёт новый узел с случайным уровнем, 
        /// обновляет все forward-ссылки на нужных уровнях.
        /// </summary>
        /// <param name="value"></param>
        void Insert(T value)
        {

        }

        /// <summary>
        /// Ищет значение, начиная с верхнего уровня вниз(как в обычном поиске).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            return false;
        }

        /// <summary>
        ///  Находит узел и перестраивает forward - ссылки на всех уровнях, где он присутствовал.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(T value)
        {
            return false;
        }

        /// <summary>
        /// Возвращает минимальный элемент(идёт по самому нижнему уровню).
        /// </summary>
        /// <returns></returns>
        public T Min()
        {
            return _head.Value;
        }

        /// <summary>
        /// Возвращает максимальный элемент(идёт по самому нижнему уровню).
        /// </summary>
        /// <returns></returns>
        public T Max()
        {
            return _head.Value;
        }

        /// <summary>
        /// Возвращает count.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Сбрасывает всё к начальному состоянию.
        /// </summary>
        public void Clear()
        {

        }
    }
}

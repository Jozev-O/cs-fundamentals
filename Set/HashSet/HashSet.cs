namespace Set.HashSet
{
    public class HashSet<T> : Interface.ISet<T>
    {
        private const int DEFAULT_CAPACITY = 16;

        /// <summary>
        /// Массив списка ключей, 
        /// где каждый элемент массива — цепочка для разрешения коллизий.
        /// Размер массива — количество ведер.
        /// </summary>
        private IList<IList<T>> _buckets;

        /// <summary>
        /// Дробное число (0.75), порог загрузки, 
        /// при превышении которого таблица расширяется для поддержания эффективности.
        /// </summary>
        private float _loadFactorThreshold { get; set; } = 0.75f;

        /// <summary>
        /// Целое число, текущее количество ведер (размер buckets).
        /// </summary>
        private int _capacity { get { return _buckets.Count; } }

        /// <summary>
        /// Целое число, хранящее текущее количество элементов в таблице.
        /// </summary>
        private int _count { get; set; } = 0;

        public int Count { get { return _count; } }

        public HashSet()
        {
            _buckets = new List<IList<T>>(DEFAULT_CAPACITY);
            for (int i = 0; i < DEFAULT_CAPACITY; i++)
            {
                _buckets.Add(new List<T>());
            }
        }

        /// <summary>
        /// Инициализирует таблицу с заданным начальным количеством ведер, 
        /// устанавливает buckets как новый массив этого размера, count в 0.
        /// </summary>
        /// <param name="initialCapacity">Количество buckets</param>
        public HashSet(int initialCapacity)
        {
            _buckets = new List<IList<T>>(initialCapacity);
            for (int i = 0; i < initialCapacity; i++)
            {
                _buckets.Add(new List<T>());
            }
        }

        /// <summary> 
        /// Вычисляет хеш-код ключа и приводит его к индексу 
        /// в диапазоне 0 до capacity-1 (например, Math.Abs(key.GetHashCode()) % capacity). 
        /// Обеспечивает равномерное распределение.
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Индекс в диапазоне 0 до capacity-1 </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int GetIndex(T key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (_buckets.Count == 0) return 0;

            return (key.GetHashCode() & 0x7FFFFFFF) % _capacity;
        }

        /// <summary>
        /// Вставляет ключ. Вычисляет индекс через GetHash, 
        /// ищет ключ в цепочке buckets[index]; если найден — возвращает True, 
        /// иначе добавляет новый ключ и возвращает False. Увеличивает count, если новый. 
        /// Если count / capacity > loadFactorThreshold, вызывает Resize.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Add(T key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if ((float)(_count + 1) / _capacity > _loadFactorThreshold)
            {
                Resize();
            }

            var index = GetIndex(key);
            var chain = _buckets[index];

            for (int i = 0; i < chain.Count; i++)
            {
                if (chain[i].Equals(key))
                {
                    return false;
                }
            }

            chain.Add(key);
            _count++;
            return true;
        }

        /// <summary>
        /// Удаляет ключ. Вычисляет индекс, находит и 
        /// удаляет из цепочки, уменьшает count, возвращает true если удалено.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(T key)
        {
            var index = GetIndex(key);
            var chain = _buckets[index];

            for (int i = 0; i < chain.Count; i++)
            {
                if (chain[i].Equals(key))
                {
                    chain.RemoveAt(i);
                    _count--;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверяет наличие ключа. Вычисляет индекс, 
        /// ищет в цепочке, возвращает true если найден.
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Возвращает true если в списке имеется заданный ключ</returns>
        public bool Contains(T key)
        {
            var index = GetIndex(key);
            var chain = _buckets[index];

            foreach (var pair in chain)
            {
                if (pair.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Удваивает capacity, создает новый массив ведер, 
        /// перехеширует все существующие ключи в новый массив 
        /// (перемещает из старого в новый по новым индексам), обновляет buckets.
        /// </summary>
        public void Resize()
        {
            // 1. Определяем новый размер
            int newSize = GetNewSize();

            // 2. Создаем новые бакеты
            var newBuckets = new List<IList<T>>(newSize);

            for (int i = 0; i < newSize; i++)
            {
                newBuckets.Add(new List<T>());
            }

            // 3. Перехешируем все существующие элементы
            for (int i = 0; i < _buckets.Count; i++)
            {
                var current = _buckets[i];
                if (current == null) continue;
                foreach (var pair in current)
                {
                    // Вычисляем новый индекс
                    int newIndex = (pair.GetHashCode() & 0x7FFFFFFF) % newSize;

                    // Вставляем в новый массив бакетов
                    newBuckets[newIndex].Add(pair);
                }
            }
            // 4. Заменяем старые бакеты новыми
            _buckets = newBuckets;
        }

        /// <summary>
        /// Возвращает новый размер для массива бакетов (обычно удваивает текущий размер).
        /// </summary>
        /// <returns>Возвращает новый размер для массива бакетов</returns>
        private int GetNewSize()
        {
            return _buckets.Count * 2;
        }

        /// <summary>
        /// Очищает все цепочки, устанавливает count в 0 (не меняет capacity).
        /// </summary>
        public void Clear()
        {
            _count = 0;
            for (int i = 0; i < _capacity; i++)
            {
                _buckets[i].Clear();
            }
        }

        public override string? ToString()
        {
            var result = "";
            for (int i = 0; i < _buckets.Count; i++)
            {
                result += $"Bucket {i}:\n";
                foreach (var pair in _buckets[i])
                {
                    result += $"  [{pair}]\n";
                }
            }
            return result;
        }
    }
}
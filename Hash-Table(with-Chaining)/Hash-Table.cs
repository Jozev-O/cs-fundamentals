namespace Hash_Table_with_Chaining_
{
    public class HashTable<TKey, TValue>
    {
        private const string KEY_NOT_FOUND_MESSAGE = "The specified key was not found in the hash table.";
        private const int DEFAULT_CAPACITY = 16;
        /// <summary>
        /// Массив списков пар ключ-значение, 
        /// где каждый элемент массива — цепочка для разрешения коллизий.
        /// Размер массива — количество ведер.
        /// </summary>
        private IList<IList<KeyValuePair<TKey, TValue>>> _buckets;

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

        /// <summary>
        /// Целое число, хранящее текущее количество элементов в таблице.
        /// </summary>
        public int Count { get { return _count; } }

        public HashTable()
        {
            _buckets = new List<IList<KeyValuePair<TKey, TValue>>>(DEFAULT_CAPACITY);
            for (int i = 0; i < DEFAULT_CAPACITY; i++)
            {
                _buckets.Add(new List<KeyValuePair<TKey, TValue>>());
            }
        }

        /// <summary>
        /// Инициализирует таблицу с заданным начальным количеством ведер, 
        /// устанавливает buckets как новый массив этого размера, count в 0.
        /// </summary>
        /// <param name="initialCapacity">Количество buckets</param>
        public HashTable(int initialCapacity)
        {
            _buckets = new List<IList<KeyValuePair<TKey, TValue>>>(initialCapacity);
            for (int i = 0; i < initialCapacity; i++)
            {
                _buckets.Add(new List<KeyValuePair<TKey, TValue>>());
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
        public int GetIndex(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (_buckets.Count == 0) return 0;

            return (key.GetHashCode() & 0x7FFFFFFF) % _capacity;
        }

        /// <summary>
        /// Вставляет или обновляет пару. Вычисляет индекс через GetHash, 
        /// ищет ключ в цепочке buckets[index]; если найден — обновляет значение, 
        /// иначе добавляет новую пару в цепочку. Увеличивает count, если новый. 
        /// Если count / capacity > loadFactorThreshold, вызывает Resize.
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(TKey key, TValue value)
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
            var newPair = new KeyValuePair<TKey, TValue>(key, value);

            for (int i = 0; i < chain.Count; i++)
            {
                if (chain[i].Key.Equals(key))
                {
                    chain[i] = newPair;
                    return;
                }
            }

            chain.Add(newPair);
            _count++;
        }

        /// <summary>
        /// Находит значение по ключу. Вычисляет индекс, ищет ключ в цепочке 
        ///  buckets[index]; возвращает значение, если найден, иначе бросает
        ///  исключение или возвращает default (реши сам, но укажи в описании).
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Значение по ключу, иначе KeyNotFoundException</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public TValue Get(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var index = GetIndex(key);
            var chain = _buckets[index];

            foreach (var pair in chain)
            {
                if (pair.Key.Equals(key))
                {
                    return pair.Value;
                }
            }

            throw new KeyNotFoundException(KEY_NOT_FOUND_MESSAGE);
        }

        /// <summary>
        /// Удаляет пару по ключу. Вычисляет индекс, находит и 
        /// удаляет из цепочки, уменьшает count, возвращает true если удалено.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            var index = GetIndex(key);
            var chain = _buckets[index];

            for (int i = 0; i < chain.Count; i++)
            {
                if (chain[i].Key.Equals(key))
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
        public bool ContainsKey(TKey key)
        {
            var index = GetIndex(key);
            var chain = _buckets[index];

            foreach (var pair in chain)
            {
                if (pair.Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Удваивает capacity, создает новый массив ведер, 
        /// перехеширует все существующие пары в новый массив 
        /// (перемещает из старого в новый по новым индексам), обновляет buckets.
        /// </summary>
        public void Resize()
        {
            // 1. Определяем новый размер
            int newSize = GetNewSize();

            // 2. Создаем новые бакеты
            var newBuckets = new List<IList<KeyValuePair<TKey, TValue>>>(newSize);

            for (int i = 0; i < newSize; i++)
            {
                newBuckets.Add(new List<KeyValuePair<TKey, TValue>>());
            }

            // 3. Перехешируем все существующие элементы
            for (int i = 0; i < _buckets.Count; i++)
            {
                var current = _buckets[i];
                if (current == null) continue;
                foreach (var pair in current)
                {
                    // Вычисляем новый индекс
                    int newIndex = (pair.Key.GetHashCode() & 0x7FFFFFFF) % newSize;

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
                    result += $"  {pair.Key}: {pair.Value}\n";
                }
            }
            return result;
        }
    }
}
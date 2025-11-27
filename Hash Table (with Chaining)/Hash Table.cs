namespace Hash_Table__with_Chaining_
{
    internal class HashTable<TKey, TValue>
    {
        private readonly float _loadFactor = 0.75f;
        private IList<List<KeyValuePair<TKey, TValue>>> _buckets = [];
        private int _size { get { return _buckets.Count; } }
        private int _count { get; set; } = 0;

        public HashTable() { }

        // добавить пару, обработать коллизию цепочкой.
        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            //Вычислить хэш-код ключа и индекс бакета:
            //index = (key.GetHashCode() & 0x7FFFFFFF) % buckets.Length(чтобы избежать отрицательных значений).
            var index = GetIndex(key);

            //В бакете(списке по индексу) пройти по элементам и проверить,
            //существует ли ключ(сравнить ключи с учетом Equals).


            if (index >= _buckets.Count)
            {
                _buckets.Add(new List<KeyValuePair<TKey, TValue>>());
            }

            var chain = _buckets[index];
            var newPair = new KeyValuePair<TKey, TValue>(key, value);
            var found = false;

            foreach (var pair in chain)
            {
                if (pair.Key.Equals(key))
                {
                    // Если ключ найден, обновить существующее значение.
                    _buckets[index].Remove(pair);
                    _buckets[index].Add(new KeyValuePair<TKey, TValue>(key, value));
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                //Если ключ не найден, добавить новую пару ключ -значение в конец списка бакета.
                _buckets[index].Add(new KeyValuePair<TKey, TValue>(key, value));

                //Увеличить count(количество элементов).
                _count++;
            }


            //Проверить загрузку: если count > buckets.Length * loadFactor,
            //вызвать Resize для расширения таблицы.
            if (_count > _size * _loadFactor)
            {
                Resize();
            }
        }
        private int GetIndex(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (_buckets.Count == 0) return 0;

            return (key.GetHashCode() & 0x7FFFFFFF) % _size;
        }
        private void Resize()
        {

            // 1. Определяем новый размер
            int newSize = GetNewSize();

            // 2. Создаем новые бакеты
            var newBuckets = new List<KeyValuePair<TKey, TValue>>[newSize];

            // 3. Перехешируем все существующие элементы
            for (int i = 0; i < _buckets.Count; i++)
            {
                var current = _buckets[i];
                if (current == null) continue;
                for (int j = 0; j < current.Count; j++)
                {
                    // Вычисляем новый индекс
                    int newIndex = (current[j].Key.GetHashCode() & 0x7FFFFFFF) % newSize;

                    // Вставляем в новый массив бакетов
                    newBuckets[newIndex] = current;
                }

                // 4. Заменяем старые бакеты новыми
                _buckets = newBuckets;
            }
            Console.WriteLine("recize");
        }

        private int GetNewSize()
        {
            return (int)(_buckets.Count * 2);
        }

        // найти и вернуть значение или default.
        //private void Search(TKey key)
        //{

        //}
        //HashTable(TKey key) : удалить по ключу.
        //Resize(): увеличить таблицу при превышении loadFactor.
        //GetHash(TKey key): вычислить хэш и индекс бакета.
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

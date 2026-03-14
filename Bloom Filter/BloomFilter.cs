using System.Collections;

namespace Bloom_Filter
{
    public class BloomFilter<T>
    {
#if DEBUG
        public BitArray getbitArray() => bitArray;
        public List<Func<T, int>> getHashFunctions() => hashFunctions;
        public int getCount() => count;
#endif
        /// <summary>
        /// Массив битов, размер m — количество битов в фильтре 
        /// (определяет точность; больше m — меньше ошибок).
        /// </summary>
        private BitArray bitArray { get; set; }

        /// <summary>
        /// Коллекция хеш-функций, k штук — количество хешей для элемента
        /// (балансирует ошибки; типично 3-10).
        /// </summary>
        private List<Func<T, int>> hashFunctions { get; set; }

        /// <summary>
        /// Целое число, текущее количество добавленных элементов
        /// (для отслеживания загрузки и вероятности ошибок).
        /// </summary>
        private int count { get; set; }

        /// <summary>
        /// Инициализирует фильтр; 
        /// вычисляет m (размер bitArray) 
        /// и k (количество хешей) 
        /// по формулам(m ≈ -capacity* ln(p) / (ln(2)^2), k ≈ -ln(p) / ln(2)),
        /// создаёт bitArray размером m, 
        /// генерирует k независимых хеш-функций (например, на базе GetHashCode с разными семенами).
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="falsePositiveProbability"></param>
        public BloomFilter(int capacity, double falsePositiveProbability)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
            if (falsePositiveProbability <= 0 || falsePositiveProbability > 1)
            {
                throw new ArgumentException(null, nameof(falsePositiveProbability));
            }

            var m = CalculateM(capacity, falsePositiveProbability);
            var k = CalculateK(falsePositiveProbability);


            bitArray = new BitArray((int)m);
            hashFunctions = [];
            var random = new Random();
            for (int i = 0; i < (int)k; i++)
            {
                int seed = random.Next();
                hashFunctions.Add(item => (item.GetHashCode() ^ seed) % (int)m);
            }
            count = 0;
        }

        /// <summary>
        /// Добавляет элемент: 
        /// вычисляет k хешей(индексы = hash % m), 
        /// устанавливает bitArray[index] = true для каждого; увеличивает count.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            for (int i = 0; i < hashFunctions.Count; i++)
            {
                var index = Math.Abs(hashFunctions[i](item) % bitArray.Length);
                bitArray.Set(index, true);
            }
            count++;
        }

        /// <summary>
        /// Проверяет возможное наличие: 
        /// вычисляет k хешей, проверяет все bitArray[index] == true; 
        /// возвращает true если все биты set(может быть ложным), 
        /// false если хоть один unset(элемент точно не добавлен).
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool MightContain(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            for (int i = 0; i < hashFunctions.Count; i++)
            {
                var index = Math.Abs(hashFunctions[i](item) % bitArray.Length);
                if (!bitArray.Get(index))
                {
                    return false; // точно нет элемента
                }
            }
            return true; // возможно есть (может быть ложное срабатывание)
        }

        /// <summary>
        /// Вычисляет текущую вероятность ошибки по формуле(1 - e^(-k* count / m))^k.
        /// </summary>
        public double EstimatedFalsePositiveProbability()
            => (Math.Pow(1 - Math.Exp(-hashFunctions.Count * count / (double)bitArray.Length), hashFunctions.Count));

        /// <summary>
        /// Сбрасывает bitArray в false, count в 0.
        /// </summary>
        public void Clear()
        {
            bitArray.SetAll(false);
            count = 0;
        }

        /// <summary>
        /// Вычисляет M (размер bitArray) по формуле m ≈ -capacity* ln(p) / (ln(2)^2), защищая от нуля 
        /// (если m получилось 0, возвращаем 1).
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="falsePositiveProbability"></param>
        /// <returns></returns>
        private static double CalculateM(int capacity, double falsePositiveProbability)
        {
            var m = Math.Ceiling(-capacity * Math.Log(falsePositiveProbability)
                                            / (Math.Pow(Math.Log(2), 2)));
            return m == 0 ? 1 : m;
        }

        /// <summary>
        /// Вычисляет M (размер bitArray) по формуле m ≈ -capacity* ln(p) / (ln(2)^2), 
        /// защищая от нуля (если m получилось 0, возвращаем 1).
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="falsePositiveProbability"></param>
        /// <returns></returns>
        private static double CalculateK(double falsePositiveProbability)
        {
            var k = Math.Ceiling(-Math.Log(falsePositiveProbability) / Math.Log(2));
            return k == 0 ? 1 : k;
        }
    }
}

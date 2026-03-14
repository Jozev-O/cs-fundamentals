namespace Bloom_Filter.Tests
{
    public class BloomFilterTests
    {
        // Тест конструктора для вычисления правильных m и k
        [Fact]
        public void Constructor_Calculates_Correct_M_And_K()
        {
            var capacity = 1000;
            var probability = 0.01;
            var bloomFilter = new BloomFilter<string>(capacity, probability);

            var expectedM = bloomFilter.getbitArray().Length;
            var expectedK = bloomFilter.getHashFunctions().Count;

            Assert.Equal(
                (int)Math.Ceiling(-capacity * Math.Log(probability) / Math.Pow(Math.Log(2), 2)),
                expectedM);

            Assert.Equal(
                (int)Math.Ceiling(Math.Log(2) * expectedM / capacity),
                expectedK);
        }

        // Add + MightContain
        [Fact]
        public void MightContain_For_Added_Element_Returns_True()
        {
            var bloomFilter = new BloomFilter<string>(1000, 0.01);
            var item = "test";

            bloomFilter.Add(item);

            Assert.True(bloomFilter.MightContain(item));
        }

        // MightContain для не добавленного элемента
        [Fact]
        public void MightContain_For_NonAdded_Element_Returns_False()
        {
            var bloomFilter = new BloomFilter<string>(1000, 0.01);

            Assert.False(bloomFilter.MightContain("test"));
        }

        // Clear
        [Fact]
        public void Clear_Resets_BloomFilter()
        {
            var bloomFilter = new BloomFilter<string>(1000, 0.01);
            var item = "test";

            bloomFilter.Add(item);
            bloomFilter.Clear();

            Assert.False(bloomFilter.MightContain(item));
        }

        // EstimatedFalsePositiveProbability
        [Fact]
        public void Estimated_FalsePositiveProbability_Returns_Correct_Value()
        {
            var capacity = 1000;
            var probability = 0.01;
            var bloomFilter = new BloomFilter<string>(capacity, probability);

            // Заполним до полной ёмкости
            for (int i = 0; i < capacity; i++)
                bloomFilter.Add(i.ToString());

            var estimatedProbability = bloomFilter.EstimatedFalsePositiveProbability();

            // Проверяем, что оценка близка к целевой вероятности, допустим ±0.002
            Assert.InRange(estimatedProbability, probability - 0.002, probability + 0.002);
        }

        // Проверка ложных срабатываний
        [Fact]
        public void FalsePositiveRate_Is_Within_Expected_Limits()
        {
            var capacity = 1000;
            var probability = 0.01;
            var bloomFilter = new BloomFilter<string>(capacity, probability);

            for (int i = 0; i < (int)(capacity * 0.8); i++)
                bloomFilter.Add(i.ToString());

            int falsePositives = 0;
            Random rand = new Random();

            for (int i = 0; i < 1000; i++)
            {
                var randomValue = rand.Next(20000, 30000).ToString(); // гарантированно новые

                if (bloomFilter.MightContain(randomValue))
                    falsePositives++;
            }

            var rate = falsePositives / 1000.0;

            Assert.True(rate <= probability + 0.005);
        }

        // capacity ≤ 0
        [Fact]
        public void Constructor_With_NonPositive_Capacity_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BloomFilter<string>(0, 0.01));
            Assert.Throws<ArgumentOutOfRangeException>(() => new BloomFilter<string>(-1, 0.01));
        }

        // probability ≤ 0 или > 1
        [Fact]
        public void Constructor_With_Invalid_Probability_Throws()
        {
            Assert.Throws<ArgumentException>(() => new BloomFilter<string>(1000, 0));
            Assert.Throws<ArgumentException>(() => new BloomFilter<string>(1000, 1.1));
        }

        // Add(null)
        [Fact]
        public void Add_With_Null_Throws_ArgumentNullException()
        {
            var bloomFilter = new BloomFilter<string>(1000, 0.01);

            Assert.Throws<ArgumentNullException>(() => bloomFilter.Add(null));
        }

        // MightContain(null)
        [Fact]
        public void MightContain_With_Null_Throws_ArgumentNullException()
        {
            var bloomFilter = new BloomFilter<string>(1000, 0.01);

            Assert.Throws<ArgumentNullException>(() => bloomFilter.MightContain(null));
        }
    }
}
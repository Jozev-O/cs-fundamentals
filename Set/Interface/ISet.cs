namespace Set.Interface
{
    public interface ISet<T>
    {
        /// <summary>
        /// Добавляет элемент, если его нет.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> Возвращает true если добавлен.</returns>
        bool Add(T item);

        /// <summary>
        /// Удаляет элемент, если есть.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Возвращает true если удалён.</returns>
        bool Remove(T item);

        /// <summary>
        /// Проверяет наличие элемента.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(T item);

        /// <summary>
        /// Возвращает количество элементов.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Очищает коллекцию.
        /// </summary>
        void Clear();
    }
}
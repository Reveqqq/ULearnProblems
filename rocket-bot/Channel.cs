using System.Collections.Generic;
using System.Linq;

namespace rocket_bot;

public class Channel<T> where T : class
{
    List<T> array = new();
    /// <summary>
    /// Возвращает элемент по индексу или null, если такого элемента нет.
    /// При присвоении удаляет все элементы после.
    /// Если индекс в точности равен размеру коллекции, работает как Append.
    /// </summary>
    public T this[int index]
    {
        get
        {
            lock (array)
            {
                if (index >= Count)
                    return null;
                return array[index];
            }
        }
        set
        {
            lock (array)
            {
                if (index == Count)
                    array.Add(value);
                else
                {
                    array[index] = value;
                    array.RemoveRange(index + 1, Count - (index + 1));
                }
            }
        }
    }

    /// <summary>
    /// Возвращает последний элемент или null, если такого элемента нет
    /// </summary>
    public T LastItem()
    {
        lock (array)
        {
            if (Count > 0)
                return array.Last();
            return null;
        }
    }

    /// <summary>
    /// Добавляет item в конец только если lastItem является последним элементом
    /// </summary>
    public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
    {
        lock (array)
        {
            if (knownLastItem == LastItem())
                array.Add(item);
        }
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции
    /// </summary>
    public int Count
    {
        get
        {
            lock (array)
                return array.Count;
        }
    }
}
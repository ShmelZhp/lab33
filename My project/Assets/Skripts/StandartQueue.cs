using System;
using System.Collections.Generic;

public class StandardQueue<T>
{
    private Queue<T> queue;

    public StandardQueue()
    {
        queue = new Queue<T>();
    }

    // Вставка элемента
    public void Enqueue(T item)
    {
        queue.Enqueue(item);
    }

    // Удаление элемента
    public T Dequeue()
    {
        if (queue.Count == 0)
            throw new InvalidOperationException("Очередь пуста");

        return queue.Dequeue();
    }

    // Проверка на пустоту
    public bool IsEmpty()
    {
        return queue.Count == 0;
    }

    // Просмотр первого элемента
    public T Peek()
    {
        if (queue.Count == 0)
            throw new InvalidOperationException("Очередь пуста");

        return queue.Peek();
    }

    // Печать очереди
    public void Print()
    {
        if (queue.Count == 0)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        Console.Write("Очередь: ");
        foreach (T item in queue)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // Количество элементов
    public int Count()
    {
        return queue.Count;
    }
}

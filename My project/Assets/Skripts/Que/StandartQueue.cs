using System;
using System.Collections.Generic;

public class StandardQueue<T>
{
    private Queue<T> queue;

    public StandardQueue()
    {
        queue = new Queue<T>();
    }

    // Âñòàâêà ýëåìåíòà
    public void Enqueue(T item)
    {
        queue.Enqueue(item);
    }

    // Óäàëåíèå ýëåìåíòà
    public T Dequeue()
    {
        if (queue.Count == 0)
            throw new InvalidOperationException("Î÷åðåäü ïóñòà");

        return queue.Dequeue();
    }

    // Ïðîâåðêà íà ïóñòîòó
    public bool IsEmpty()
    {
        return queue.Count == 0;
    }

    // Ïðîñìîòð ïåðâîãî ýëåìåíòà
    public T Peek()
    {
        if (queue.Count == 0)
            throw new InvalidOperationException("Î÷åðåäü ïóñòà");

        return queue.Peek();
    }

    // Ïå÷àòü î÷åðåäè
    public void Print()
    {
        if (queue.Count == 0)
        {
            Console.WriteLine("Î÷åðåäü ïóñòà");
            return;
        }

        Console.Write("Î÷åðåäü: ");
        foreach (T item in queue)
        {
            Console.Write(item + " ");
        }
        
    }

    // Êîëè÷åñòâî ýëåìåíòîâ
    public int Count()
    {
        return queue.Count;
    }
}
using lab3;
using System;
using System.Collections.Generic;

public class ListQueue<T>
{
    private Node<T> front;  // указатель на первый элемент
    private Node<T> rear;   // указатель на последний элемент
    private int count;      // количество элементов

    public ListQueue()
    {
        front = null;
        rear = null;
        count = 0;
    }

    // Вставка элемента (enqueue)
    public void Enqueue(T item)
    {
        Node<T> newNode = new Node<T>(item);

        if (rear == null)
        {
            // очередь пуста
            front = newNode;
            rear = newNode;
        }
        else
        {
            rear.Next = newNode;
            rear = newNode;
        }

        count++;
    }

    // Удаление элемента (dequeue)
    public T Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста");

        T data = front.Data;
        front = front.Next;

        if (front == null)
            rear = null;

        count--;
        return data;
    }

    // Проверка на пустоту
    public bool IsEmpty()
    {
        return front == null;
    }

    // Вывод первого элемента (peek)
    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста");

        return front.Data;
    }

    // Печать всей очереди
    
    // Количество элементов
    public int Count()
    {
        return count;
    }
    public List<T> ToList()
    {
        List<T> items = new List<T>();
        Node<T> current = front;
        while (current != null)
        {
            items.Add(current.Data);
            current = current.Next;
        }
        return items;
    }
    public List<string> Print()
    {
        List<string> elements = new List<string>();

        if (IsEmpty())
        {
            elements.Add("Очередь пуста");
            return elements;
        }

        Node<T> current = front;
        while (current != null)
        {
            elements.Add(current.Data.ToString());
            current = current.Next;
        }

        return elements;
    }
}
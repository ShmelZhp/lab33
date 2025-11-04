using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab3
{
    public static class LinkedListOperations
    {
        // 1. Функция, которая переворачивает список L
        public static void Reverse<T>(LinkedList<T> list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            Node<T> previous = null;
            Node<T> current = GetHeadNode(list);
            Node<T> next = null;

            while (current != null)
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            SetHeadNode(list, previous);
        }

        // 2. Функция, которая переносит в начало непустого списка L его последний элемент
        public static void MoveLastToFirst<T>(LinkedList<T> list)
        {
            if (list == null || list.Count == 0) return;

            if (list.Count == 1) return; // Один элемент - ничего не меняется

            Node<T> current = GetHeadNode(list);
            Node<T> previous = null;

            // Находим последний элемент и предпоследний
            while (current.Next != null)
            {
                previous = current;
                current = current.Next;
            }

            // Переносим последний элемент в начало
            previous.Next = null;
            current.Next = GetHeadNode(list);
            SetHeadNode(list, current);
        }

        // 2. Функция, которая переносит в конец непустого списка L его первый элемент
        public static void MoveFirstToLast<T>(LinkedList<T> list)
        {
            if (list == null || list.Count == 0 || list.Count == 1) return;

            Node<T> first = GetHeadNode(list);
            Node<T> current = first;

            // Находим последний элемент
            while (current.Next != null)
            {
                current = current.Next;
            }

            // Переносим первый элемент в конец
            SetHeadNode(list, first.Next);
            first.Next = null;
            current.Next = first;
        }

        // 3. Функция, которая определяет количество различных элементов списка, содержащего целые числа
        public static int CountDistinct(LinkedList<int> list)
        {
            if (list == null || list.Count == 0) return 0;

            HashSet<int> distinctElements = new HashSet<int>();
            Node<int> current = GetHeadNode(list);

            while (current != null)
            {
                distinctElements.Add(current.Data);
                current = current.Next;
            }

            return distinctElements.Count;
        }

        // 4. Функция, которая удаляет из списка L неуникальные элементы
        public static void RemoveNonUnique<T>(LinkedList<T> list) where T : IComparable
        {
            if (list == null || list.Count == 0) return;

            Dictionary<T, int> frequency = new Dictionary<T, int>();
            Node<T> current = GetHeadNode(list);

            // Подсчитываем частоту элементов
            while (current != null)
            {
                if (frequency.ContainsKey(current.Data))
                    frequency[current.Data]++;
                else
                    frequency[current.Data] = 1;
                current = current.Next;
            }

            // Удаляем элементы с частотой > 1
            Node<T> dummy = new Node<T>(default(T));
            dummy.Next = GetHeadNode(list);
            Node<T> prev = dummy;
            current = GetHeadNode(list);

            while (current != null)
            {
                if (frequency[current.Data] > 1)
                {
                    prev.Next = current.Next;
                }
                else
                {
                    prev = current;
                }
                current = current.Next;
            }

            SetHeadNode(list, dummy.Next);
        }

        // 5. Функция вставки списка самого в себя вслед за первым вхождением числа х
        public static void InsertListAfterFirstOccurrence<T>(LinkedList<T> list, T x) where T : IComparable
        {
            if (list == null) return;

            Node<T> current = GetHeadNode(list);

            // Ищем первое вхождение x
            while (current != null)
            {
                if (current.Data.CompareTo(x) == 0)
                {
                    // Создаем копию оставшейся части списка
                    Node<T> copy = CopyList(current.Next);

                    // Вставляем копию после текущего элемента
                    Node<T> temp = current.Next;
                    current.Next = copy;

                    // Соединяем конец копии с оригинальным продолжением
                    Node<T> copyEnd = copy;
                    while (copyEnd.Next != null)
                    {
                        copyEnd = copyEnd.Next;
                    }
                    copyEnd.Next = temp;

                    return;
                }
                current = current.Next;
            }
        }

        // 6. Функция, которая вставляет в непустой упорядоченный список новый элемент Е
        public static void InsertInOrderedList<T>(LinkedList<T> list, T element) where T : IComparable
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            Node<T> newNode = new Node<T>(element);

            // Если список пустой или новый элемент должен быть первым
            if (list.Count == 0 || GetHeadNode(list).Data.CompareTo(element) >= 0)
            {
                newNode.Next = GetHeadNode(list);
                SetHeadNode(list, newNode);
                return;
            }

            Node<T> current = GetHeadNode(list);

            // Ищем позицию для вставки
            while (current.Next != null && current.Next.Data.CompareTo(element) < 0)
            {
                current = current.Next;
            }

            // Вставляем новый элемент
            newNode.Next = current.Next;
            current.Next = newNode;
        }

        // 7. Функция, которая удаляет из списка L все элементы Е
        public static void RemoveAllOccurrences<T>(LinkedList<T> list, T element) where T : IComparable
        {
            if (list == null || list.Count == 0) return;

            Node<T> dummy = new Node<T>(default(T));
            dummy.Next = GetHeadNode(list);
            Node<T> prev = dummy;
            Node<T> current = GetHeadNode(list);

            while (current != null)
            {
                if (current.Data.CompareTo(element) == 0)
                {
                    prev.Next = current.Next;
                }
                else
                {
                    prev = current;
                }
                current = current.Next;
            }

            SetHeadNode(list, dummy.Next);
        }

        // 8. Функция, которая вставляет в список L новый элемент F перед первым вхождением элемента Е
        public static void InsertBeforeFirstOccurrence<T>(LinkedList<T> list, T newElement, T targetElement) where T : IComparable
        {
            if (list == null) return;

            // Если список пустой или targetElement в начале
            if (list.Count == 0 || GetHeadNode(list).Data.CompareTo(targetElement) == 0)
            {
                list.AddFirst(newElement);
                return;
            }

            Node<T> current = GetHeadNode(list);

            while (current.Next != null)
            {
                if (current.Next.Data.CompareTo(targetElement) == 0)
                {
                    Node<T> newNode = new Node<T>(newElement);
                    newNode.Next = current.Next;
                    current.Next = newNode;
                    return;
                }
                current = current.Next;
            }
        }

        // 9. Функция дописывает к списку L список E (оба содержат целые числа)
        public static void AppendList(LinkedList<int> list, LinkedList<int> otherList)
        {
            if (list == null || otherList == null) return;

            if (list.Count == 0)
            {
                SetHeadNode(list, GetHeadNode(otherList));
                return;
            }

            Node<int> current = GetHeadNode(list);

            // Находим последний элемент первого списка
            while (current.Next != null)
            {
                current = current.Next;
            }

            // Копируем второй список и присоединяем к первому
            current.Next = CopyList(GetHeadNode(otherList));
        }

        // 10. Функция разбивает список целых чисел на два списка по первому вхождению заданного числа
        public static (LinkedList<int>, LinkedList<int>) SplitByFirstOccurrence(LinkedList<int> list, int number)
        {
            if (list == null) return (new LinkedList<int>(), new LinkedList<int>());

            LinkedList<int> firstPart = new LinkedList<int>();
            LinkedList<int> secondPart = new LinkedList<int>();

            Node<int> current = GetHeadNode(list);
            bool found = false;

            while (current != null)
            {
                if (!found && current.Data == number)
                {
                    found = true;
                    current = current.Next;
                    continue;
                }

                if (!found)
                {
                    firstPart.AddLast(current.Data);
                }
                else
                {
                    secondPart.AddLast(current.Data);
                }

                current = current.Next;
            }

            return (firstPart, secondPart);
        }

        // 11. Функция удваивает список (приписывает в конец списка себя самого)
        public static void DoubleList<T>(LinkedList<T> list)
        {
            if (list == null || list.Count == 0) return;

            // Копируем весь список
            Node<T> copy = CopyList(GetHeadNode(list));

            // Находим конец оригинального списка
            Node<T> current = GetHeadNode(list);
            while (current.Next != null)
            {
                current = current.Next;
            }

            // Присоединяем копию к концу
            current.Next = copy;
        }

        // 12. Функция меняет местами два элемента списка, заданные пользователем
        public static void SwapElements<T>(LinkedList<T> list, T first, T second) where T : IComparable
        {
            if (list == null || list.Count < 2) return;

            // Если элементы одинаковые, ничего не делаем
            if (first.CompareTo(second) == 0) return;

            Node<T> firstNode = null;
            Node<T> secondNode = null;
            Node<T> current = GetHeadNode(list);

            // Ищем узлы с заданными значениями
            while (current != null)
            {
                if (current.Data.CompareTo(first) == 0)
                    firstNode = current;
                else if (current.Data.CompareTo(second) == 0)
                    secondNode = current;

                if (firstNode != null && secondNode != null)
                    break;

                current = current.Next;
            }

            // Если оба элемента найдены, меняем их данные
            if (firstNode != null && secondNode != null)
            {
                T temp = firstNode.Data;
                firstNode.Data = secondNode.Data;
                secondNode.Data = temp;
            }
        }

        // Вспомогательные методы для доступа к приватным полям через Reflection
        private static Node<T> GetHeadNode<T>(LinkedList<T> list)
        {
            var field = typeof(LinkedList<T>).GetField("head", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (Node<T>)field.GetValue(list);
        }

        private static void SetHeadNode<T>(LinkedList<T> list, Node<T> newHead)
        {
            var field = typeof(LinkedList<T>).GetField("head", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field.SetValue(list, newHead);

            // Обновляем счетчик
            var countField = typeof(LinkedList<T>).GetField("count", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            int count = 0;
            Node<T> current = newHead;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            countField.SetValue(list, count);
        }

        // Вспомогательный метод для копирования списка
        private static Node<T> CopyList<T>(Node<T> head)
        {
            if (head == null) return null;

            Node<T> newHead = new Node<T>(head.Data);
            Node<T> currentOriginal = head.Next;
            Node<T> currentCopy = newHead;

            while (currentOriginal != null)
            {
                currentCopy.Next = new Node<T>(currentOriginal.Data);
                currentCopy = currentCopy.Next;
                currentOriginal = currentOriginal.Next;
            }

            return newHead;
        }

        // Метод для чтения списка из файла
        public static LinkedList<int> ReadListFromFile(string filePath)
        {
            LinkedList<int> list = new LinkedList<int>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int number))
                    {
                        list.AddLast(number);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }

            return list;
        }
    }
}
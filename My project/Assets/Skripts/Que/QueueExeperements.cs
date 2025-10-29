using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;
using lab3;

public class QueueExperements : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _valueSlider; // Общее количество операций (например, 100)
    [SerializeField] private UnityEngine.UI.Slider _stepSlider;  // Шаг (например, 10)
    [SerializeField] private UnityEngine.UI.Slider _valueSliderStandart; // Общее количество операций (например, 100)
    [SerializeField] private UnityEngine.UI.Slider _stepSliderStandart;

    public List<Vector2Int> MakeExperement()
    {
        // Генерируем данные (списки команд)
        var data = GenerateRandomData((int)_valueSlider.value, (int)_stepSlider.value);
        var results = new List<Vector2Int>();

        // Для каждого набора команд выполняем измерение времени
        foreach (var commands in data)
        {
            ListQueue<string> queue = new ListQueue<string>();
            int time = ParseData(commands, queue);
            results.Add(new Vector2Int(commands.Count, time));
        }

        return results;
    }

    public List<Vector2Int> MakeQueueOnlyExperementListQue()
    {
        int maxValue = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;
        var results = new List<Vector2Int>();

        for (int count = step; count <= maxValue; count += step)
        {
            ListQueue<string> queue = new ListQueue<string>();
            List<int> commands = GenerateRandomCommandCodes(count); // 1=Enqueue,2=Dequeue,3=Peek,4=IsEmpty

            Stopwatch sw = Stopwatch.StartNew();

            foreach (int cmd in commands)
            {
                switch (cmd)
                {
                    case 1: queue.Enqueue("x"); break;
                    case 2: if (!queue.IsEmpty()) queue.Dequeue(); break;
                    case 3: if (!queue.IsEmpty()) queue.Peek(); break;
                    case 4: queue.IsEmpty(); break;
                }
            }

            sw.Stop();
            results.Add(new Vector2Int(count, (int)(sw.Elapsed.TotalMilliseconds * 1000))); // время в мкс
        }

        return results;
    }
    public List<Vector2Int> MakeQueueOnlyExperementStandartQue(bool isPrint)
    {
        int maxValue = (int)_valueSliderStandart.value;
        int step = (int)_stepSliderStandart.value;
        var results = new List<Vector2Int>();

        for (int count = step; count <= maxValue; count += step)
        {
            StandardQueue<string> queue = new StandardQueue<string>();
            List<int> commands = GenerateRandomCommandCodes(count); // 1=Enqueue,2=Dequeue,3=Peek,4=IsEmpty

            Stopwatch sw = Stopwatch.StartNew();

            foreach (int cmd in commands)
            {
                switch (cmd)
                {
                    case 1: queue.Enqueue("x"); break;
                    case 2: if (!queue.IsEmpty()) queue.Dequeue(); break;
                    case 3: if (!queue.IsEmpty()) queue.Peek(); break;
                    case 4: queue.IsEmpty(); break;
                    case 5: if (isPrint) queue.Print(); break;
                }
            }

            sw.Stop();
            results.Add(new Vector2Int(count, (int)(sw.Elapsed.TotalMilliseconds * 1000))); // время в мкс
        }

        return results;
    }

    public List<Vector2Int> MakeQueueOnlyExperementListQue(bool isPrint)
    {
        int maxValue = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;
        var results = new List<Vector2Int>();

        for (int count = step; count <= maxValue; count += step)
        {
            ListQueue<string> queue = new ListQueue<string>();
            List<int> commands = GenerateRandomCommandCodes(count);

            Stopwatch sw = Stopwatch.StartNew();

            foreach (int cmd in commands)
            {
                switch (cmd)
                {
                    case 1: queue.Enqueue("x"); break;
                    case 2: if (!queue.IsEmpty()) queue.Dequeue(); break;
                    case 3: if (!queue.IsEmpty()) queue.Peek(); break;
                    case 4: queue.IsEmpty(); break;
                    case 5: if (isPrint) queue.Print(); break;
                }
            }

            sw.Stop();
            results.Add(new Vector2Int(count, (int)(sw.Elapsed.TotalMilliseconds * 1000)));
        }

        return results;
    }

    public List<Vector2Int> MakeEnqueuePrintExperementsList(bool isPrint)
    {
        int maxValue = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;
        var results = new List<Vector2Int>();

        for (int count = step; count <= maxValue; count += step)
        {
            ListQueue<string> queue = new ListQueue<string>();
            List<int> commands = GenerateLinearDataCodes(count); // 1 и 5 поочередно

            Stopwatch sw = Stopwatch.StartNew();

            foreach (int cmd in commands)
            {
                switch (cmd)
                {
                    case 1: queue.Enqueue("x"); break;
                    case 5: if (isPrint) queue.Print(); break;
                }
            }

            sw.Stop();
            results.Add(new Vector2Int(count, (int)(sw.Elapsed.TotalMilliseconds * 1000)));
        }

        return results;
    }

    public List<Vector2Int> MakeEnqueuePrintExperementsStandart(bool isPrint)
    {
        int maxValue = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;
        var results = new List<Vector2Int>();

        for (int count = step; count <= maxValue; count += step)
        {
            StandardQueue<string> queue = new StandardQueue<string>();
            List<int> commands = GenerateLinearDataCodes(count); // 1 и 5 поочередно

            Stopwatch sw = Stopwatch.StartNew();

            foreach (int cmd in commands)
            {
                switch (cmd)
                {
                    case 1: queue.Enqueue("x"); break;
                    case 5: if (isPrint) queue.Print(); break;
                }
            }

            sw.Stop();
            results.Add(new Vector2Int(count, (int)(sw.Elapsed.TotalMilliseconds * 1000)));
        }

        return results;
    }
    private int ParseData(List<string> commands, ListQueue<string> queue)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (string operation in commands)
        {
            string op = operation.Trim();

            if (op.Contains(','))
            {
                string[] parts = op.Split(',');
                queue.Enqueue(parts[1]);
            }
            else
            {
                int opCode = int.Parse(op);
                switch (opCode)
                {
                    case 2:
                        if (!queue.IsEmpty()) queue.Dequeue();
                        else { queue.Enqueue("temp"); queue.Dequeue(); }
                        break;
                    case 3:
                        if (!queue.IsEmpty()) queue.Peek();
                        break;
                    case 4:
                        queue.IsEmpty();
                        break;
                    case 5:
                        queue.Print();
                        break;
                }
            }
        }

        stopwatch.Stop();
        int microseconds = (int)Math.Floor(stopwatch.Elapsed.TotalMilliseconds * 100);
        return microseconds;
    }

    private List<int> GenerateRandomCommandCodes(int count)
    {
        var list = new List<int>();
        for (int i = 0; i < count; i++)
            list.Add(Random.Range(1, 6)); // от 1 до 5
        return list;
    }

    private List<int> GenerateLinearDataCodes(int count)
    {
        var list = new List<int>();
        for (int i = 0; i < count; i++)
        {
            if (i % 2 == 0)
                list.Add(1); // Enqueue
            else
                list.Add(5); // Print
        }
        return list;
    }

    private List<List<string>> GenerateRandomData(int maxValue, int step)
    {
        var data = new List<List<string>>();
        for (int count = step; count <= maxValue; count += step)
        {
            List<string> temp = new List<string>();
            for (int j = 0; j < count; j++)
                temp.Add(GenerateRandomCommand());
            data.Add(temp);
        }
        return data;
    }

    private string GenerateRandomCommand()
    {
        int random = Random.Range(1, 6);
        return random switch
        {
            1 => "1,cat",
            2 => "2",
            3 => "3",
            4 => "4",
            5 => "5",
            _ => "4"
        };
    }
}

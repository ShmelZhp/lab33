using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;


public class QueueExperements : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Slider _valueSlider;  // Общее количество операций (например, 10)
    [SerializeField] private UnityEngine.UI.Slider _stepSlider;  // Шаг (например, 10)

    public (List<Vector2Int>, List<Vector2Int>) DiffLengthsExperiment()
    {
        // Эксперимент: различная длина операций
        int maxLength = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;

        var data = GenerateRandomData(maxLength, step);
        var resultsList = new List<Vector2Int>();
        var resultsStandard = new List<Vector2Int>();

        foreach (var commands in data)
        {
            ListQueue<string> listQueue = new ListQueue<string>();
            StandardQueue<string> standardQueue = new StandardQueue<string>();

            int timeList = ParseListData(commands, listQueue);
            int timeStandard = ParseStandartData(commands, standardQueue);

            int commandCount = commands.Count;
            resultsList.Add(new Vector2Int(commandCount, timeList));
            resultsStandard.Add(new Vector2Int(commandCount, timeStandard));
        }

        return (resultsList, resultsStandard);

    }

    public (List<Vector2Int>, List<Vector2Int>) DiffOps()
    {
        // Эксперимент: одинаковая длина, разный состав операций
        int fixedLength = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;

        var dataVaried = GenerateRandomData(fixedLength, step);
        var resultsList = new List<Vector2Int>();
        var resultsStandard = new List<Vector2Int>();

        foreach (var commands in dataVaried)
        {
            ListQueue<string> listQueue = new ListQueue<string>();
            StandardQueue<string> standardQueue = new StandardQueue<string>();

            int timeList = ParseListData(commands, listQueue);
            int timeStandard = ParseStandartData(commands, standardQueue);

            int opCount = commands.Count;
            resultsList.Add(new Vector2Int(opCount, timeList));
            resultsStandard.Add(new Vector2Int(opCount, timeStandard));
        }

		return (resultsList, resultsStandard);
	}

    public List<Vector2Int> MakeListExperement(int length)
    {
        // Генерируем списки команд разной длины
        var data = GenerateRandomData(length, (int)_stepSlider.value);
        var results = new List<Vector2Int>();

        // Для каждого набора команд замеряем время выполнения
        for (int i = 0; i < data.Count; i++)
        {
            ListQueue<string> queue = new ListQueue<string>();

            int time = ParseListData(data[i], queue);
            int commandCount = data[i].Count;

            results.Add(new Vector2Int(commandCount, time));
        }

        return results;
    }
    public List<Vector2Int> MakeStandartExperement(int length)
    {
        // Генерируем списки команд разной длины
        var data = GenerateRandomData(length, (int)_stepSlider.value);
        var results = new List<Vector2Int>();

        // Для каждого набора команд замеряем время выполнения
        for (int i = 0; i < data.Count; i++)
        {
            StandardQueue<string> queue = new StandardQueue<string>();

            int time = ParseStandartData(data[i], queue);
            int commandCount = data[i].Count;

            results.Add(new Vector2Int(commandCount, time));
        }

        return results;
    }

    private string GenerateRandomCommand()
    {
        int random = Random.Range(1, 6);
        return random switch
        {
            1 => "1,cat ",
            2 => "2 ",
            3 => "3 ",
            4 => "4 ",
            5 => "2 ",
            _ => "4 "
        };
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

    private int ParseListData(List<string> commands, ListQueue<string> stack)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (string operation in commands)
        {
            string op = operation.Trim();
            if (op.Contains(','))
            {
                string[] pushParts = op.Split(',');
                stack.Enqueue(pushParts[1]);
            }
            else
            {
                int opCode = int.Parse(op);
                switch (opCode)
                {
                    case 2:
                        if (!stack.IsEmpty()) stack.Dequeue();
                        else { stack.Enqueue("temp"); stack.Dequeue(); }
                        break;
                    case 3:
                        stack.Dequeue();
                        break;
                    case 4:
                        stack.IsEmpty();
                        break;
                    case 5:
                        stack.Print();
                        break;
                }
            }
        }

        stopwatch.Stop();
        int microseconds = (int)Math.Floor(stopwatch.Elapsed.TotalMilliseconds * 100);
        return microseconds;
    }
    private int ParseStandartData(List<string> commands, StandardQueue<string> stack)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (string operation in commands)
        {
            string op = operation.Trim();
            if (op.Contains(','))
            {
                string[] pushParts = op.Split(',');
                stack.Enqueue(pushParts[1]);
            }
            else
            {
                int opCode = int.Parse(op);
                switch (opCode)
                {
                    case 2:
                        if (!stack.IsEmpty()) stack.Dequeue();
                        else { stack.Enqueue("temp"); stack.Dequeue(); }
                        break;
                    case 3:
                        stack.Dequeue();
                        break;
                    case 4:
                        stack.IsEmpty();
                        break;
                    case 5:
                        stack.Print();
                        break;
                }
            }
        }

        stopwatch.Stop();
        int microseconds = (int)Math.Floor(stopwatch.Elapsed.TotalMilliseconds * 100);
        return microseconds;
    }






}
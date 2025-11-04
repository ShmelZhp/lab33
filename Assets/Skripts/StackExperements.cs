using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackExperements : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _valueSlider; // Общее количество операций (например, 100)
    [SerializeField] private UnityEngine.UI.Slider _stepSlider;  // Шаг (например, 10)

    public List<Vector2Int> MakeExperement()
    {
        // Генерируем списки команд разной длины
        var data = GenerateRandomData((int)_valueSlider.value, (int)_stepSlider.value);
        var results = new List<Vector2Int>();

        // Для каждого набора команд замеряем время выполнения
        for (int i = 0; i < data.Count; i++)
        {
            lab3.Stack<string> stack = new lab3.Stack<string>();

            int time = ParseData(data[i], stack);
            int commandCount = data[i].Count;

            results.Add(new Vector2Int(commandCount, time));
        }

        return results;
    }
    public List<Vector2Int> MakeStackOnlyExperement()
    {
        int maxValue = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;

        var results = new List<Vector2Int>();

        for (int count = step; count <= maxValue; count += step)
        {
            lab3.Stack<string> stack = new lab3.Stack<string>();
            List<int> commands = GenerateRandomCommandCodes(count); // 1=Push,2=Pop,3=Top,4=IsEmpty

            Stopwatch sw = Stopwatch.StartNew();

            foreach (int cmd in commands)
            {
                switch (cmd)
                {
                    case 1: stack.Push("x"); break;
                    case 2: if (!stack.IsEmpty()) stack.Pop(); break;
                    case 3: stack.Top(); break;
                    case 4: stack.IsEmpty(); break;
                }
            }

            sw.Stop();
            results.Add(new Vector2Int(count, (int)(sw.Elapsed.TotalMilliseconds * 1000))); // время в мкс
        }

        return results;
    }
    public List<Vector2Int> MakeStackOnlyExperement(bool isPrint)
    {
        int maxValue = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;

        var results = new List<Vector2Int>();

        for (int count = step; count <= maxValue; count += step)
        {
            lab3.Stack<string> stack = new lab3.Stack<string>();
            List<int> commands = GenerateRandomCommandCodes(count); // 1=Push,2=Pop,3=Top,4=IsEmpty

            Stopwatch sw = Stopwatch.StartNew();

            foreach (int cmd in commands)
            {
                switch (cmd)
                {
                    case 1: stack.Push("x"); break;
                    case 2: if (!stack.IsEmpty()) stack.Pop(); break;
                    case 3: stack.Top(); break;
                    case 4: stack.IsEmpty(); break;
                    case 5: if(isPrint)stack.Print(); break;
                }
            }

            sw.Stop();
            results.Add(new Vector2Int(count, (int)(sw.Elapsed.TotalMilliseconds * 1000))); // время в мкс
        }

        return results;
    }
    public List<Vector2Int> MakePushPrintExperements(bool isPrint)
    {
        int maxValue = (int)_valueSlider.value;
        int step = (int)_stepSlider.value;

        var results = new List<Vector2Int>();

        for (int count = step; count <= maxValue; count += step)
        {
            lab3.Stack<string> stack = new lab3.Stack<string>();
            List<int> commands = GenerateLinearDataCodes(count); // 1=Push,2=Pop,3=Top,4=IsEmpty

            Stopwatch sw = Stopwatch.StartNew();

            foreach (int cmd in commands)
            {
                switch (cmd)
                {
                    case 1: stack.Push("x"); break;
                    case 2: if (!stack.IsEmpty()) stack.Pop(); break;
                    case 3: stack.Top(); break;
                    case 4: stack.IsEmpty(); break;
                    case 5: if(isPrint)stack.Print(); break;
                }
            }

            sw.Stop();
            results.Add(new Vector2Int(count, (int)(sw.Elapsed.TotalMilliseconds * 1000))); // время в мкс
        }

        return results;
    }
    private List<int> GenerateRandomCommandCodes(int count)
    {
        var list = new List<int>();
        for (int i = 0; i < count; i++)
            list.Add(Random.Range(1, 6)); // 1-4
        return list;
    }

    private List<int> GenerateLinearDataCodes(int count)
    {
        var list = new List<int>();
        for (int i = 0; i < count; i++)
        {
            if (i % 2 == 0)
            {
                list.Add(1);
                
            }
            else
            {
                list.Add(5);
            }
        }
        return list;
    }

    private int ParseData(List<string> commands, lab3.Stack<string> stack)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (string operation in commands)
        {
            string op = operation.Trim();
            if (op.Contains(','))
            {
                string[] pushParts = op.Split(',');
                stack.Push(pushParts[1]);
            }
            else
            {
                int opCode = int.Parse(op);
                switch (opCode)
                {
                    case 2:
                        if (!stack.IsEmpty()) stack.Pop();
                        else { stack.Push("temp"); stack.Pop(); }
                        break;
                    case 3:
                        stack.Top();
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
    private List<List<string>> GenerateLinearData(int maxValue, int step)
    {
        var data = new List<List<string>>();

        for (int count = step; count <= maxValue; count += step)
        {
            List<string> temp = new List<string>();
            for (int j = 0; j < count; j++)
                temp.Add(GenerateLinearCommand());

            data.Add(temp);
        }

        return data;
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
    private string GenerateLinearCommand()
    {
        return "1,cat 5";
    }
}

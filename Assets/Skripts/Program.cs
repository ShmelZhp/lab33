



/*
using UnityEngine.Windows;

namespace lab3
{



    public class Program
    {
        public static void Main(string[] args)
        {
            Stack<string> stack = new Stack<string>();
            string[] lines = File.ReadAllLines("input.txt");

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                foreach (string operation in parts)
                {
                    if (operation.Contains(','))
                    {
                        // Операция Push: "1,element"
                        string[] pushParts = operation.Split(',');
                        int opCode = int.Parse(pushParts[0]);
                        string element = pushParts[1];
                        stack.Push(element);
                        Console.WriteLine($"Push({element}) выполнен");
                    }
                    else
                    {
                        int opCode = int.Parse(operation);
                        switch (opCode)
                        {
                            case 2: // Pop
                                string popped = stack.Pop();
                                Console.WriteLine($"Pop() вернул: {popped}");
                                break;
                            case 3: // Top
                                string top = stack.Top();
                                Console.WriteLine($"Top() вернул: {top}");
                                break;
                            case 4: // isEmpty
                                bool empty = stack.IsEmpty();
                                Console.WriteLine($"isEmpty() вернул: {empty}");
                                break;
                            case 5: // Print
                                Console.Write("Print(): ");
                                stack.Print();
                                break;
                        }
                    }
                }
            }
        }
    }
}
*/

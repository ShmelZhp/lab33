using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace lab3
{
    public class queParser : MonoBehaviour
    {
        public TMP_InputField inputField;
        public Button autoButton;
        public Button nextButton;
        public TextMeshProUGUI outputText;
        public ScrollRect scrollRect;

        private List<string> operations = new List<string>();
        private int counter = 0;
        private bool parsed = false;

        private ListQueue<string> queue = new ListQueue<string>(); // постоянная очередь

        public void Init()
        {
            autoButton.onClick.AddListener(AutoButtonClick);
            nextButton.onClick.AddListener(NextButtonClick);
        }

        private void AutoButtonClick()
        {
            if (!parsed)
                ParseInput();

            DisplayOperationsAuto();
        }

        private void NextButtonClick()
        {
            if (!parsed)
                ParseInput();

            DisplayOperation(counter);
            counter++;
        }

        public void ParseInput()
        {
            if (operations.Count != 0)
                operations.Clear();

            string[] lines = inputField.text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (string operation in lines)
            {
                if (operation.Contains(",")) // enqueue
                {
                    string[] parts = operation.Split(',');
                    string element = parts[1];
                    operations.Add($"ENQUEUE {element}");
                }
                else
                {
                    operations.Add(operation);
                }
            }

            parsed = true;
            counter = 0;
        }

        private void DisplayOperationsAuto()
        {
            outputText.text = "";

            foreach (string op in operations)
            {
                ExecuteOperation(op);
            }
        }

        private void DisplayOperation(int index)
        {
            if (index == 0)
                outputText.text = "";

            if (index < operations.Count)
            {
                ExecuteOperation(operations[index]);
            }
            else
            {
                outputText.text += "Все операции выполнены\n";
            }
        }

        private void ExecuteOperation(string operation)
        {
            try
            {
                if (operation.StartsWith("ENQUEUE"))
                {
                    string element = operation.Split(' ')[1];
                    queue.Enqueue(element);
                    outputText.text += $"Enqueue({element}) выполнен\n";
                }
                else
                {
                    int code = int.Parse(operation);
                    switch (code)
                    {
                        case 2:
                            if (!queue.IsEmpty())
                            {
                                string dq = queue.Dequeue();
                                outputText.text += $"Dequeue() вернул: {dq}\n";
                            }
                            else
                            {
                                outputText.text += "Очередь пуста — Dequeue() невозможен\n";
                            }
                            break;

                        case 3:
                            if (!queue.IsEmpty())
                            {
                                string peek = queue.Peek();
                                outputText.text += $"Peek() вернул: {peek}\n";
                            }
                            else
                            {
                                outputText.text += "Очередь пуста — Peek() невозможен\n";
                            }
                            break;

                        case 4:
                            bool empty = queue.IsEmpty();
                            outputText.text += $"isEmpty() вернул: {empty}\n";
                            break;

                        case 5:
                            outputText.text += "Print():\n";
                            var tmp = queue.Print();
                            foreach (string element in tmp)
                                outputText.text += $"<{element}>\n";
                            break;

                        default:
                            outputText.text += $"Неизвестная операция: {operation}\n";
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                outputText.text += $"Ошибка: {e.Message}\n";
            }
        }
    }
}

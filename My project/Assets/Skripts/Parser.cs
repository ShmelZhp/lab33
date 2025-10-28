using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace lab3
{
    public class Parser : MonoBehaviour
    {
        public TMP_InputField inputField;
        public Button autoButton;
        public Button nextButton;
        private List<string> operations = new List<string>();
        private int currentOperation = 0;
        private int counter = 0;
        public TextMeshProUGUI outputText;
        public ScrollRect scrollRect;
        private bool isNext = false;
        private bool parseBool = false;

        
        public void Init()
        {
            autoButton.onClick.AddListener(AutoButtonClick);
            nextButton.onClick.AddListener(NextButtonClick);
        }

        

        private void AutoButtonClick()
        {
            
            ParseInput();
            DisplayOperationsAuto();
        }
        private void NextButtonClick()
        {
            ParseInput();
            DisplayOperation(counter);
            counter++;
        }
        public void ParseInput()
        {
           
                if (operations.Count != 0)
                {
                    operations.Clear();
                }

                Stack<string> stack = new Stack<string>();

                string[] lines = inputField.text.Split(' ');

                foreach (string operation in lines)
                {
                    if (operation.Contains(','))
                    {
                        // Операция Push: "1,element"
                        string[] pushParts = operation.Split(',');

                        string element = pushParts[1];
                        stack.Push(element);
                        operations.Add($"Push({element}) выполнен\n");

                    }
                    else
                    {
                        int opCode = int.Parse(operation);
                        switch (opCode)
                        {
                            case 2: // Pop
                                if (!stack.IsEmpty())
                                {
                                    string popped = stack.Pop();
                                    operations.Add($"Pop() вернул: {popped}\n");
                                }
                                else
                                {
                                    DisplayError("Stack Пуст и не может выполнить Pop()");
                                    return;
                                }

                                break;
                            case 3: // Top
                                string top = stack.Top();

                                operations.Add($"Top() вернул: {top}\n");
                                break;
                            case 4: // isEmpty
                                bool empty = stack.IsEmpty();

                                operations.Add($"isEmpty() вернул: {empty}\n");
                                break;
                            case 5: // Print
                                operations.Add($"Print():\n");
                                var tmp = stack.Print();
                                foreach (string element in tmp)
                                {
                                    operations.Add($"<{element}>\n");
                                }

                                break;
                        }
                    }

                }

                

        }
        

        private void DisplayOperationsAuto()
                {
                    // Очищаем предыдущий текст
                    outputText.text = "";
            
                    // Добавляем все операции построчно
                    foreach (string operation in operations)
                    {
                        outputText.text += operation;
                    }

                  
                    
                }

        private void DisplayOperation(int index)
        {
            if(index < operations.Count){
            outputText.text += operations[index];
            }
            else
            {
                outputText.text += "Все операции выполнены";
            }
        }

        private void DisplayError(string message)
        {
            outputText.text = message;
        }
                
        
        }
    }

using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace lab3
{
    public class PostfixCalculator : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;   // поле ввода постфиксного выражения
    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private Button _starfButton;// поле для вывода результата

    public void Init()
    {
        _starfButton.onClick.AddListener(OnCalculateClick);
    }
    public void OnCalculateClick()
    {
        try
        {
            string expression = inputField.text;
            double result = EvaluatePostfix(expression);
            outputText.text = $"Результат: {result}";
        }
        catch (Exception ex)
        {
            outputText.text = $"Ошибка: {ex.Message}";
        }
    }
    

    private double EvaluatePostfix(string expression)
    {
        Stack<double> stack = new Stack<double>();
        string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (string token in tokens)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
            {
                // Число → кладём в стек
                stack.Push(number);
            }
            else
            {
                switch (token)
                {
                    case "+":
                        stack.Push(stack.Pop() + stack.Pop());
                        break;

                    case "-":
                        {
                            double b = stack.Pop();
                            double a = stack.Pop();
                            stack.Push(a - b);
                        }
                        break;

                    case "*":
                        stack.Push(stack.Pop() * stack.Pop());
                        break;

                    case ":":
                    case "/":
                        {
                            double b = stack.Pop();
                            double a = stack.Pop();
                            stack.Push(a / b);
                        }
                        break;

                    case "^":
                        {
                            double b = stack.Pop();
                            double a = stack.Pop();
                            stack.Push(Math.Pow(a, b));
                        }
                        break;

                    case "sin":
                        stack.Push(Math.Sin(stack.Pop()));
                        break;

                    case "cos":
                        stack.Push(Math.Cos(stack.Pop()));
                        break;

                    case "ln":
                        stack.Push(Math.Log(stack.Pop()));
                        break;

                    case "sqrt":
                        stack.Push(Math.Sqrt(stack.Pop()));
                        break;

                    default:
                        throw new Exception($"Неизвестный оператор: {token}");
                }
            }
        }

        if (stack.Count != 1)
            throw new Exception("Ошибка в выражении: остались лишние элементы в стеке");

        return stack.Pop();
    }
}
        
        
    }
    

    

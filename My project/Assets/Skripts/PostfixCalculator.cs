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
        [SerializeField] private TMP_InputField inputField;   // поле ввода инфиксного выражения
        [SerializeField] private TextMeshProUGUI outputText;
        [SerializeField] private Button _starfButton;
        [SerializeField] private Button _convertButton; // кнопка для конвертации

        public void Init()
        {
            _starfButton.onClick.AddListener(OnCalculateClick);
            _convertButton.onClick.AddListener(OnConvertClick);
        }

        public void OnConvertClick()
        {
            try
            {
                string infixExpression = inputField.text;
                string postfixExpression = InfixToPostfix(infixExpression);
                inputField.text = postfixExpression;
                outputText.text = $"Постфиксная запись: {postfixExpression}";
            }
            catch (Exception ex)
            {
                outputText.text = $"Ошибка конвертации: {ex.Message}";
            }
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

        private string InfixToPostfix(string infixExpression)
        {
            // Словарь приоритетов операторов
            Dictionary<string, int> precedence = new Dictionary<string, int>
            {
                { "+", 1 }, { "-", 1 },
                { "*", 2 }, { "/", 2 }, { ":", 2 },
                { "^", 3 },
                { "sin", 4 }, { "cos", 4 }, { "ln", 4 }, { "sqrt", 4 }
            };

            Stack<string> operatorStack = new Stack<string>();
            List<string> output = new List<string>();
            
            // Разбиваем выражение на токены
            string[] tokens = TokenizeInfix(infixExpression);

            foreach (string token in tokens)
            {
                if (string.IsNullOrWhiteSpace(token)) continue;

                // Если токен - число, добавляем в выход
                if (IsNumber(token))
                {
                    output.Add(token);
                }
                // Если токен - функция, кладём в стек
                else if (IsFunction(token))
                {
                    operatorStack.Push(token);
                }
                // Если токен - оператор
                else if (precedence.ContainsKey(token))
                {
                    while (operatorStack.Count > 0 && 
                           operatorStack.Top() != "(" && 
                           precedence.ContainsKey(operatorStack.Top()) &&
                           (precedence[operatorStack.Top()] > precedence[token] || 
                            (precedence[operatorStack.Top()] == precedence[token] && token != "^")) &&
                           !IsFunction(operatorStack.Top()))
                    {
                        output.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(token);
                }
                // Если открывающая скобка
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                // Если закрывающая скобка
                else if (token == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Top() != "(")
                    {
                        output.Add(operatorStack.Pop());
                    }
                    
                    if (operatorStack.Count == 0)
                        throw new Exception("Несбалансированные скобки");
                    
                    operatorStack.Pop(); // Убираем открывающую скобку
                    
                    // Если на вершине стека функция - добавляем в выход
                    if (operatorStack.Count > 0 && IsFunction(operatorStack.Top()))
                    {
                        output.Add(operatorStack.Pop());
                    }
                }
            }

            // Выталкиваем оставшиеся операторы из стека
            while (operatorStack.Count > 0)
            {
                if (operatorStack.Top() == "(" || operatorStack.Top() == ")")
                    throw new Exception("Несбалансированные скобки");
                
                output.Add(operatorStack.Pop());
            }

            return string.Join(" ", output);
        }

        private string[] TokenizeInfix(string expression)
        {
            List<string> tokens = new List<string>();
            string currentToken = "";
            
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                
                if (char.IsWhiteSpace(c))
                {
                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                    continue;
                }
                
                if (char.IsDigit(c) || c == '.' || c == ',')
                {
                    currentToken += c;
                }
                else if (char.IsLetter(c))
                {
                    if (!string.IsNullOrEmpty(currentToken) && !char.IsLetter(currentToken[0]))
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                    currentToken += c;
                }
                else if (IsOperator(c.ToString()) || c == '(' || c == ')')
                {
                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                    tokens.Add(c.ToString());
                }
            }
            
            if (!string.IsNullOrEmpty(currentToken))
            {
                tokens.Add(currentToken);
            }
            
            return tokens.ToArray();
        }

        private bool IsNumber(string token)
        {
            return double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        }

        private bool IsFunction(string token)
        {
            return token == "sin" || token == "cos" || token == "ln" || token == "sqrt";
        }

        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == ":" || token == "^";
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
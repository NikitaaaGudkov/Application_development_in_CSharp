namespace ConsoleApp1
{
    internal class Calculator
    {
        public double result;
        public event EventHandler<CalculatorArgs> ShowResult = null!;

        public Stack<double> stack;
        public Calculator()
        {
            result = 0;
            stack = new Stack<double>();
        }

        public void Calculation(string? symbol, string? value)
        {
            value = value?.Replace(".", ",");
            if (!double.TryParse(value, out double number))
            {
                Console.WriteLine("Ошибка! Вы ввели не число");
            }
            else
            {
                stack.Push(result);
                switch (symbol)
                {
                    case "+":
                        {
                            result += number;
                            break;
                        }
                    case "-":
                        {
                            result -= number;
                            break;
                        }
                    case "*":
                        {
                            result *= number;
                            break;
                        }
                    case "/":
                        {
                            if (number == 0)
                            {
                                Console.WriteLine("Вы ввели 0. На ноль делить нельзя!");
                                stack.Pop();
                            }
                            else
                            {
                                result /= number;
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Ошибка! Неизвестная операция");
                            break;
                        }
                }
                result = Math.Round(result, 3, MidpointRounding.AwayFromZero);
            }    
            ShowResult.Invoke(this, new CalculatorArgs(result));
        }


        public void CancelLastCalculation()
        {
            result = stack.Count > 0 ? stack.Pop() : 0;
            ShowResult.Invoke(this, new CalculatorArgs(result));
        }
    }

    public class CalculatorArgs : EventArgs
    {
        public double answer;
        public CalculatorArgs(double value)
        {
            answer = value;
        }
    }
}

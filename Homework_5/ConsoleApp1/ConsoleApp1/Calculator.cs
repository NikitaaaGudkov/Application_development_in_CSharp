namespace ConsoleApp1
{
    internal class Calculator
    {
        public int result;
        public event EventHandler<CalculatorArgs> ShowResult = null!;

        public Stack<int> stack;
        public Calculator()
        {
            result = 0;
            stack = new Stack<int>();
        }

        public void Calculation(string? symbol, string? value)
        {
            if (!int.TryParse(value, out int number))
            {
                Console.WriteLine("Ошибка! Вы ввели не число");
            }
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
                        result /= number;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Ошибка! Неизвестная операция");
                        break;
                    }
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
        public int answer;
        public CalculatorArgs(int value)
        {
            answer = value;
        }
    }
}

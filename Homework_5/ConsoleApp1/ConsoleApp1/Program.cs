/*
Доработайте программу калькулятор реализовав выбор действий и вывод результатов на экран в цикле так 
чтобы калькулятор мог работать до тех пор пока пользователь не нажмет отмена или введёт пустую строку.
 */


using ConsoleApp1;

Calculator calculator = new Calculator();
calculator.ShowResult += PrintResult;
while (true)
{
    Console.Write("Введите операцию: +, -, *, /.\n" +
        "Для отмены крайнего действия напишите \"возврат\".\n" +
        "Для завершения программы напишите \"отмена\" или введите пустую строку\t");
    string? operation = Console.ReadLine();
    if(operation == string.Empty || operation == "отмена")
    {
        Console.WriteLine("Завершение работы...");
        break;
    }
    else if (operation == "возврат")
    {
        calculator.CancelLastCalculation();
    }
    else
    {
        Console.Write("Введите число: ");
        string? number = Console.ReadLine();
        calculator.Calculation(operation, number);
    }
    Console.WriteLine();
}
calculator.ShowResult -= PrintResult;


void PrintResult(object? sender, CalculatorArgs e)
{
    Console.WriteLine($"Текущий результат равен = {e.answer}");
}
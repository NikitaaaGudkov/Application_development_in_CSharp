// Дан массив и число. Найдите три числа в массиве сумма которых равна искомому числу.
// Подсказка: если взять первое число в массиве, можно ли найти в оставшейся его части
// два числа равных по сумме первому.

int[] array = new int[] { 2, 5, 7, 3, 1, 9, 4, 8, 6 };
int desiredNumber = 10;

for (int i = 0; i < array.Length; ++i)
{
	for (int j = i + 1; j < array.Length; ++j)
	{
		for (int k = j + 1; k < array.Length; ++k)
		{
			if(array[i] + array[j] + array[k] == desiredNumber)
			{
				Console.WriteLine($"{array[i]} + {array[j]} + {array[k]} = {desiredNumber}");
				return;
			}
		}
	}
}
Console.WriteLine("Таких чисел нет :(");
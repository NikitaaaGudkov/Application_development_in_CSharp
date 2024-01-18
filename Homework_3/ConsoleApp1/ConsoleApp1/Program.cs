//Доработайте приложение поиска пути в лабиринте, но на этот раз вам нужно определить сколько всего выходов
//имеется в трёхмерном лабиринте:
//int[,,] labirynth = new int[5, 5, 5];
//Сигнатура метода:
//static int HasExit(int startI, int startJ, int[,] l)


int[,,] labirynth1 = new int[,,]
                 { {
                 {1, 1, 1, 0, 1, 0, 0 },
                 {1, 0, 0, 0, 0, 0, 1 },
                 {1, 0, 1, 1, 1, 0, 1 },
                 {0, 0, 0, 0, 1, 0, 0 },
                 {1, 1, 0, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 }
                 },
                 {
                 {1, 1, 1, 0, 1, 0, 0 },
                 {1, 0, 0, 0, 0, 0, 1 },
                 {1, 0, 1, 1, 1, 0, 1 },
                 {0, 0, 0, 0, 1, 0, 0 },
                 {1, 1, 0, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 }
                 },
                 {
                 {1, 1, 1, 0, 1, 0, 0 },
                 {1, 0, 0, 0, 0, 0, 1 },
                 {1, 0, 1, 1, 1, 0, 1 },
                 {0, 0, 0, 0, 1, 0, 0 },
                 {1, 1, 0, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 }
                 } };

int[,,] labirynth2 = new int[,,]
                 { {
                 {1, 1, 1, 0, 1, 0, 0 },
                 {1, 0, 0, 0, 0, 0, 1 },
                 {1, 0, 1, 1, 1, 0, 1 },
                 {0, 0, 0, 0, 1, 0, 0 },
                 {1, 1, 0, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 }
                 },
                 {
                 {1, 1, 1, 0, 1, 0, 0 },
                 {1, 0, 0, 0, 0, 0, 1 },
                 {1, 0, 1, 1, 1, 0, 1 },
                 {0, 0, 0, 0, 1, 0, 0 },
                 {1, 1, 0, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 }
                 },
                 {
                 {1, 1, 1, 0, 1, 0, 0 },
                 {1, 0, 0, 0, 0, 0, 1 },
                 {1, 0, 1, 1, 1, 0, 1 },
                 {0, 0, 0, 0, 1, 0, 0 },
                 {1, 1, 0, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 },
                 {1, 1, 1, 0, 1, 1, 1 }
                 } };

Console.WriteLine(FindThroughRecursion(1, 3, 3, labirynth1));

Console.WriteLine(FindThroughQueue(1, 3, 3, labirynth2));


// Решение через рекурсию
static int FindThroughRecursion(int z, int x, int y, int[,,] array)
{
    if (!IsEmpty(z, x, y, array))
        return 0;

    int count = 0;
    array[z, x, y] = 2;

    count += SearchExit(z, x, y, array);

    count += FindThroughRecursion(z - 1, x, y, array);

    count += FindThroughRecursion(z, x, y + 1, array);
    count += FindThroughRecursion(z, x + 1, y, array);
    count += FindThroughRecursion(z, x - 1, y, array);
    count += FindThroughRecursion(z, x, y - 1, array);

    count += FindThroughRecursion(z + 1, x, y, array);

    return count;
}


// Проверка, можно ли перейти в данную ячейку
static bool IsEmpty(int z, int x, int y, int[,,] array)
{
    if (x < 0 || x >= array.GetLength(1))
        return false;
    if (y < 0 || y >= array.GetLength(2))
        return false;
    if (z < 0 || z >= array.GetLength(0))
        return false;
    return array[z, x, y] == 0;
}


// Поиск всех возможных выходов данной ячейки
static int SearchExit(int z, int x, int y, int[,,] array)
{
    int result = 0;

    if (x - 1 < 0 || x + 1 >= array.GetLength(1))
        ++result;
    if (y - 1 < 0 || y + 1 >= array.GetLength(2))
        ++result;
    if (z - 1 < 0 || z + 1 >= array.GetLength(0))
        ++result;
    return result;
}


// Решение через очередь
static int FindThroughQueue(int z, int x, int y, int[,,] array)
{
    int count = 0;
    int[] position = new int[3] { z, x, y };
    Queue<int[]> queue = new Queue<int[]>();
    queue.Enqueue(position);
    while (queue.Count > 0)
    {
        int[] currPosition = queue.Dequeue();
        int currZ = currPosition[0];
        int currX = currPosition[1];
        int currY = currPosition[2];
        if (IsEmpty(currZ, currX, currY, array))
        {
            array[currZ, currX, currY] = 2;
            count += SearchExit(currZ, currX, currY, array);
            queue.Enqueue(new int[] { currZ - 1, currX, currY });
            queue.Enqueue(new int[] { currZ + 1, currX, currY });
            queue.Enqueue(new int[] { currZ, currX - 1, currY });
            queue.Enqueue(new int[] { currZ, currX + 1, currY });
            queue.Enqueue(new int[] { currZ, currX, currY - 1 });
            queue.Enqueue(new int[] { currZ, currX, currY + 1 });
        }
    }
    return count;
}
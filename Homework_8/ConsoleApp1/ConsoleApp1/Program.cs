/*
Объедините две предыдущих работы (практические работы 2 и 3): поиск файла и поиск текста в файле 
написав утилиту которая ищет файлы определенного расширения с указанным текстом. 
Рекурсивно. Пример вызова утилиты: utility.exe txt текст. 
*/

string file = "utility.txt";
string text = "текст";
string directory = @"C:\Users\user\Desktop\HW_8";
if(!Directory.Exists(directory))
{
	Console.WriteLine($"Директории {directory} не существует");
	return;
}
List<string> result = SearchFile(file, text, directory);
if (result.Count == 0)
{
    Console.WriteLine($"Файл {file} с указанным текстом \"{text}\" в директории {directory} отсутствует");
}
else
{
    Console.WriteLine($"Найдены следующие пути файла {file} с указанным текстом \"{text}\":");
    foreach (string item in result)
    {
        Console.WriteLine(item);
    }
}



static List<string> SearchFile(string targetFile, string targetText, string curDirectory)
{
	var result = new List<string>();
    string[] files = Directory.GetFiles(curDirectory);
	foreach (var file in files)
	{
		if(Path.GetFileName(file) == targetFile)
		{
            using (StreamReader streamReader = new StreamReader(new FileStream(file, FileMode.Open, FileAccess.Read)))
			{
				string text = streamReader.ReadToEnd();
				if (text.Contains(targetText))
				{
					result.Add(file);
				}
			}
		}
	}

	string[] directories = Directory.GetDirectories(curDirectory);
	foreach(var directory in directories) 
	{
		result.AddRange(SearchFile(targetFile, targetText, directory));
	}
	return result;
}
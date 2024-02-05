/*
Напишите 2 метода использующие рефлексию
1 - сохраняет информацию о классе в строку
2- позволяет восстановить класс из строки с информацией о классе
В качестве примере класса используйте класс TestClass.

class TestClass
{
public int I { get; set; }
public string? S { get; set; }
public decimal D { get; set; }
public char[]? C { get; set; }

public TestClass()
{ }
private TestClass(int i)
{
    this.I = i;
}
public TestClass(int i, string s, decimal d, char[] c):this(i)
{
    this.S = s;
    this.D = d;
    this.C = c;
}
}
 */

using System.Collections;
using System.Text;


TestClass tc = new TestClass(5, "строка", 10m, new char[] { 'A', 'B', 'C'});
string str = ObjectToString(tc);
Console.WriteLine(str);
object? obj = StringToObject(str);
Console.WriteLine(obj?.GetType().FullName);

string ObjectToString(object obj)
{
    StringBuilder sb = new StringBuilder();
    Type type = obj.GetType();
    sb.Append(type.AssemblyQualifiedName + ":" + type.Name + "|");
    var properties = type.GetProperties();
    foreach (var p in properties)
    {
        sb.Append(p.Name + ":");
        if(typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
        {
            IEnumerable? elem = p.GetValue(obj) as IEnumerable;
            foreach (var item in elem!)
            {
                sb.Append(item);
            }
        }
        else
        {
            sb.Append(p.GetValue(obj));
        }
        sb.Append('|');
    }
    return sb.ToString();
}

object? StringToObject(string info)
{
    var infoToArray = info.Split(',');

    var assemblyName = infoToArray[1].Trim();
    var typeName = infoToArray[0].Trim();

    var assemblyQualifiedNameString = info.Split('|')[0];
    var paramToArray = info.Split('|').Where(str => str != string.Empty && str != assemblyQualifiedNameString).ToArray<string>();
    var parameters = new object[paramToArray.Length];
    for (int i = 0; i < paramToArray.Length; ++i)
    {
        string typeValueParameter = paramToArray[i].Split(':')[0];
        object value = null!;
        switch(typeValueParameter)
        {
            case "I":
            {
                value = int.Parse(paramToArray[i].Split(':')[1]);
                break;
            }
            case "S":
            {
                value = paramToArray[i].Split(':')[1];
                break;
            }
            case "D":
            {
                value = Convert.ToDecimal(paramToArray[i].Split(':')[1]);
                break;
            }
            case "C":
            {
                value = paramToArray[i].Split(':')[1].ToCharArray();
                break;
            }
        }
        parameters[i] = value;
    }

    object? result = Activator.CreateInstance(assemblyName, typeName, true, 0, null, parameters, null, null)?.Unwrap();
    return result;
}


class TestClass
{
    public int I { get; set; }
    public string? S { get; set; }
    public decimal D { get; set; }
    public char[]? C { get; set; }

    public TestClass()
    { }
    private TestClass(int i)
    {
        this.I = i;
    }
    public TestClass(int i, string s, decimal d, char[] c) : this(i)
    {
        this.S = s;
        this.D = d;
        this.C = c;
    }
}

/*
 * Напишите приложение, конвертирующее произвольный JSON в XML. Используйте JsonDocument.
*/

using System.Text.Json;
using System.Xml.Linq;


// Произвольный JSON
string jsonData = """
    {
      "name": "Никита",
      "surname": "Гудков",
      "age": 25,
      "hobby": [
      "computer game", "sport", "walk"
      ],
      "other":
      {
        "par1": "value1",
        "par2": "value2"
      }
    }
    """;


Console.WriteLine(ConvertJsonToXml(jsonData));


static XDocument ConvertJsonToXml(string jsonString)
{
    using JsonDocument doc = JsonDocument.Parse(jsonString);
    return new XDocument(ConvertJsonElementToXElement(doc.RootElement));
}


static XElement ConvertJsonElementToXElement(JsonElement element)
{
    switch (element.ValueKind)
    {
        case JsonValueKind.Object:
            var obj = new XElement("object");
            foreach (var property in element.EnumerateObject())
            {
                obj.Add(new XElement(property.Name, ConvertJsonElementToXElement(property.Value)));
            }
            return obj;

        case JsonValueKind.Array:
            var arr = new XElement("array");
            foreach (var item in element.EnumerateArray())
            {
                arr.Add(ConvertJsonElementToXElement(item));
            }
            return arr;

        case JsonValueKind.String:
            return new XElement("value", element.GetString());

        case JsonValueKind.Number:
            return new XElement("value", element.GetDecimal());

        case JsonValueKind.True:
            return new XElement("value", true);

        case JsonValueKind.False:
            return new XElement("value", false);

        default:
            return null!;
    }
}
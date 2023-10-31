using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;
using NccsvConverter.NccsvSerializer.SchemaDatasetJsonSerializer;

Console.WriteLine("If you want to validate, write name of file:");
string filePathFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
                        + "\\NccsvConverter.ConsoleApp\\TestData\\";
string fileName = Console.ReadLine();
string filePath = Path.Combine(filePathFolder, fileName);

if (File.Exists(filePath))
{
    DataSet dataSet = DataSet.FromFile(filePath, true);

    if (MessageRepository.Messages.Count > 0)
    {
        foreach (var message in MessageRepository.Messages)
        {
            Console.WriteLine(message.Text);
        }
    }
    else
    {
        Console.WriteLine("No problems found.");

        Console.WriteLine("If you want to serialize, press y:");
        var input = Console.ReadKey();
        Console.WriteLine();
        if (input.KeyChar == 'y')
            Console.WriteLine(Serializer.ToJson(dataSet));
    }
}
else
    Console.WriteLine($"File \"{filePath}\" could not be found.");
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;
using NccsvConverter.NccsvSerializer.SchemaDatasetJsonSerializer;

Console.WriteLine("Write name of file or press enter to quit");
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

        // TODO: make optional
        Serializer.ToJson(dataSet);
    }
}
else
    Console.WriteLine($"File \"{filePath}\" could not be found.");









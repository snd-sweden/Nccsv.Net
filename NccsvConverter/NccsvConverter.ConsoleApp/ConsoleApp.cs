using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

Console.WriteLine("Write name of file or press enter to quit");
string filePathFolder = "C:\\SND\\Project\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\";
string fileName = Console.ReadLine();
string filePath = Path.Combine(filePathFolder, fileName);

DataSet dataSet = new();

if (File.Exists(filePath))
{
    dataSet.FromFile(filePath, true);

    if (MessageRepository.Messages.Count > 0)
    {
        foreach (var message in MessageRepository.Messages)
        {
            Console.WriteLine(message.Text);
        }
    }
    else
        Console.WriteLine("No problems found.");
}
else
    Console.WriteLine($"File \"{filePath}\" could not be found.");








using NccsvConverter.NccsvParser.FileHandling;
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

Console.WriteLine("File path: ");
string filePathFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
            + "\\NccsvConverter.ConsoleApp\\TestData\\";
string fileName = Console.ReadLine();
string filePath = Path.Combine(filePathFolder, fileName);

var separatedLines = new List<string[]>();

if (Verifier.VerifyPath(filePath))
{
    separatedLines = Handler.NccsvFileReader(filePath);
}

if (separatedLines != null && Verifier.VerifySeparatedLines(separatedLines))
{
    Handler.NccsvHandler(separatedLines);
}

if (MessageRepository.Messages.Count > 0)
{
    foreach (var message in MessageRepository.Messages)
    {
        Console.WriteLine(message.Text);
    }
}
else
    Console.WriteLine("No problems found.");





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

var dataSet = new DataSet();

if (Verifier.VerifySeparatedLines(separatedLines))
{
    dataSet = Handler.NccsvHandler(separatedLines);
}


foreach (var message in MessageRepository.Messages)
{
    Console.WriteLine(message.Text);
}




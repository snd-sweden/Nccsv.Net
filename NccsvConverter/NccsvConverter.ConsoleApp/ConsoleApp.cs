// See https://aka.ms/new-console-template for more information

using NccsvConverter.NccsvParser.FileHandling;
using NccsvConverter.NccsvParser.Helpers;

Console.WriteLine("Hello, World!");
var parser = new Parser();
var csv = parser.FromText(
    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\TestData\\ryder.nccsv");

    foreach (var s in csv[0])
    {
        Console.WriteLine(s);
    }
    




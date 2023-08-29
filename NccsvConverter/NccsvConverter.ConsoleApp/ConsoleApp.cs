// See https://aka.ms/new-console-template for more information

using NccsvConverter.NccsvParser.FileHandling;
using NccsvConverter.NccsvParser.Helpers;

Console.WriteLine("Hello, World!");
var verifier = new NccsvVerifierMethods();
var parser = new Parser();
var csv = parser.FromText("C:\\SND\\Project\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv");

    foreach (var s in csv[0])
    {
        Console.WriteLine(s);
    }
    




// See https://aka.ms/new-console-template for more information

using NccsvConverter.MainProject.NccsvParser.FileHandling;
using NccsvConverter.MainProject.NccsvParser.Helpers;

Console.WriteLine("Hello, World!");
var verifier = new NccsvVerifierMethods();
var parser = new NccsvParser();
var csv = parser.FromText("C:\\SND\\Project\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv");

    foreach (var s in csv[0])
    {
        Console.WriteLine(s);
    }
    




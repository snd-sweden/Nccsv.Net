// See https://aka.ms/new-console-template for more information

using NccsvConverter.NccsvParser.FileHandling;
using NccsvConverter.NccsvParser.Helpers;

Console.WriteLine("Hello, World!");
var verifier = new NccsvVerifierMethods();
var parser = new NccsvParserMethods();
var fileReader = new Parser();
var csv = fileReader.FromText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\TestData\\ryder.nccsv");

//foreach (var s in csv[0])
//{
//    Console.WriteLine(s);
//}

Console.WriteLine(parser.FindProperties(csv)[0][0]);





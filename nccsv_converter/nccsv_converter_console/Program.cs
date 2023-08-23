// See https://aka.ms/new-console-template for more information

using nccsv_verifier;

var nccsv = new NccsvVerifierMethods();
Console.WriteLine("Hello, World!");
Console.WriteLine(nccsv.Utf8Checker("C:\\SND_repos\\nccsv_netcdf\\nccsv_converter\\nccsv_verifier_tests\\testcase-utf-8-true.csv"));
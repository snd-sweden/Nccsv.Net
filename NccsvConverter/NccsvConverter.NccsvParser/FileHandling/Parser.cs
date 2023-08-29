using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.FileHandling;

public class Parser
{
    public List<string[]> FromText(string fileName)
    {
        var csv = new List<string[]>();

        var lines = File.ReadAllLines(fileName);

        foreach (var line in lines)
            csv.Add(line.Split(','));

        return csv;
    }

    public DataSet FromList(List<string[]> csv)
    {
        NccsvVerifierMethods verifier = new NccsvVerifierMethods();
        NccsvParserMethods parser = new NccsvParserMethods();
        DataSet ds = new DataSet();

        // Verify
        // check for extension
        // check for tag


        // Parse
        // FindGlobalProperties

        // AddGlobalProperties

        // FindProperties

        // CheckIfVariableExists

        // if not -> create new variable

        // CreateVariable


        // SetVariableDataType

        // AddProperties

        // FindData

        return new DataSet();
    }

}
using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.FileHandling;

public class Parser
{
    // FromText gets a .csv file and converts it into a array of strings for further handling.
    public static List<string[]> FromText(string fileName)
    {
        var csv = new List<string[]>();

        // check for extension

        var lines = File.ReadAllLines(fileName);

        string[] separatedLine;

        foreach (var line in lines) 
        {
            if (line != string.Empty)
            {
                separatedLine = NccsvParserMethods.Separate(line).ToArray();
                csv.Add(separatedLine);
            }
        }

        return csv;
    }

    public static DataSet FromList(List<string[]> csv)
    {
        
        DataSet ds = new DataSet();

        // Verify
        // check for tag
        if (!NccsvVerifierMethods.VerifyNccsv(csv))
        {
            throw new Exception();
        }

        // Parse

        // FindGlobalProperties
        var globalProps = NccsvParserMethods.FindGlobalProperties(csv);

        //Add title and summary

        ds.Title = NccsvParserMethods.FindTitle(globalProps);
        ds.Summary = NccsvParserMethods.FindSummary(globalProps);

        // AddGlobalProperties
        NccsvParserMethods.AddGlobalProperties(ds, globalProps);

        // FindVariables
        var variables = NccsvParserMethods.FindVariables(csv);

        // AddVariables

        foreach (var v in variables)
        {
            // CheckIfVariableExists
            if (!NccsvParserMethods.CheckIfVariableExists(ds.Variables, v[0]))
            {
                var varToCreate = NccsvParserMethods.IsolateProperty(variables, v[0]);
                // CreateVariable
                var newVariable = NccsvParserMethods.CreateVariable(varToCreate);

                // SetVariableDataType
                NccsvParserMethods.SetVariableDataType(newVariable, varToCreate);

                // Add to variable list of DataSet
                ds.Variables.Add(newVariable);
            }

        }
        
        // FindData
        var dataSection = NccsvParserMethods.FindData(csv);
        NccsvParserMethods.AddData(dataSection, ds);

        return ds;
    }

}
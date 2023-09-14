using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.FileHandling;

public class Parser
{
    // FromText gets a .csv file and converts it into a array of strings for further handling.
    public static List<string[]> FromText(string filePath)
    {
        var separatedNccsv = new List<string[]>();

        // check for extension

        var lines = File.ReadAllLines(filePath);

        string[] separatedLine;

        foreach (var line in lines) 
        {
            if (line != string.Empty)
            {
                separatedLine = NccsvParserMethods.Separate(line).ToArray();
                separatedNccsv.Add(separatedLine);
            }
        }

        return separatedNccsv;
    }

    public static DataSet FromList(List<string[]> separatedNccsv)
    {
        
        DataSet dataSet = new DataSet();

        // Verify
        // check for tag
        if (!NccsvVerifierMethods.VerifyNccsv(separatedNccsv))
        {
            throw new Exception();
        }

        // Parse

        // FindGlobalAttributes
        var globalAttributes = NccsvParserMethods.FindGlobalAttributes(separatedNccsv);

        //Add title and summary

        dataSet.Title = NccsvParserMethods.FindTitle(globalAttributes);
        dataSet.Summary = NccsvParserMethods.FindSummary(globalAttributes);

        // AddGlobalAttributes
        NccsvParserMethods.AddGlobalAttributes(dataSet, globalAttributes);

        // FindVariableMetaData
        var variableMetaData = NccsvParserMethods.FindVariableMetaData(separatedNccsv);

        // AddVariables

        foreach (var line in variableMetaData)
        {
            // CheckIfVariableExists
            if (!NccsvParserMethods.CheckIfVariableExists(dataSet.Variables, line[0]))
            {
                var varToCreate = NccsvParserMethods.IsolateVariableAttributes(variableMetaData, line[0]);
                // CreateVariable
                var newVariable = NccsvParserMethods.CreateVariable(varToCreate);

                // SetVariableDataType
                NccsvParserMethods.SetVariableDataType(newVariable, varToCreate);

                // Add to variable list of DataSet
                dataSet.Variables.Add(newVariable);
            }

        }
        
        // FindData
        var dataSection = NccsvParserMethods.FindData(separatedNccsv);
        NccsvParserMethods.AddData(dataSection, dataSet);

        return dataSet;
    }

}
using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.FileHandling;

public class Handler
{
    // Gets a .nccsv file and converts it into a array of strings for further handling.
    public static List<string[]> NccsvFileReader(string filePath)
    {
        var separatedNccsv = new List<string[]>();

        var lines = File.ReadAllLines(filePath); // TODO: handle exceptions

        // TODO: move following code into separate method, issue #68
        if (Verifier.VerifyLines(lines))
        {
            string[] separatedLine;

            foreach (var line in lines) 
            {
                if (line != string.Empty)
                {
                    separatedLine = NccsvParserMethods.Separate(line, Array.IndexOf(lines, line) + 1).ToArray();
                    separatedNccsv.Add(separatedLine);
                }
            }

            return separatedNccsv;
        }
        else
            return null;
        
    }


    // Takes a separated nccsv as a list of arrays and creates
    // a data set with given data and metadata from the nccsv file
    public static DataSet NccsvHandler(List<string[]> separatedNccsv)
    {
        DataSet dataSet = new DataSet();


        // Find and add global attributes to dataset
        var globalAttributes = NccsvParserMethods.FindGlobalAttributes(separatedNccsv);

        dataSet.Title = NccsvParserMethods.FindTitle(globalAttributes);
        dataSet.Summary = NccsvParserMethods.FindSummary(globalAttributes);

        NccsvParserMethods.AddGlobalAttributes(dataSet, globalAttributes);


        // Find variable metadata
        var variableMetaData = NccsvParserMethods.FindVariableMetaData(separatedNccsv);

        //TODO: if
        Verifier.VerifyVariableMetaData(variableMetaData);


        // Find data
        var dataSection = NccsvParserMethods.FindData(separatedNccsv);

        //TODO: if
        Verifier.VerifyData(dataSection);


        // Create variables from variable metadata and add to dataset
        foreach (var line in variableMetaData)
        {
            if (!NccsvParserMethods.CheckIfVariableExists(dataSet.Variables, line[0]))
            {
                var varToCreate = NccsvParserMethods.IsolateVariableAttributes(variableMetaData, line[0]);

                var newVariable = NccsvParserMethods.CreateVariable(varToCreate);

                NccsvParserMethods.SetVariableDataType(newVariable, varToCreate);

                //TODO: if
                Verifier.VerifyVariable(newVariable, dataSection);

                dataSet.Variables.Add(newVariable);
            }
        }
        

        // Add data to dataSet
        NccsvParserMethods.AddData(dataSection, dataSet);


        //TODO: if
        Verifier.VerifyDataSet(dataSet);

        return dataSet;
    }

}
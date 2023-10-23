using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Repositories;
using System.Data;
using NccsvConverter.NccsvParser.Validators;

namespace NccsvConverter.NccsvParser.Models;

public class DataSet
{
    private MetaData _metaData = new MetaData();
    public MetaData MetaData { get; set; }
    public List<DataValue[]> Data { get; set; } = new();


    public DataSet()
    {
        MetaData = _metaData;
    }


    public static DataSet FromFile(string filePath, bool saveData = false)
    {
        ExtensionValidator.Validate(filePath);
        FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return FromStream(stream, saveData);
    }


    public static DataSet FromStream(FileStream stream, bool saveData = false)
    {
        DataSet dataSet = new();
        string line;
        List<string[]> metaDataList = new();
        List<string> headers = new();

        bool endMetaDataFound = false;
        bool endDataFound = false;
        bool headersFound = false;

        int rowNumber = 0;

        using StreamReader sr = new StreamReader(stream);
        {
            while ((line = sr.ReadLine()) != null)
            {
                rowNumber++;

                if (line == string.Empty)
                    continue;

                if (line == "*END_METADATA*")
                {
                    endMetaDataFound = true;
                    headersFound = true;
                    continue;
                }

                var separatedLine = NccsvParserMethods.Separate(line, rowNumber);

                if (!endMetaDataFound)
                    metaDataList.Add(separatedLine.ToArray());
                else if (endMetaDataFound)
                {
                    if (line == "*END_DATA*")
                    {
                        endDataFound = true;
                        DataValidator.Validate(endDataFound);
                        break;
                    }
                    else if (headersFound)
                    {
                        //TODO: verify headers? check for scalar variables
                        MetaDataValidator.Validate(metaDataList, endMetaDataFound);
                        dataSet.MetaDataHandler(metaDataList);
                        headers = separatedLine;
                        headersFound = false;
                        continue;
                    }
                    else
                        dataSet.DataRowHandler(separatedLine.ToArray(), headers.ToArray(), rowNumber, saveData);
                }
            }

            if (rowNumber == 0)
            {
                MessageRepository.Messages.Add(
                    new Message($"File is empty.", Severity.Critical));
            }

            return dataSet;
        }
    }


    private void MetaDataHandler(List<string[]> metaDataList)
    {
        // Find and add global attributes to dataset
        var globalAttributes = NccsvParserMethods.FindGlobalAttributes(metaDataList);

        MetaData.Title = NccsvParserMethods.FindTitle(globalAttributes);
        MetaData.Summary = NccsvParserMethods.FindSummary(globalAttributes);

        NccsvParserMethods.AddGlobalAttributes(this, globalAttributes);

        // Find variable metadata
        var variableMetaData = NccsvParserMethods.FindVariableMetaData(metaDataList);

        if (!VariableMetaDataValidator.Validate(variableMetaData))
            return;

        // Create variables from variable metadata and add to dataset
        foreach (var line in variableMetaData)
        {
            if (!NccsvParserMethods.CheckIfVariableExists(MetaData.Variables, line[0]))
            {
                var varToCreate = NccsvParserMethods.IsolateVariableAttributes(variableMetaData, line[0]);

                var newVariable = NccsvParserMethods.CreateVariable(varToCreate);

                NccsvParserMethods.SetVariableDataType(newVariable, varToCreate);

                //TODO: Verify variable?

                MetaData.Variables.Add(newVariable);
            }
        }
    }


    private void DataRowHandler(string[] dataRow, string[] headers, int rowNumber, bool saveData)
    {
        if (!DataRowValidator.Validate(dataRow, headers, rowNumber))
            return;
        NccsvParserMethods.AddData(dataRow, headers, this, rowNumber, saveData);
    }
}
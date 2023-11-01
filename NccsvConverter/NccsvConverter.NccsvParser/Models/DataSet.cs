using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Repositories;
using NccsvConverter.NccsvParser.Validators;
using System.Globalization;

namespace NccsvConverter.NccsvParser.Models;

public class DataSet
{
    private MetaData _metaData; 
    public MetaData MetaData
    {
        get { return _metaData; }
    }
    private List<DataValue[]> _data = new();
    public List<DataValue[]> Data 
    {
        get { return _data; }
    }


    // Parses and validates NCCSV from file. Returns a DataSet object.
    // By default both data and metadata is validated but only metadata is saved.
    // You can choose to save the data section by setting saveData = true.
    public static DataSet FromFile(string filePath, bool saveData = false)
    {
        ExtensionValidator.Validate(filePath);
        FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return FromStream(stream, saveData);
    }


    // Parses and validates NCCSV from stream. Returns a DataSet object.
    // By default both data and metadata is validated but only metadata is saved.
    // You can choose to save the data section by setting saveData = true.
    public static DataSet FromStream(Stream stream, bool saveData = false)
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
                        DataValidator.Validate(endDataFound, headers); // TODO: endDataFound will always be true, this particular check should be moved
                        break;
                    }
                    else if (headersFound)
                    {
                        //TODO: verify headers? check for scalar variables
                        MetaDataValidator.Validate(metaDataList, endMetaDataFound);
                        dataSet._metaData = new MetaData(metaDataList);
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


    // Validates and parses a data row (via SetData)
    private void DataRowHandler(string[] dataRow, string[] headers, int rowNumber, bool saveData)
    {
        if (!DataRowValidator.Validate(dataRow, headers, rowNumber))
            return;
        SetData(dataRow, headers, rowNumber, saveData);
    }


    // Parses a data row. Validates each data value in given row. If saveData = true, saves data values to DataSet.
    private void SetData(string[] dataRow, string[] headers, int rowNumber, bool saveData)
    {
        List<DataValue> dataValues = new();

        for (int i = 0; i < dataRow.Length; i++)
        {
            var variable = _metaData.Variables
                .FirstOrDefault(v => v.VariableName
                    .Equals(headers[i]));

            if (variable != null)
            {
                var dataValue = CreateDataValueAccordingToDataType(dataRow[i], variable);

                if (dataValue != null)
                {
                    if (saveData)
                        dataValues.Add(dataValue);
                }
                else
                {
                    MessageRepository.Messages.Add(
                        new Message($"Row {rowNumber}: Data value \"{dataRow[i]}\" could not be parsed to variable datatype: {variable.VariableDataType}.", Severity.NonCritical));
                }
            }
            else
            {
                MessageRepository.Messages.Add(
                    new Message($"Header \"{dataRow[i]}\" did not match any variables.", Severity.NonCritical));
            }
        }

        if (saveData)
            _data.Add(dataValues.ToArray());
    }


    // Creates and returns a DataValueAs<T> from a given value and variable,
    // where T is the DataType of the variable that acts as column header.
    // If unsuccessfull, returns a null value.
    private static DataValue? CreateDataValueAccordingToDataType(string value, Variable variable)
    {
        bool result;

        switch (variable.VariableDataType)
        {
            case "byte":
                // byte -> c# sbyte

                if (value != "")
                { 
                    result = sbyte.TryParse(value, out sbyte byteValue);

                    if (result)
                        return new DataValueAs<sbyte>()
                        {
                            Variable = variable,
                            Value = byteValue
                        };
                }
                else
                {
                    return new DataValueAs<sbyte?>()
                    {
                        Variable = variable,
                        Value = null
                    };
                }

                return null;

            case "ubyte":
                // unsigned byte -> c# byte

                if (value != "")
                {
                    result = byte.TryParse(value, out byte ubyteValue);

                    if (result)
                    return new DataValueAs<byte>()
                    {
                        Variable = variable,
                        Value = ubyteValue
                    };
                }
                else
                {
                    return new DataValueAs<byte?>()
                    {
                        Variable = variable,
                        Value = null
                    };
                }
                
                return null;

            case "short":

                if (value != "")
                {
                     result = short.TryParse(value, out short shortValue);

                    if (result)
                        return new DataValueAs<short>()
                        {
                            Variable = variable,
                            Value = shortValue
                        };
                }
                else
                {
                    return new DataValueAs<short?>()
                    {
                        Variable = variable,
                        Value = null
                    };
                }

                return null;

            case "ushort":

                if (value != "")
                {
                    result = ushort.TryParse(value, out ushort ushortValue);

                    if (result)
                        return new DataValueAs<ushort>()
                        {
                            Variable = variable,
                            Value = ushortValue
                        };
                }
                else
                {
                    return new DataValueAs<ushort?>()
                    {
                        Variable = variable,
                        Value = null
                    };
                }
                
                return null;

            case "int":

                if (value != "")
                {
                    result = int.TryParse(value, out int intValue);

                    if (result)
                        return new DataValueAs<int>()
                        {
                            Variable = variable,
                            Value = intValue
                        };
                }
                else
                {
                    return new DataValueAs<int?>()
                    {
                        Variable = variable,
                        Value = null
                    };
                }
                
                return null;

            case "uint":

                if (value != "")
                {
                    result = uint.TryParse(value, out uint uintValue);

                    if (result)
                        return new DataValueAs<uint>()
                        {
                            Variable = variable,
                            Value = uintValue
                        };
                }
                else
                {
                    return new DataValueAs<uint?>()
                    {
                        Variable = variable,
                        Value = null
                    };
                }

                return null;

            case "long":
                // TODO: check L
                if (value != "")
                {
                    result = long.TryParse(value[..^1], out long longValue);

                    if (result)
                        return new DataValueAs<long>()
                        {
                            Variable = variable,
                            Value = longValue
                        };
                }
                else
                {
                    return new DataValueAs<long?>()
                    {
                        Variable = variable,
                        Value = null
                    };
                }
                
                return null;

            case "ulong":
                // TODO: check uL
                if (value != "")
                {
                    result = ulong.TryParse(value[..^2], out ulong ulongValue);

                    if (result)
                        return new DataValueAs<ulong>()
                        {
                            Variable = variable,
                            Value = ulongValue
                        };
                }
                else
                {
                    return new DataValueAs<ulong?>()
                    {
                        Variable = variable,
                        Value = null
                    };
                }
                return null;

            case "float":

                if (value != "")
                {
                    result = float.TryParse(value, CultureInfo.InvariantCulture, out float floatValue);

                    if (result)
                    return new DataValueAs<float>()
                    {
                        Variable = variable,
                        Value = floatValue
                    };
                }
                else
                {
                    return new DataValueAs<float>()
                    {
                        Variable = variable,
                        Value = float.NaN
                    };
                }

                return null;

            case "double":

                if (value != "")
                {
                    result = double.TryParse(value, CultureInfo.InvariantCulture, out double doubleValue);

                    if (result)
                    return new DataValueAs<double>()
                    {
                        Variable = variable,
                        Value = doubleValue
                    };
                }   
                else
                {
                    return new DataValueAs<double>()
                    {
                        Variable = variable,
                        Value = double.NaN
                    };
                }
                
                return null;

            case "string":
                return new DataValueAs<string>
                {
                    Variable = variable,
                    Value = value
                };

            case "char":
                // TODO: handle special char cases
                result = char.TryParse(value, out char charValue);

                if (result)
                    return new DataValueAs<char>()
                    {
                        Variable = variable,
                        Value = charValue
                    };
                else
                    return null;

            default:
                return null;
        }
    }
}
using NccsvConverter.NccsvParser.Validators;

namespace NccsvConverter.NccsvParser.Models;

public class Variable
{
    private string _variableName;
    public string VariableName
    {
        get { return _variableName; }
    }
    private string _variableDataType;
    public string VariableDataType
    {
        get { return _variableDataType; }
    }
    private bool _scalar = false;
    public bool Scalar
    {
        get { return _scalar; }
    }
    private string? _scalarValue;
    public string? ScalarValue
    {
        get { return _scalarValue; }
    }
    private Dictionary<string, List<string>> _attributes = new();
    public Dictionary<string, List<string>> Attributes
    {
        get { return _attributes; }
    }


    public Variable(List<string[]> variableMetaData)
    {
        // This loop finds the name of the variable, even if there would be
        // empty lines before it.
        foreach (var line in variableMetaData)
        {
            if (line[0].Length > 0)
            {
                _variableName = line[0];

                //Also, adding a Scalar property to the Variable model, also inserts the value of the scalar.
                if (line[1] == "*SCALAR*")
                {
                    _scalar = true;
                    _scalarValue = line[2];
                }
                break;
            }
        }

        SetDataType(variableMetaData);
        SetAttributes(variableMetaData);
    }


    // Takes a variable metadata list, extracts the name of the data type
    // and sets a given variable data type to that name.
    // Used by: CreateVariable
    private void SetDataType(List<string[]> variableMetaData)
    {
        string variableDataType = "";

        foreach (var line in variableMetaData)
        {
            if (line[1] == "*DATA_TYPE*")
            {
                if (DataTypeValidator.Validate(line[2]))
                {
                    variableDataType = line[2].ToLower();
                }
            }

            if (line[1] == "*SCALAR*")
            {
                variableDataType = GetTypeOfAttributeValue(line[2]).ToString().ToLower();
            }
        }

        _variableDataType = variableDataType;
    }


    private DataType GetTypeOfAttributeValue(string value)
    {
        switch (value[^1])
        {
            case 'b':
                if (value[^2] == 'u')
                {
                    if (Int32.TryParse(value[..^3], out _))
                    {
                        return DataType.Ubyte;
                    }

                }
                else if (Int32.TryParse(value[..^2], out _))
                {
                    return DataType.Byte;
                }

                return DataType.String;

            case 's':
                if (value[^2] == 'u')
                {
                    if (Int32.TryParse(value[..^3], out _))
                    {
                        return DataType.Ushort;
                    }
                }

                else if (Int32.TryParse(value[..^2], out _))
                {
                    return DataType.Short;
                }

                return DataType.String;

            case 'i':
                if (value[^2] == 'u')
                {
                    if (uint.TryParse(value[..^3], out _))
                    {
                        return DataType.Uint;
                    }
                }

                else if (int.TryParse(value[..^2], out _))
                {
                    return DataType.Int;
                }

                return DataType.String;

            case 'L':
                if (value[^2] == 'u')
                {
                    if (ulong.TryParse(value[..^3], out _))
                    {
                        return DataType.Ulong;
                    }
                }

                else if (long.TryParse(value[..^2], out _))
                {
                    return DataType.Long;
                }

                return DataType.String;


            case 'f':
                value = value.Replace('.', ',').ToLower();
                if (float.TryParse(value[..^1], out _))
                {
                    return DataType.Float;
                }

                return DataType.String;


            case 'd':
                value = value.Replace('.', ',').ToLower();
                if (double.TryParse(value[..^2], out _))
                {
                    return DataType.Double;
                }

                return DataType.String;

            case '\'':
                if (value[0] == '\'')
                {
                    return DataType.Char;
                }

                return DataType.String;

            default:
                return DataType.String;
        }

    }


    // Adds attributes to a given Variable as a dictionary where
    // [1] is the attribute name and [2] to [n] is the attribute values
    private void SetAttributes(List<string[]> variableMetaData)
    {
        foreach (var attribute in variableMetaData)
        {
            // Disregard data type row as datatype is set in SetVariableDataType
            if (attribute[1] == "*DATA_TYPE*")
            {
                continue;
            }

            var attributeName = attribute[1];
            List<string> values = new List<string>();

            for (int i = 2; i < attribute.Length; i++)
            {
                values.Add(attribute[i]);
            }

            // Add to Attributes as <[1], List<[2]-[n]>>
            _attributes.Add(attributeName, values);
        }
    }
}
using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.Validators;

public class DataTypeValidator
{
    public static bool Validate(string variableDataType)
    {
        bool criticalResult = true;

        // Critical
        if (!CheckIfVariableDataTypeIsOfAcceptedType(variableDataType))
            criticalResult = false;

        return criticalResult;
    }


    // Returns true if if the data type of a variable is one of the types specified by DataType enum.
    private static bool CheckIfVariableDataTypeIsOfAcceptedType(string variableDataType)
    {
        foreach (var dataType in Enum.GetValues(typeof(DataType)))
        {
            if (dataType.ToString().ToLower() == variableDataType.ToLower())
            {
                return true;
            }
        }

        // TODO: error message
        return false;
    }
}
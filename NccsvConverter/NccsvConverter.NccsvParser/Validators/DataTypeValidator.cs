using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.Validators;

public class DataTypeValidator : Validator
{
    public static bool Validate(string variableDataType)
    {
        return CheckIfVariableDataTypeIsOfAcceptedType(variableDataType);
    }


    private static bool CheckIfVariableDataTypeIsOfAcceptedType(string variableDataType)
    {
        foreach (var dataType in Enum.GetValues(typeof(DataType)))
        {
            if (dataType.ToString().ToLower() == variableDataType.ToLower())
            {
                return true;
            }
        }
        return false;
    }
}

using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class VariableMetaDataValidator : Validator
{
    public VariableMetaDataValidator(List<string[]> variableMetaData)
    {
        //Critical

        //Non-critical
        CheckVariableNames(variableMetaData);
        CheckAttributeNames(variableMetaData);
        CheckVariableMetaDataForDataType(variableMetaData);

        _result = true;
    }

    // Variable names must not start with a digit. Returns true if name is acceptable.
    public static bool CheckVariableNames(List<string[]> variableMetaData)
    {
        bool flag = true;

        foreach (var row in variableMetaData)
        {
            if (char.IsDigit(row[0][0]))
            {
                MessageRepository.Messages.Add(
                    new Message($"Row {variableMetaData.IndexOf(row) + 1}, Variable \"{row[0]}\": Variable names can't start with a digit.", Severity.NonCritical));

                flag = false;
            }
        }

        return flag;
    }

    // Attribute names must not start with a digit. Returns true if name is acceptable.
    public static bool CheckAttributeNames(List<string[]> variableMetaData)
    {
        bool flag = true;

        foreach (var row in variableMetaData)
        {
            if (char.IsDigit(row[1][0]))
            {
                MessageRepository.Messages.Add(
                    new Message($"Row {variableMetaData.IndexOf(row) + 1}, Attribute \"{row[1]}\": Attribute names can't start with a digit.", Severity.NonCritical));

                flag = false;
            }
        }

        return flag;
    }

    // Each variable described in the metadata section should have a *DATA_TYPE* or
    // *SCALAR* attribute (where data type follows the scalar value). Returns true if the number of
    // distinct variable names matches the number of *DATA_TYPE* or *SCALAR* attributes.
    public static bool CheckVariableMetaDataForDataType(List<string[]> variableMetaData)
    {
        List<string> distinctVariables = new List<string>();

        foreach (var row in variableMetaData)
        {
            if (!distinctVariables.Contains(row[0]))
                distinctVariables.Add(row[0]);
        }

        List<string> variablesWithDataType = new List<string>();

        foreach (var row in variableMetaData
                     .Where(r => r[1]
                                     .Equals("*DATA_TYPE*")
                                 || r[1]
                                     .Equals("*SCALAR*")))
        {
            variablesWithDataType.Add(row[0]);
        }

        if (distinctVariables.Count.Equals(variablesWithDataType.Count))
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("Not all variables have a data type.", Severity.NonCritical));

            return false;
        }
    }
}
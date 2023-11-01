using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class ValueValidator
{
    public static bool Validate(string value, int row)
    {
        bool criticalResult = true;

        // Non-critical
        CheckValueForSpace(value, row);

        return criticalResult;
    }


    // Returns true if there is no whitespace before or after a given value.
    public static bool CheckValueForSpace(string value, int row)
    {
        if (value.EndsWith(" ")
            || value.StartsWith(" "))
        {
            MessageRepository.Messages.Add(
                new Message($"Row {row}, value \"{value}\": Values can't start with or end with whitespace.", 
                Severity.NonCritical));

            return false;
        }

        return true;
    }
}
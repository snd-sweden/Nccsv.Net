
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class ValueValidator : Validator
{
    public static bool Validate(string value, int row)
    {
        return CheckValueForSpace(value, row);
    }


    // Returns false if there is whitespace before or after a given value.
    public static bool CheckValueForSpace(string value, int row)
    {
        if (value.EndsWith(" ")
            || value.StartsWith(" "))
        {
            MessageRepository.Messages.Add(
                new Message($"Row {row}, value \"{value}\": Values can't start with or end with whitespace.", Severity.NonCritical));

            return false;
        }

        return true;
    }
}
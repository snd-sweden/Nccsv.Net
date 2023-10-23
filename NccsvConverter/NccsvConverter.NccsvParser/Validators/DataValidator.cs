
using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class DataValidator : Validator
{
    public static bool Validate(bool endDataFound)
    {
        //Critical

        //Non-critical
        CheckForDataEndTag(endDataFound);

        return endDataFound;
    }


    //TODO: write tests
    public static bool CheckForDataEndTag(bool dataEndFound)
    {
        if (!dataEndFound)
        {
            MessageRepository.Messages.Add(
                new Message("Couldn't find \"*END_DATA*\".", Severity.NonCritical));
            return false;
        }
        else
            return true;
    }


    // Checks for *END_DATA* that must exist at end of data section.
    // Returns true if found.
    // Note: This only checks that *END_DATA* exists *somewhere* in the file.
    public static bool CheckForDataEndTag(List<string[]> separatedLines)
    {
        foreach (var row in separatedLines)
        {
            if (row[0].Equals("*END_DATA*"))
                return true;
        }

        MessageRepository.Messages.Add(
            new Message("Couldn't find \"*END_DATA*\".", Severity.NonCritical));

        return false;
    }
}
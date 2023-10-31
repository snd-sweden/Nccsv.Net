using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class DataValidator : Validator
{
    public static bool Validate(bool endDataFound, List<string> headers)
    {
        //Critical

        //Non-critical
        CheckForHeaders(headers);
        CheckForDataEndTag(endDataFound);

        return endDataFound;
    }


    // Returns true if headers can be found (which means there is data in the data section).
    public static bool CheckForHeaders(List<string> headers)
    {
        if (headers.Count == 0)
        {
            MessageRepository.Messages.Add(
                    new Message($"Couldn't find headers.", Severity.NonCritical));
            return false;
        }
        return true;
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
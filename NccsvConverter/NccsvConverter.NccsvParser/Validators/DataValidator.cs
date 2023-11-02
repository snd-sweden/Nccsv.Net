using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class DataValidator
{
    public static bool Validate(bool endDataFound, List<string> headers)
    {
        bool criticalResult = true;

        //Non-critical
        CheckForHeaders(headers);
        CheckForDataEndTag(endDataFound);

        return criticalResult;
    }


    // Returns true if headers can be found (which means there is data in the data section).
    public static bool CheckForHeaders(List<string> headers)
    {
        if (headers.Count == 0)
        {
            MessageRepository.Messages.Add(
                    new Message($"Couldn't find headers.", 
                    Severity.NonCritical));
            return false;
        }
        return true;
    }


    // Returns true if "*END_DATA*" can be found
    public static bool CheckForDataEndTag(bool dataEndFound)
    {
        if (!dataEndFound)
        {
            MessageRepository.Messages.Add(
                new Message("Couldn't find \"*END_DATA*\".", 
                Severity.NonCritical));
            return false;
        }
        else
            return true;
    }
}
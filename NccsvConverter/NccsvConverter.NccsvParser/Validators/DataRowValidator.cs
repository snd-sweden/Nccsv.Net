using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class DataRowValidator
{
    public static bool Validate(string[] dataRow, string[] headers, int rowNumber)
    {
        bool criticalResult = true;

        // Critical
        if (!CheckNumberOfDataValuesToHeaders(dataRow, headers, rowNumber))
            criticalResult = false;

        return criticalResult;
    }


    // Returns true if the row of data have the same number of values as there is headers.
    public static bool CheckNumberOfDataValuesToHeaders(string[] dataRow, string[] headers, int row)
    {
        if (dataRow.Length == headers.Length)
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message($"Row {row}: Number of data values ({dataRow.Length}) " +
                $"does not match number of headers ({headers.Length}).", 
                Severity.Critical));

            return false;
        }
    }
}
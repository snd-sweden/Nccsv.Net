
using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class DataRowValidator : Validator
{
    

    public DataRowValidator(string[] dataRow, string[] headers, int rowNumber)
    {
        bool result = true;

        //Critical
        if (!CheckNumberOfDataValuesToHeaders(dataRow, headers, rowNumber))
            result = false;

        //Non-critical

        _result = result;
    }

    // Returns true if the row of data have the same number of values as there is headers.
    public static bool CheckNumberOfDataValuesToHeaders(string[] dataRow, string[] headers, int row)
    {
        if (dataRow.Length == headers.Length)
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message($"Row {row}: Number of data values ({dataRow.Length}) does not match number of headers ({headers.Length}).", Severity.Critical));

            return false;
        }
    }


}
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Helpers;

public class NccsvVerifierMethods
{
    // Note: Verifier methods return true if what they check appears to follow the
    // NCCSV documentation.
    // TODO: Methods should probably return true if they find whatever, false if not, for logic & rea


    // Returns true if file has .nccsv extension.
    public static bool CheckNccsvExtension(string filePath)
    {
        if (filePath.EndsWith(".nccsv"))
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("Couldn't find .nccsv extension."));
            return false;
        }
    }


    // Returns true if file is not empty.
    public static bool CheckForContent(string[] lines)
    {
        if (lines.Length == 0)
        {
            MessageRepository.Messages.Add(
                new Message("Couldn't find any file content."));

            return false;
        }
        else
        {
            return true;
        }
    }


    // Returns true if global attributes is found, false if not. // TODO: forgot tests!
    public static bool CheckForGlobalAttributes(List<string[]> separatedLines)
    {
        if (separatedLines[0][0].Equals("*GLOBAL*"))
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("Couldn't find global attributes at first row."));
            return false;
        }
    }


    // Returns true if the *GLOBAL* "Conventions" attribute is first row.
    public static bool CheckGlobalConventions(List<string[]> separatedLines)
    {
        if (separatedLines[0][1].Equals("Conventions"))
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("Couldn't find global \"Conventions\" attribute at first row."));
            return false;
        }
    }


    // Returns true if Global Conventions row include a reference to NCCSV.
    public static bool CheckNccsvVerification(List<string[]> separatedLines)
    {
        if (separatedLines[0][2].Contains("NCCSV"))
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("Couldn't find reference to \"NCCSV\" in global conventions."));
            return false;
        }
    }


    // Returns true if Metadata end tag comes before data end tag.
    // Note: This only checks the order. If *END_METADATA* does not exist, this will
    // still return true.
    public static bool CheckOrderOfEndTags(List<string[]> separatedLines)
    {
        int indexMetaData = 0;
        int indexData = 0;

        foreach (var row in separatedLines)
        {
            if (row[0].Equals("*END_METADATA*"))
                indexMetaData = separatedLines.IndexOf(row);
            else if (row[0].Equals("*END_DATA*"))
                indexData = separatedLines.IndexOf(row);
        }

        if (indexData > indexMetaData)
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("\"*END_METADATA\" and \"*END_DATA*\" isn't in the correct order."));
            return false;
        }
    }


    // Checks for *END_METADATA* that must exist at end of metadata section.
    // Returns true if found.
    // Note: This only checks that *END_METADATA* exists *somewhere* in the file.
    public static bool CheckForMetaDataEndTag(List<string[]> separatedLines)
    {
        foreach (var row in separatedLines)
        {
            if (row[0].Equals("*END_METADATA*"))
                return true;
        }

        MessageRepository.Messages.Add(
            new Message("Couldn't find \"*END_METADATA\"."));

        return false;
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
            new Message("Couldn't find \"*END_DATA*\"."));

        return false;
    }


    // Attributes must have value. Returns true if the variableMetaData list
    // rows has more than 2 columns.
    public static bool CheckAttributesForValue(List<string[]> variableMetaData)
    {
        // Checks if rows are more than 2 columns because values resides on column [2] to [n]
        foreach (var row in variableMetaData)
        {
            if (row.Length > 2)
                continue;
            else
            {
                MessageRepository.Messages.Add(
                new Message("Couldn't find values for all attributes."));

                return false;
            }
        }

        return true;
    }


    // Variable names must not start with a digit. Returns true if name is acceptable.
    public static bool CheckVariableNames(List<string[]> variableMetaData)
    {
        foreach (var row in variableMetaData)
        {
            if (char.IsDigit(row[0][0]))
            {
                MessageRepository.Messages.Add(
                    new Message("Variable names can't start with a digit."));

                return false;
            }
        }

        return true;
    }


    // Attribute names must not start with a digit. Returns true if name is acceptable.
    public static bool CheckAttributeNames(List<string[]> variableMetaData)
    {
        foreach (var row in variableMetaData)
        {
            if (char.IsDigit(row[1][0]))
            {
                MessageRepository.Messages.Add(
                    new Message("Attribute names can't start with a digit."));

                return false;
            }
        }

        return true;
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
            new Message("Not all variables have a data type."));

            return false;
        }
    }


    // Returns true if a given variable have a data type.
    // TODO: Remove? See CheckVariableMetaDataForDataType
    public static bool CheckVariableForDataType(Variable variable)
    {
        if (variable.DataType != null
            && variable.DataType != string.Empty)
            return true;
        else
        {
            MessageRepository.Messages.Add(
            new Message($"Couldn't find data type for {variable.VariableName}."));

            return false;
        }
    }


    // Returns true if scalar variable is valid by checking that it isn't included in the
    // data section as a header.
    public static bool CheckDataForScalarVariable(Variable scalarVariable, List<string[]> data)
    {
        foreach (var header in data[0])
        {
            if (scalarVariable.VariableName == header)
            {
                MessageRepository.Messages.Add(
                    new Message("Scalar variable can't be described in data section."));

                return false;
            }
        }

        return true;
    }


    // Data must be of the data type specified by the metadata.
    // Note: How we check for this depends on how/if we store data types.for
    // TODO : Needs work - continue when we know what to do with data types - or remove if unnecessary
    public static bool CheckDataForDataType(List<string[]> data, List<Variable> variables)
    {
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                var variableType = Type.GetType(variables
                    .Where(v => v.VariableName
                    .Equals(data[0][j]))
                    .First().DataType);
                var dataType = data[i][j].GetType();

                if (variableType == dataType)
                    continue;
                else
                {
                    MessageRepository.Messages.Add(
                        new Message("Something went wrong")); // write better error message

                    return false;
                }
            }
        }

        return true;
    }


    // Returns false if there is whitespace before or after values.
    public static bool CheckDataValuesForSpace(List<string[]> data)
    {
        foreach (var dataRow in data)
        {
            foreach (var value in dataRow)
            {
                if (value.EndsWith(" ")
                    || value.StartsWith(" "))
                {
                    MessageRepository.Messages.Add(
                        new Message("Data values can't start with or end with whitespace."));

                    return false;
                }
            }
        }

        return true;
    }


    // Returns true if the row of data have the same number of values as
    // the list of variable names.
    public static bool CheckNumberOfDataValuesToVariables(List<DataValue[]> data, List<Variable> variables)
    {
        var nonScalarVariables = variables.Where(v => v.Scalar == false).ToList();

        // Numbers of data values in a row should be same as number of variables that is not scalar
        if (data[0].Length == nonScalarVariables.Count)
            return true;
        else
            return false;
    }
}

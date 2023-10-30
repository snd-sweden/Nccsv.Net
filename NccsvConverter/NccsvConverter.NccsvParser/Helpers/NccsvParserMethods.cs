using NccsvConverter.NccsvParser.Validators;

namespace NccsvConverter.NccsvParser.Helpers;

public class NccsvParserMethods
{
    // Splits a given line at "," but not if it's within a string.
    // Returns the split line as a list of strings.
    internal static List<string> Separate(string line, int row)
    {
        List<string> separatedLine = new List<string>();
        string tempString = string.Empty;
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '\"')
            {
                inQuotes = !inQuotes;
            }

            else if (line[i] == ',')
            {
                // Ignore commas within a string
                if (!inQuotes)
                {
                    ValueValidator.Validate(tempString, row);
                    separatedLine.Add(tempString.Trim().Trim('"'));
                    tempString = string.Empty;
                    continue;
                }
            }

            tempString += line[i];
        }

        ValueValidator.Validate(tempString, row);
        separatedLine.Add(tempString.Trim().Trim('"'));

        return separatedLine;
    }
}


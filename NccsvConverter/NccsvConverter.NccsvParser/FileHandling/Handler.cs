using NccsvConverter.NccsvParser.Helpers;

namespace NccsvConverter.NccsvParser.FileHandling;

public class Handler
{
    // Gets a .nccsv file and converts it into a array of strings for further handling.
    public static List<string[]> NccsvFileReader(string filePath)
    {
        var separatedNccsv = new List<string[]>();
        var lines = File.ReadAllLines(filePath); // TODO: handle exceptions

        // TODO: move following code into separate method, issue #68
        if (Verifier.VerifyLines(lines))
        {
            string[] separatedLine;

            foreach (var line in lines) 
            {
                if (line != string.Empty)
                {
                    separatedLine = NccsvParserMethods.Separate(line, Array.IndexOf(lines, line) + 1).ToArray();
                    separatedNccsv.Add(separatedLine);
                }
            }

            return separatedNccsv;
        }
        else
            return null;

    }
}

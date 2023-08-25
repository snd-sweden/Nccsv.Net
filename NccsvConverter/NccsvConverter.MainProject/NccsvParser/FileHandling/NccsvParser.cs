namespace NccsvConverter.MainProject.NccsvParser.FileHandling;

public class NccsvParser
{
    public List<string[]> FromText(string fileName)
    {
        var csv = new List<string[]>();

        var lines = System.IO.File.ReadAllLines(fileName);

        foreach (var line in lines)
            csv.Add(line.Split(','));

        return csv;
    }
}
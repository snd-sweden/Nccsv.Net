namespace NccsvConverter.NccsvParser.Models;

public class DataSet
{
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public Dictionary<string, string> GlobalAttributes { get; set; } = new();
    public List<Variable> Variables { get; set; } = new();
    public List<DataValue[]> Data { get; set; } = new();
}
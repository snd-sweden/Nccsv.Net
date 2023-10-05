namespace NccsvConverter.NccsvParser.Models;

public enum Severity
{
    Essential,
    Recommended
}

public class Message
{
    public string Text { get; set; } = string.Empty;
    public Severity Severity { get; set; }

    public Message(string text)
    {
        Text = text;
        Severity = Severity.Essential;
    }

    public Message(string text, Severity severity)
    {
        Text = text;
        Severity = severity;
    }
}
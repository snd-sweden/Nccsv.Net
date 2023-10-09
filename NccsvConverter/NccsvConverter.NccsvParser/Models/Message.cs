using System.Diagnostics.Contracts;

namespace NccsvConverter.NccsvParser.Models;

public enum Severity
{
    NonCritical,
    Critical,
    Recommended
}

public class Message
{
    public string Text { get; set; } = string.Empty;
    public Severity Severity { get; set; }


    public Message(string text, Severity severity)
    {
        if (severity == Severity.Critical)
        {
            Text = "[Failed to parse further] " + text;
            Severity = Severity.Critical;
        }
        else if (severity == Severity.NonCritical)
        {
            Text = "[Warning] " + text;
            Severity = Severity.NonCritical;
        }
        else if (severity == Severity.Recommended)
        {
            Text = "[Recommendation] " + text;
            Severity = Severity.Recommended;
        }
    }

}
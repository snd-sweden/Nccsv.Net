namespace NccsvConverter.NccsvParser.Models;

public enum Severity
{
    NonCritical,
    Critical,
    Recommended
}

public class Message
{
    private string _text = string.Empty;
    public string Text
    {
        get { return _text; }
    }
    private Severity _severity;
    public Severity Severity
    {
        get { return _severity; }
    }


    public Message(string text, Severity severity)
    {
        if (severity == Severity.Critical)
        {
            _text = "[Failed to parse further] " + text;
            _severity = Severity.Critical;
        }
        else if (severity == Severity.NonCritical)
        {
            _text = "[Warning] " + text;
            _severity = Severity.NonCritical;
        }
        else if (severity == Severity.Recommended)
        {
            _text = "[Recommendation] " + text;
            _severity = Severity.Recommended;
        }
    }
}
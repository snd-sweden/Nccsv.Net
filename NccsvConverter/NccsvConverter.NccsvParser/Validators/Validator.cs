namespace NccsvConverter.NccsvParser.Validators;

public abstract class Validator
{
    private bool _result;

    public bool Result
    {
        get => _result;
        private set => _result = value;
    }
}
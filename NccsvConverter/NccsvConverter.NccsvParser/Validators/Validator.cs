namespace NccsvConverter.NccsvParser.Validators;

public abstract class Validator
{
    protected bool _result;

    public bool Result
    {
        get => _result;
        private set => _result = value;
    }
}
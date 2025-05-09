namespace Lab5;

public class MissingWeightException : Exception
{
    public MissingWeightException() : base("Weight is missing.")
    {
    }

    public MissingWeightException(string message) : base(message)
    {
    }

    public MissingWeightException(string message, Exception inner) : base(message, inner)
    {
    }
}
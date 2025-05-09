namespace Lab5;

public class NegativeWeightException : Exception
{
    public NegativeWeightException() : base("The weight cannot be negative.")
    {
    }

    public NegativeWeightException(string message) : base(message)
    {
    }

    public NegativeWeightException(string message, Exception inner) : base(message, inner)
    {
    }
}
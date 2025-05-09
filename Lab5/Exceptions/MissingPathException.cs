namespace Lab5;

public class MissingPathException : Exception
{
    public MissingPathException() : base("There is no path between selected nodes.")
    {
    }

    public MissingPathException(string message) : base(message)
    {
    }

    public MissingPathException(string message, Exception inner) : base(message, inner)
    {
    }
}
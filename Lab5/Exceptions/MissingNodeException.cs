namespace Lab5;

public class MissingNodeException : Exception
{
    public MissingNodeException() : base("The node youre trying to connect to is missing.")
    {
    }

    public MissingNodeException(string message) : base(message)
    {
    }

    public MissingNodeException(string message, Exception inner) : base(message, inner)
    {
    }
}
namespace Lab5;

public class SelfNeighborException : Exception
{
    public SelfNeighborException() : base("A node cannot be its own neighbor.")
    {
    }

    public SelfNeighborException(string message) : base(message)
    {
    }

    public SelfNeighborException(string message, Exception inner) : base(message, inner)
    {
    }
}
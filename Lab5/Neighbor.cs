namespace Lab5;

public class Neighbor : IVertex
{
    private Node Node { get; set; }
    public VertexState State { get { return Node.State; } set { Node.State = value; } }
    public string Name { get { return Node.Name; } set { Node.Name = value; } }
    public List<Neighbor> Neighbors { get { return Node.Neighbors; } set { Node.Neighbors = value; } }
    public int Weight { get; set; }

    public Neighbor(Node node, int weight = 1)
    {
        Node = node;
        Weight = weight;
    }

    public Node AsNode()
    {
        return Node;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Neighbor neighbor && Name == neighbor.Name;
    }

    int IComparable<IVertex>.CompareTo(IVertex? other)
    {
        return this.Name.CompareTo(other?.Name);
    }
}
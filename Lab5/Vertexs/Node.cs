
namespace Lab5;

public class Node : IVertex
{
	public string Name { get; set; }
	public List<Neighbor> Neighbors { get; set; }
	public VertexState State { get; set; }

	public Node(string name, VertexState state = VertexState.UnDiscovered)
	{
		Name = name;
		State = state;
		Neighbors = new List<Neighbor>();
	}

	public bool IsVisited => State == VertexState.Visited;

	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	public override bool Equals(object? obj)
	{
		return obj is Node node && Name == node.Name;
	}

	int IComparable<IVertex>.CompareTo(IVertex? other)
	{
		return this.Name.CompareTo(other?.Name);
	}
}

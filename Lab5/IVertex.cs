namespace Lab5;

interface IVertex : IComparable<IVertex>
{
    string Name { get; set; }
    List<Neighbor> Neighbors { get; set; }
    VertexState State { get; set; }
    int GetHashCode();
    bool Equals(object? obj);
}
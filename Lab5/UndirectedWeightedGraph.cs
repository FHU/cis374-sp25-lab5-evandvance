using System.Text.RegularExpressions;

namespace Lab5;

public class UndirectedWeightedGraph
{
    public List<Node> Nodes { get; set; }

    public UndirectedWeightedGraph()
    {
        Nodes = new List<Node>();
    }

    public UndirectedWeightedGraph(string path)
    {
        Nodes = new List<Node>();

        List<string> lines = new List<string>();

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()!) is not null)
                {
                    line = line.Trim();
                    if (line == "")
                    {
                        continue;
                    }
                    if (line[0] == '#')
                    {
                        continue;
                    }

                    lines.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        // process the lines
        if (lines.Count < 1)
        {
            // empty file
            Console.WriteLine("Graph file was empty");
            return;
        }

        // Add nodes
        string[] nodeNames = Regex.Split(lines[0], @"\W+");

        foreach (var name in nodeNames)
        {
            Nodes.Add(new Node(name));
        }

        string[] nodeNamesAndWeight;
        // Add edges
        for (int i = 1; i < lines.Count; i++)
        {
            // extract node names
            nodeNamesAndWeight = Regex.Split(lines[i], @"\W+");
            if (nodeNamesAndWeight.Length < 3)
            {
                throw new Exception("Two nodes and a weight are required for each edge.");
            }

            // add edge between those nodes
            AddEdge(nodeNamesAndWeight[0], nodeNamesAndWeight[1], int.Parse(nodeNamesAndWeight[2]));
        }
    }

    public void AddEdge(string node1Name, string node2Name, int weight)
    {
        Node? node1 = GetNodeByName(node1Name);
        Node? node2 = GetNodeByName(node2Name);

        if (node1 is null || node2 is null)
        {
            throw new Exception("Invalid node name");
        }

        node1.Neighbors.Add(new Neighbor(node2, weight));
        node2.Neighbors.Add(new Neighbor(node1, weight));
    }

    public Node? GetNodeByName(string nodeName) => Nodes.Find(node => node.Name == nodeName);

    public int ConnectedComponents
    {
        get
        {
            int numConnectedComponents = 0;

            // choose a random vertex
            Node startingNode = Nodes[0];
            // do a DFS from that vertex
            // Leave reset to true to initialize state
            DFS(startingNode, reset: true);
            // increment the CC count
            numConnectedComponents++;

            foreach (Node node in Nodes[1..])
            {
                if (node.State == VertexState.UnDiscovered)
                {
                    DFS(node, reset: false);
                    numConnectedComponents++;
                }
            }

            return numConnectedComponents;
        }
    }


    public bool IsReachable(string node1name, string node2name)
    {
        Node? node1 = GetNodeByName(node1name);
        Node? node2 = GetNodeByName(node2name);

        if (node1 is null || node2 is null)
        {
            throw new Exception($"{node1name} or {node2name} does not exist.)");
        }

        // Do a DFS
        var pred = DFS(node1);

        // Was a pred for node2 found?
        return pred[node2] is not null;
    }


    /// <summary>
    /// Searches the graph in a depth-first manner, creating a
    /// dictionary of the Node to Predessor Node links discovered by starting at the given node.
    /// Neighbors are visited in alphabetical order. 
    /// </summary>
    /// <param name="startingNode">The starting node of the depth first search</param>
    /// <returns>A dictionary of the Node to Predecessor Node 
    /// for each node in the graph reachable from the starting node
    /// as discovered by a DFS.</returns>
    public Dictionary<Node, Node?> DFS(Node startingNode, bool reset = true)
    {
        Dictionary<Node, Node?> pred = new Dictionary<Node, Node?>();

        if (reset)
        {
            // setup DFS
            foreach (Node node in Nodes)
            {
                pred[node] = null;
                node.State = VertexState.UnDiscovered;
            }
        }

        // call the recursive method
        DFSVisit(startingNode, pred);

        return pred;
    }

    /// <summary>
    /// Find the first path between the given nodes in a DFS manner 
    /// and return its total cost. Choices/ties are made in alphabetical order. 
    /// </summary>
    /// <param name="node1name">The starting node's name</param>
    /// <param name="node2name">The ending node's name</param>
    /// <param name="pathList">A list of the nodes in the path from the starting node to the ending node</param>
    /// <returns>The total cost of the weights in the path</returns>
    public int DFSPathBetween(string node1name, string node2name, out List<Node> pathList)
    {
        // 1. initilize all the things 
        pathList = new List<Node>();

        Node? node1 = GetNodeByName(node1name);
        Node? node2 = GetNodeByName(node2name);

        if (node1 is null || node2 is null)
        {
            throw new Exception($"{node1name} or {node2name} does not exist.)");
        }

        var pred = DFS(node1);

        // 3. Post-process the data structures and convert them to the right format.

        int sumOfAllWeights = 0;

        Node? currentNode = node2;

        while (currentNode is not null)
        {
            sumOfAllWeights += pred[currentNode] is not null ? pred[currentNode]!.Neighbors.Find(x => x.AsNode() == currentNode)!.Weight : 0;
            pathList.Add(currentNode);
            currentNode = pred[currentNode];
        }

        pathList.Reverse(); // Reverse path list to make it in node1 -> node2 order

        return sumOfAllWeights;
    }

    private void DFSVisit(Node node, Dictionary<Node, Node?> pred)
    {
        // color node gray
        node.State = VertexState.Discovered;

        // sort the neighbors so that we visit them in alpha order
        node.Neighbors.Sort();

        // visit every neighbor 
        foreach (var neighbor in node.Neighbors)
        {
            if (neighbor.State == VertexState.UnDiscovered)
            {
                pred[neighbor.AsNode()] = node;
                DFSVisit(neighbor.AsNode(), pred);
            }
        }

        // color the node black
        node.State = VertexState.Visited;
    }

    /// <summary>
    /// Searches the graph in a breadth-first manner, creating a
    /// dictionary of the Node to Predecessor and Distance discovered by starting at the given node.
    /// Neighbors are visited in alphabetical order. 
    /// </summary>
    /// <param name="startingNode"></param>
    /// <returns>A dictionary of the Node to Predecessor Node and Distance 
    /// for each node in the graph reachable from the starting node
    /// as discovered by a BFS.</returns>
    public Dictionary<Node, (Node? pred, int dist)> BFS(Node startingNode)
    {
        Dictionary<Node, (Node? pred, int dist)> resultsDictionary = new Dictionary<Node, (Node? pred, int dist)>();

        // initialize the dictionary 
        foreach (Node node in Nodes)
        {
            node.State = VertexState.UnDiscovered;
            resultsDictionary[node] = (null, int.MaxValue);
        }

        // setting up the starting node
        startingNode.State = VertexState.Discovered;
        resultsDictionary[startingNode] = (null, 0);

        // create a queue
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(startingNode);

        // iteratively process the graph (neighbors of the nodes in the queue)
        while (queue.Count > 0)
        {
            // get the front of queue 
            var node = queue.Dequeue();

            // sort the neighbors so that we visit them in alpha order
            node.Neighbors.Sort();

            foreach (var neighbor in node.Neighbors)
            {
                if (neighbor.State == VertexState.UnDiscovered)
                {
                    neighbor.State = VertexState.Discovered;
                    int distance = resultsDictionary[node].dist;
                    resultsDictionary[neighbor.AsNode()] = (node, distance + 1);
                    queue.Enqueue(neighbor.AsNode());
                }
            }

            node.State = VertexState.Visited;
        }

        return resultsDictionary;
    }


    // TODO
    /// <summary>
    /// Find the first path between the given nodes in a BFS manner 
    /// and return its total cost. Choices/ties are made in alphabetical order. 
    /// </summary>
    /// <param name="node1name">The starting node's name</param>
    /// <param name="node2name">The ending node's name</param>
    /// <param name="pathList">A list of the nodes in the path from the starting node to the ending node</param>
    /// <returns>The total cost of the weights in the path</returns>
    public int BFSPathBetween(string node1, string node2, out List<Node> pathList)
    {
        pathList = new List<Node>();


        return 0;
    }


    // TODO
    public Dictionary<Node, (Node? pred, int cost)> Dijkstra(Node startingNode)
    {
        // PriorityQueue<Neighbor, int> priorityQueue = new PriorityQueue<Neighbor, int>();
        // var neightbor = new Neighbor(){ Node= new Node(), Weight= 4};

        // priorityQueue.Enqueue( neightbor, neightbor.Weight + currentCost);

        // HashSet<Node> visited = new HashSet<Node>();    


        return null;
    }

    // TODO
    /// <summary>
    /// Find the first path between the given nodes using Dijkstra's algorithm
    /// and return its total cost. Choices/ties are made in alphabetical order. 
    /// </summary>
    /// <param name="node1name">The starting node name</param>
    /// <param name="node2name">The ending node name</param>
    /// <param name="pathList">A list of the nodes in the path from the starting node to the ending node</param>
    /// <returns>The total cost of the weights in the path</returns>
    public int DijkstraPathBetween(string node1, string node2, out List<Node> pathList)
    {
        pathList = new List<Node>();

        return 0;
    }

}

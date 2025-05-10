using Lab5;

namespace UnitTests;
[TestClass]
public class UndirectedWeightedGraphTests
{
    [TestMethod]
    public void TestGraphConstructorHandlesSelfEdges()
    {
        Assert.ThrowsException<SelfNeighborException>(() => new UndirectedWeightedGraph("../../../graphs/GraphWithSelfEdge.txt"));
    }

    [TestMethod]
    public void TestGraphConstructorHandlesNegativeWeights()
    {
        Assert.ThrowsException<NegativeWeightException>(() =>
        {
            var undirectedGraph = new UndirectedWeightedGraph("../../../graphs/GraphWithNegativeWeight.txt");
            undirectedGraph.AddEdge("a", "b", -1);
        });
    }

    [TestMethod]
    public void TestGraphConstructorHandlesNonExistentEdgeWeights()
    {
        Assert.ThrowsException<MissingWeightException>(() => new UndirectedWeightedGraph("../../../graphs/GraphMissingWeight.txt"));
    }

    [TestMethod]
    public void TestGraphConstructorHandlesNonExistentNodes()
    {
        Assert.ThrowsException<MissingNodeException>(() => new UndirectedWeightedGraph("../../../graphs/GraphMissingNodeInEdgeList.txt"));
    }


    [TestMethod]
    public void Graph1IsReachable()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("d", "e"));
        Assert.IsTrue(undirectedGraph.IsReachable("d", "c"));
    }

    [TestMethod]
    public void Graph1ConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        Assert.AreEqual(1, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void Graph2IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Assert.IsFalse(undirectedGraph.IsReachable("a", "c"));
        Assert.IsFalse(undirectedGraph.IsReachable("e", "c"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "e"));
        Assert.IsFalse(undirectedGraph.IsReachable("b", "a"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "b"));

    }

    [TestMethod]
    public void Graph2ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Assert.AreEqual(5, undirectedGraph.ConnectedComponents);
    }


    [TestMethod]
    public void Graph3IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph3.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "d"));
        Assert.IsTrue(undirectedGraph.IsReachable("h", "g"));

        Assert.IsFalse(undirectedGraph.IsReachable("a", "h"));
        Assert.IsFalse(undirectedGraph.IsReachable("c", "i"));
        Assert.IsFalse(undirectedGraph.IsReachable("g", "b"));

    }

    [TestMethod]
    public void Graph3ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph3.txt");

        Assert.AreEqual(3, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void Graph4IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "i"));
        Assert.IsTrue(undirectedGraph.IsReachable("g", "b"));
        Assert.IsTrue(undirectedGraph.IsReachable("c", "f"));
        Assert.IsTrue(undirectedGraph.IsReachable("a", "d"));
        Assert.IsTrue(undirectedGraph.IsReachable("b", "i"));

    }

    [TestMethod]
    public void Graph4ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Assert.AreEqual(1, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void SavannahIsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/Savannah.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "i"));
        Assert.IsTrue(undirectedGraph.IsReachable("g", "b"));
        Assert.IsTrue(undirectedGraph.IsReachable("c", "f"));
        Assert.IsTrue(undirectedGraph.IsReachable("a", "j"));
        Assert.IsTrue(undirectedGraph.IsReachable("b", "i"));


        Assert.IsFalse(undirectedGraph.IsReachable("a", "d"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "j"));

    }

    [TestMethod]
    public void SavannahConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/Savannah.txt");

        Assert.AreEqual(2, undirectedGraph.ConnectedComponents);
    }


    [TestMethod]
    public void TestDFSHubAndSpokeGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.DFS(startingNode);

        var expectedResults = new Dictionary<string, string?>
        {
            { "a", null },
            { "b", "a" },
            { "c", "b" },
            { "d", "b" },
            { "e", "b" }
        };

        EvaluateDFSResults(result, expectedResults);
    }

    [TestMethod]
    public void TestDFSNoConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.DFS(startingNode);

        var expectedResults = new Dictionary<string, string?>
        {
            { "a", null },
            { "b", null },
            { "c", null },
            { "d", null },
            { "e", null }
        };
        EvaluateDFSResults(result, expectedResults);
    }

    [TestMethod]
    public void TestDFSThreeConnectedComponentsGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.DFS(startingNode);

        var expectedResults = new Dictionary<string, string?>
        {
            { "a", null },
            { "b", "a" },
            { "c", "b" },
            { "d", "b" },
            { "e", "b" },
            { "f", null },
            { "g", null },
            { "h", null },
            { "i", null }
        };

        EvaluateDFSResults(result, expectedResults);
    }

    [TestMethod]
    public void TestDFSChatGPTGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/ChatGPTGraph.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.DFS(startingNode);

        var expectedResults = new Dictionary<string, string?>
        {
            { "a", null },
            { "b", "a" },
            { "c", "b" },
            { "d", "e" },
            { "e", "c" },
            { "f", "d" },
            { "g", "f" }
        };
        EvaluateDFSResults(result, expectedResults);
    }

    private void EvaluateDFSResults(
        Dictionary<Node, Node?> result,
        Dictionary<string, string?> expectedResults
    )
    {
        Assert.IsTrue(result.Count == expectedResults.Count);
        foreach (var node in result.Keys)
        {
            Assert.IsTrue(result[node]?.Name == expectedResults[node.Name]);
        }
    }

    [TestMethod]
    public void TestDFSPathBetweenHubAndSpokeGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var (cost, pathList) = undirectedGraph.DFSPathBetween("a", "c");

        string[] expectedPath = { "a", "b", "c" };

        Assert.IsTrue(cost == 7);
        EvaluatePathBetweenResults(pathList, expectedPath);
    }

    [TestMethod]
    public void TestDFSPathBetweenNoConnectedComponentsGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        Assert.ThrowsException<MissingPathException>(() => undirectedGraph.DFSPathBetween("a", "c"));
    }

    [TestMethod]
    public void TestDFSPathBetweenThreeConnectedComponentsGraph()
    {
        // This is kinda the same test as the first DFSPathBetween test, but with different weights and nodes
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var (cost, pathList) = undirectedGraph.DFSPathBetween("a", "c");
        string[] expectedPath = { "a", "b", "c" };

        Assert.IsTrue(cost == 13);
        EvaluatePathBetweenResults(pathList, expectedPath);
    }

    [TestMethod]
    public void TestDFSPathBetweenChatGPTGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/ChatGPTGraph.txt");

        var (cost, pathList) = undirectedGraph.DFSPathBetween("c", "f");
        string[] expectedPath = { "c", "a", "b", "d", "e", "g", "f" };

        Assert.IsTrue(cost == 13);
        EvaluatePathBetweenResults(pathList, expectedPath);

        // C -> F is poor in DFS of this graph because DFS visits in alpha order to be deterministic
    }

    [TestMethod]
    public void TestBFSHubAndSpokeGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.BFS(startingNode);

        var expectedResults = new Dictionary<string, (string? predName, int cost)>
        {
            { "a", ( null, 0 ) },
            { "b", ( "a", 1 ) },
            { "c", ( "b", 2 ) },
            { "d", ( "b", 2 ) },
            { "e", ( "b", 2 ) }
        };

        EvaluateBFSResults(result, expectedResults);
    }

    [TestMethod]
    public void TestBFSNoConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.BFS(startingNode);

        var expectedResults = new Dictionary<string, (string? predName, int hops)>
        {
            { "a", ( null, 0 ) },
            { "b", ( null, int.MaxValue ) },
            { "c", ( null, int.MaxValue ) },
            { "d", ( null, int.MaxValue ) },
            { "e", ( null, int.MaxValue ) }
        };
        EvaluateBFSResults(result, expectedResults);
    }

    [TestMethod]
    public void TestBFSThreeConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.BFS(startingNode);

        var expectedResults = new Dictionary<string, (string? predName, int hops)>
        {
            { "a", ( null, 0 ) },
            { "b", ( "a", 1 ) },
            { "c", ( "b", 2 ) },
            { "d", ( "b", 2 ) },
            { "e", ( "b", 2 ) },
            { "f", ( null, int.MaxValue ) },
            { "g", ( null, int.MaxValue ) },
            { "h", ( null, int.MaxValue ) },
            { "i", ( null, int.MaxValue ) },
        };

        EvaluateBFSResults(result, expectedResults);
    }

    [TestMethod]
    public void TestBFSChatGPTGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/ChatGPTGraph.txt");

        var startingNode = undirectedGraph.GetNodeByName("c") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.BFS(startingNode);

        var expectedResults = new Dictionary<string, (string? predName, int hops)>
        {
            { "c", ( null, 0 ) },
            { "a", ( "c", 1 ) },
            { "b", ( "c", 1 ) },
            { "d", ( "b", 2 ) },
            { "e", ( "c", 1 ) },
            { "f", ( "d", 3 ) },
            { "g", ( "b", 2 ) }
        };

        EvaluateBFSResults(result, expectedResults);
    }

    private void EvaluateBFSResults(
        Dictionary<Node, (Node? pred, int dist)> result,
        Dictionary<string, (string? predName, int hops)> expectedResults
    )
    {
        Assert.IsTrue(result.Count == expectedResults.Count);
        foreach (var node in result.Keys)
        {
            Assert.IsTrue(result[node].dist == expectedResults[node.Name].hops);
            if (expectedResults[node.Name].predName != null)
            {
                Assert.IsTrue(result[node].pred?.Name == expectedResults[node.Name].predName);
            }
            else
            {
                Assert.IsNull(result[node].pred);
            }
        }
    }

    [TestMethod]
    public void TestBFSPathBetweenHubAndSpokeGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var (hopCount, pathList) = undirectedGraph.BFSPathBetween("a", "c");
        string[] expectedPath = { "a", "b", "c" };

        Assert.IsTrue(hopCount == 2);
        EvaluatePathBetweenResults(pathList, expectedPath);
    }

    [TestMethod]
    public void TestBFSPathBetweenNoConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        Assert.ThrowsException<MissingPathException>(() => undirectedGraph.BFSPathBetween("a", "c"));
    }

    [TestMethod]
    public void TestBFSPathBetweenConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var (hopCount, pathList) = undirectedGraph.BFSPathBetween("a", "c");

        string[] expectedPath = { "a", "b", "c" };

        Assert.IsTrue(hopCount == 2);
        EvaluatePathBetweenResults(pathList, expectedPath);
    }

    [TestMethod]
    public void TestBFSPathBetweenChatGPTGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/ChatGPTGraph.txt");

        var (hopCount, pathList) = undirectedGraph.BFSPathBetween("c", "f");

        Assert.IsTrue(hopCount == 3);

        string[] expectedPath = { "c", "b", "d", "f" };

        EvaluatePathBetweenResults(pathList, expectedPath);
    }

    [TestMethod]
    public void TestDjikstrasHubAndSpokeGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.Dijkstra(startingNode);

        var expectedResults = new Dictionary<string, (string? predName, int cost)>
        {
            { "a", ( null, 0 ) },
            { "b", ( "a", 2 ) },
            { "c", ( "b", 7 ) },
            { "d", ( "b", 3 ) },
            { "e", ( "b", 5 ) }
        };

        EvaluateDjikstrasResults(result, expectedResults);
    }

    [TestMethod]
    public void TestDjikstrasNoConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.Dijkstra(startingNode);

        var expectedResults = new Dictionary<string, (string? predName, int cost)>
        {
            { "a", ( null, 0 ) },
            { "b", ( null, int.MaxValue ) },
            { "c", ( null, int.MaxValue ) },
            { "d", ( null, int.MaxValue ) },
            { "e", ( null, int.MaxValue ) }
        };

        EvaluateDjikstrasResults(result, expectedResults);
    }

    [TestMethod]
    public void TestDjikstrasThreeConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.Dijkstra(startingNode);

        var expectedResults = new Dictionary<string, (string? predName, int cost)>
        {
            { "a", ( null, 0 ) },
            { "b", ( "a", 7 ) },
            { "c", ( "b", 13 ) },
            { "d", ( "b", 15 ) },
            { "e", ( "b", 11 ) },
            { "f", ( null, int.MaxValue ) },
            { "g", ( null, int.MaxValue ) },
            { "h", ( null, int.MaxValue ) },
            { "i", ( null, int.MaxValue ) },
        };

        EvaluateDjikstrasResults(result, expectedResults);
    }

    [TestMethod]
    public void TestDjikstraChatGPTGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/ChatGPTGraph.txt");

        var startingNode = undirectedGraph.GetNodeByName("c") ?? throw new MissingNodeException("Node a not found");

        var result = undirectedGraph.Dijkstra(startingNode);

        var expectedResults = new Dictionary<string, (string? predName, int cost)>
        {
            { "c", ( null, 0 ) },
            { "a", ( "b", 3 ) },
            { "b", ( "c", 1 ) },
            { "d", ( "b", 3 ) },
            { "e", ( "c", 3 ) },
            { "f", ( "g", 6 ) },
            { "g", ( "e", 5 ) }
        };

        EvaluateDjikstrasResults(result, expectedResults);
    }


    private void EvaluateDjikstrasResults(
        Dictionary<Node, (Node? pred, int cost)> result,
        Dictionary<string, (string? predName, int cost)> expectedResults
    )
    {
        foreach (var node in result.Keys)
        {
            Assert.IsTrue(result[node].cost == expectedResults[node.Name].cost);
            if (expectedResults[node.Name].predName != null)
            {
                Assert.IsTrue(result[node].pred?.Name == expectedResults[node.Name].predName);
            }
            else
            {
                Assert.IsNull(result[node].pred);
            }
        }
    }

    [TestMethod]
    public void TestDijkstraPathBetweenHubAndSpokeGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var (cost, pathList) = undirectedGraph.DijkstraPathBetween("a", "c");

        string[] expectedPath = { "a", "b", "c" };

        Assert.IsTrue(cost == 7);
        EvaluatePathBetweenResults(pathList, expectedPath);
    }

    [TestMethod]
    public void TestDjikstraDijkstraPathBetweenNoConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        Assert.ThrowsException<MissingPathException>(() => undirectedGraph.DijkstraPathBetween("a", "c"));
    }

    [TestMethod]
    public void TestDjikstraDijkstraPathBetweenThreeConnectedComponents()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var (cost, pathList) = undirectedGraph.DijkstraPathBetween("a", "c");

        string[] expectedPath = { "a", "b", "c" };

        Assert.IsTrue(cost == 13);
        EvaluatePathBetweenResults(pathList, expectedPath);
    }

    [TestMethod]
    public void TestDijkstraPathBetweenChatGPTGraph()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/ChatGPTGraph.txt");

        var (cost, pathList) = undirectedGraph.DijkstraPathBetween("c", "f");

        string[] expectedPath = { "c", "e", "g", "f" };

        Assert.IsTrue(cost == 6);
        EvaluatePathBetweenResults(pathList, expectedPath);
    }

    private void EvaluatePathBetweenResults(
        List<Node> pathList,
        string[] expectedPath
    )
    {
        Assert.IsTrue(pathList.Count == expectedPath.Length);
        for (int i = 0; i < expectedPath.Length; i++)
        {
            Assert.IsTrue(pathList[i].Name == expectedPath[i]);
        }
    }

}
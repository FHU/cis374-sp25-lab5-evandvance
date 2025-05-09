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
        Assert.ThrowsException<NegativeWeightException>(() => new UndirectedWeightedGraph("../../../graphs/GraphWithNegativeWeight.txt"));
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
    public void TestDFSGraph1()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.DFS(startingNode);

        //TODO Test results of dfs

    }

    [TestMethod]
    public void TestDFSGraph2()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.DFS(startingNode);

        //TODO Test results of dfs

    }

    [TestMethod]
    public void TestDFSGraph3()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.DFS(startingNode);

        //TODO Test results of dfs

    }

    [TestMethod]
    public void TestDFSPathBetweenGraph1()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var (cost, pathList) = undirectedGraph.DFSPathBetween("a", "c");

        //TODO Test results of dfs

    }

    [TestMethod]
    public void TestDFSPathBetweenGraph2()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var (cost, pathList) = undirectedGraph.DFSPathBetween("a", "c");

        //TODO Test results of dfs

    }

    [TestMethod]
    public void TestDFSPathBetweenGraph3()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var (cost, pathList) = undirectedGraph.DFSPathBetween("a", "c");

        //TODO Test results of dfs

    }

    [TestMethod]
    public void TestBFSGraph1()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.BFS(startingNode);

        //TODO Test results of bfs

    }

    [TestMethod]
    public void TestBFSGraph2()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.BFS(startingNode);

        //TODO Test results of bfs

    }

    [TestMethod]
    public void TestBFSGraph3()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.BFS(startingNode);

        //TODO Test results of bfs

    }

    [TestMethod]
    public void TestBFSPathBetweenGraph1()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var (cost, pathList) = undirectedGraph.BFSPathBetween("a", "c");

        //TODO Test results of bfs

    }

    [TestMethod]
    public void TestBFSPathBetweenGraph2()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var (cost, pathList) = undirectedGraph.BFSPathBetween("a", "c");

        //TODO Test results of bfs

    }

    [TestMethod]
    public void TestBFSPathBetweenGraph3()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var (cost, pathList) = undirectedGraph.BFSPathBetween("a", "c");

        //TODO Test results of bfs

    }


    [TestMethod]
    public void TestDjikstrasGraph1()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.Dijkstra(startingNode);

        //TODO Test results of djikstra

    }

    [TestMethod]
    public void TestDjikstrasGraph2()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.Dijkstra(startingNode);

        //TODO Test results of djikstra

    }

    [TestMethod]
    public void TestDjikstrasGraph3()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var startingNode = undirectedGraph.GetNodeByName("a") ?? throw new Exception("Node a not found");

        var result = undirectedGraph.Dijkstra(startingNode);

        //TODO Test results of djikstra

    }

    [TestMethod]
    public void TestDjikstraDijkstraPathBetweenGraph1()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph1.txt");

        var (cost, pathList) = undirectedGraph.DijkstraPathBetween("a", "c");

        //TODO Test results of djikstra

    }

    [TestMethod]
    public void TestDjikstraDijkstraPathBetweenGraph2()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph2.txt");

        var (cost, pathList) = undirectedGraph.DijkstraPathBetween("a", "c");

        //TODO Test results of djikstra

    }

    [TestMethod]
    public void TestDjikstraDijkstraPathBetweenGraph3()
    {
        UndirectedWeightedGraph undirectedGraph = new UndirectedWeightedGraph("../../../graphs/graph3.txt");

        var (cost, pathList) = undirectedGraph.DijkstraPathBetween("a", "c");

        //TODO Test results of djikstra

    }

}
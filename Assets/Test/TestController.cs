using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public static GameObject[] Nodes;
    public List<int[]> Weights = new List<int[]>();

    void Awake()
    {
        Nodes = GameObject.FindGameObjectsWithTag("Node");
        int NumberOfNodes = Nodes.Length;
        Debug.Log("Number of Nodes found: " + NumberOfNodes);

        for (int i = 0; i < NumberOfNodes; i++)
        {
            int[] tempWeights = new int[NumberOfNodes];
            GameObject currentNode = Nodes[i];
            TestNode currentScript = currentNode.GetComponent<TestNode>();
            currentScript.ID = i;

            for (int j = 0; j < NumberOfNodes; j++)
            {
                GameObject testNode = Nodes[j];
                // If the node is the same as the currentNode
                if (currentNode == testNode)
                {
                    tempWeights[j] = 0;
                }

                // If the node is not part of the connectNodes in the currentNode, skip
                if (currentScript.ConnectedNodes.IndexOf(testNode) == -1)
                {
                    tempWeights[j] = 0;
                    continue;
                }

                //If the node is part of the connectedNodes, calculate the weight
                float distance = Vector3.Distance(currentNode.transform.position, testNode.transform.position);
                int absoluteDistance = Mathf.FloorToInt(Mathf.Abs(distance));
                tempWeights[j] = absoluteDistance;
            }

            Weights.Add(tempWeights);
        }

        for (int i = 0; i < NumberOfNodes; i++)
        {
            for (int j = 0; j < NumberOfNodes; j++)
            {
                if (Weights[i][j] != 0)
                {
                    Weights[j][i] = Weights[i][j];
                }
            }
        }

        string arrayString = "";
        for (int i = 0; i < NumberOfNodes; i++)
        {
            for (int j = 0; j < NumberOfNodes; j++)
            {
                arrayString += string.Format("{0} ", Weights[i][j]);
            }
            Debug.Log(arrayString);
            arrayString = "";
        }
    }

    public Stack<TestNode> Dijkstra(TestNode StartingNode, TestNode EndNode)
    {
        TestNode[] nodes = new TestNode[Nodes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = Nodes[i].GetComponent<TestNode>().clone();
        }
        TestNode StartingScript = nodes[StartingNode.GetComponent<TestNode>().ID];
        TestNode EndScript = nodes[EndNode.GetComponent<TestNode>().ID];

        StartingScript.currWeight = 0;

        TestNode next = StartingScript;
        while (next.currWeight != double.PositiveInfinity && !EndScript.visited)
        {
            next = visit(next.ID, nodes);
        }
        next = EndScript;

        Stack<TestNode> path = new Stack<TestNode>();
        path.Push(null);
        path.Push(Nodes[next.ID].GetComponent<TestNode>());
        while (next.parent != null)
        {
            path.Push(Nodes[next.parent.ID].GetComponent<TestNode>());
            next = next.parent;
        }

        return path;
    }

    private TestNode visit(int index, TestNode[] nodes)
    {
        TestNode curr = nodes[index];
        for (int j = 0; j < Weights.Count; j++)
        {
            TestNode neighbour = nodes[j];
            double newWeight = (curr.currWeight + Weights[index][j]);
            if (!neighbour.visited && Weights[index][j] != 0 && newWeight < neighbour.currWeight)
            {
                neighbour.currWeight = newWeight;
                neighbour.parent = curr;
            }
        }

        curr.visited = true;
        TestNode next = new TestNode(double.PositiveInfinity);
        foreach (TestNode node in nodes)
        {
            if (!node.visited && node.currWeight < next.currWeight)
            {
                next = node;
            }
        }
        return next;
    }
    void Update()
    {

    }
}

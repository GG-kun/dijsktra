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
            TestNode currentScript = currentNode.GetComponent<TestNode>().clone();
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
        TestNode StartingScript = StartingNode.GetComponent<TestNode>().clone();
        TestNode EndScript = EndNode.GetComponent<TestNode>().clone();

        int InitialID = StartingScript.ID;
        int EndID = EndScript.ID;

        StartingScript.currWeight = 0;

        TestNode next = Nodes[InitialID].GetComponent<TestNode>().clone();

        while (next.currWeight != double.PositiveInfinity && !Nodes[EndID].GetComponent<TestNode>().clone().visited)
        {
            next = visit(next.ID);
        }

        next = Nodes[EndID].GetComponent<TestNode>().clone();

        Stack<TestNode> path = new Stack<TestNode>();
        path.Push(null);
        path.Push(next);
        while (next.parent != null)
        {
            path.Push(next.parent);
            next = next.parent;
        }

        return path;
    }

    private TestNode visit(int index)
    {
        TestNode curr = Nodes[index].GetComponent<TestNode>().clone();
        for (int j = 0; j < Weights.Count; j++)
        {
            TestNode neighbour = Nodes[j].GetComponent<TestNode>().clone();
            double newWeight = (curr.currWeight + Weights[index][j]);
            if (!neighbour.visited && Weights[index][j] != 0 && newWeight < neighbour.currWeight)
            {
                neighbour.currWeight = newWeight;
                neighbour.parent = curr;
            }
        }

        curr.visited = true;
        TestNode next = new TestNode(double.PositiveInfinity);
        foreach (GameObject node in Nodes)
        {
            TestNode curNode = node.GetComponent<TestNode>().clone();
            if (!curNode.visited && curNode.currWeight < next.currWeight)
            {
                next = curNode;
            }

        }
        return next;
    }
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject StartingNode;
    public GameObject EndNode;

    private GameObject[] Nodes;

    public List<int[]> Weights = new List<int[]>();

    void Start()
    {
        if (StartingNode == null)
        {
            Debug.LogError("StartingNode is missing!");
        }
        if (EndNode == null)
        {
            Debug.LogError("EndNode is missing!");
        }
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

        Dijkstra();
    }

    void Dijkstra()
    {
        TestNode StartingScript = StartingNode.GetComponent<TestNode>();
        TestNode EndScript = EndNode.GetComponent<TestNode>();

        int InitialID = StartingScript.ID;
        int EndID = EndScript.ID;

        StartingScript.currWeight = 0;

        TestNode next = Nodes[InitialID].GetComponent<TestNode>();
        do
        {
            next = visit(next.ID);
        } while (next.currWeight != double.PositiveInfinity && !Nodes[EndID].GetComponent<TestNode>().visited);

        TestPlayer.path.Push(null);
        TestPlayer.path.Push(next);
        do
        {
            TestPlayer.path.Push(next.parent);
            next = next.parent;
        } while (next.parent != null);
    }

    public TestNode visit(int index)
    {
        TestNode curr = Nodes[index].GetComponent<TestNode>();
        for (int j = 0; j < Weights.Count; j++)
        {
            TestNode neighbour = Nodes[j].GetComponent<TestNode>();
            double newWeight = (curr.currWeight + Weights[index][j]);
            if (!neighbour.visited && Weights[index][j] != 0 && newWeight < neighbour.currWeight)
            {
                neighbour.currWeight = newWeight;
                neighbour.parent = curr;
            }
        }

        curr.visited = true;
        TestNode next = new TestNode();
        foreach (GameObject node in Nodes)
        {
            TestNode curNode = node.GetComponent<TestNode>();
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

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
        Nodes = GameObject.FindGameObjectsWithTag("Node");
        int NumberOfNodes = Nodes.Length;
        Debug.Log("Number of Nodes found: " + NumberOfNodes);

        for (int i = 0; i < NumberOfNodes; i++)
        {
            int[] tempWeights = new int[NumberOfNodes];
            GameObject currentNode = Nodes[i];
            TestNode currentScript = currentNode.GetComponent<TestNode>();

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

    void Update()
    {

    }
}

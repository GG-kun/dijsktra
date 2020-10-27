using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour
{
    public int[,] weights = new int[,]{{0,4,0,5,0},
        {4,0,0,0,3},
        {0,0,0,6,7},
        {5,0,6,0,4},
        {0,3,7,4,0}
    };
    public Node[] nodes;
    public int initial, final;

    public Node visit(int index){
        Node curr = nodes[index];
        for (int j = 0; j < weights.GetLength(1); j++)
        {
            Node neighbour = nodes[j];
            double newWeight = (curr.currWeight + weights[index, j]);
            if(!neighbour.visited && weights[index, j] != 0 && newWeight < neighbour.currWeight){
                neighbour.currWeight = newWeight;
                neighbour.parent = curr;
            }
        }
        curr.visited = true;        
        Node next = new Node(double.PositiveInfinity);
        foreach (Node node in nodes)
        {
            if(!node.visited && node.currWeight < next.currWeight){
                next = node;
            }
        }
        return next;
    }

    private void Awake() {
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].id = i;
        }
        nodes[initial].currWeight = 0;
        Node next = nodes[initial];
        do
        {
            next = visit(next.id);
        } while (next.currWeight != double.PositiveInfinity && !nodes[final].visited);
        next = nodes[final];
        Player.path.Push(null);
        Player.path.Push(next);
        do
        {
            Player.path.Push(next.parent);
            next = next.parent;
        } while (next.parent != null);
    }

    private void FixedUpdate() {
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                if(weights[i,j] != 0){
                    Debug.DrawLine(nodes[i].position, nodes[j].position);
                }
            }
        }
    }
}

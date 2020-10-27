using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 position;
    public double currWeight = double.PositiveInfinity;
    public Node parent;
    public bool visited = false;
    public int id;
    void Awake()
    {
        position = transform.position;
    }

    public Node(double currWeight){
        this.currWeight = currWeight;
    }
}
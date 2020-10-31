using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public Stack<TestNode> path = new Stack<TestNode>();
    public float diffDistance = 0.001f;
    public float speed = 1.0f;
    public TestNode target;
    public TestNode StartingNode;
    public TestNode EndNode;

    // Start is called before the first frame update
    void Start()
    {
        TestController algorithm = gameObject.AddComponent<TestController>();
        double minDistance = double.PositiveInfinity;
        foreach (GameObject nodeObject in TestController.Nodes)
        {
            TestNode node = nodeObject.GetComponent<TestNode>();
            double distance = Mathf.Abs(Vector3.Distance(transform.position,nodeObject.transform.position));
            if(distance < minDistance){
                StartingNode = node;
                minDistance = distance;
            }
            if(node.isTreasure){
                EndNode = node;
            }
        }
        this.path = algorithm.Dijkstra(StartingNode, EndNode);
        Debug.Log("STACK START: " + this.path.Count);
        this.target = TestController.Nodes[this.path.Pop().ID].GetComponent<TestNode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.target != null)
        {
            // Move our position a step closer to the target.
            float step = this.speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, this.target.transform.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, this.target.transform.position) < diffDistance)
            {
                this.target = this.path.Pop();
            }
        }
    }
}

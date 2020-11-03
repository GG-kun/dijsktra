using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNode : MonoBehaviour
{
    // Start is called before the first frame update
    public int ID;
    public List<GameObject> ConnectedNodes;
    public bool visited = false;
    public TestNode parent;
    public double currWeight = double.PositiveInfinity;
    public bool isTreasure = false;
    public bool isClone = false;

    public TestNode(double currWeight)
    {
        this.currWeight = currWeight;
    }

    private void Awake()
    {
        if (ConnectedNodes != null)
        {
            List<GameObject> id_l = new List<GameObject>();
            for (int i = 0; i < ConnectedNodes.Count; i++)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (ConnectedNodes[i].transform.position - transform.position), out hit))
                {
                    if (hit.transform.tag != "Node")
                    {
                        id_l.Add(ConnectedNodes[i]);
                        // Debug.Log(transform.name + " does not have a clear path to " + ConnectedNodes[i].transform.name);
                    }
                }
            }

            foreach (var go in id_l)
            {
                ConnectedNodes.Remove(go);
            }

            for (int i = 0; i < ConnectedNodes.Count; i++)
            {

                TestNode tmp = ConnectedNodes[i].GetComponent<TestNode>();
                if (tmp.ConnectedNodes.IndexOf(this.gameObject) == -1)
                {
                    tmp.ConnectedNodes.Add(this.gameObject);
                }
                else
                {
                    continue;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (ConnectedNodes != null)
        {
            for (int i = 0; i < ConnectedNodes.Count; i++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(transform.position, ConnectedNodes[i].transform.position);
            }
        }
    }

    public TestNode clone()
    {
        GameObject tmp = new GameObject();
        TestNode clone = tmp.AddComponent<TestNode>();
        Destroy(tmp);
        clone.currWeight = this.currWeight;
        clone.ID = this.ID;
        clone.ConnectedNodes = this.ConnectedNodes;
        clone.visited = this.visited;
        clone.parent = this.parent;
        clone.isTreasure = this.isTreasure;
        this.isClone = true;
        return clone;
    }
}

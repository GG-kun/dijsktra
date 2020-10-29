using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNode : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> ConnectedNodes;
    public bool Visited = false;
    public double currWeight = double.PositiveInfinity;
    void Start()
    {
        if (ConnectedNodes != null)
        {
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

    // Update is called once per frame
    void Update()
    {

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
}

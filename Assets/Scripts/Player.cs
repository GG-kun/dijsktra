﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Stack<Node> path = new Stack<Node>();
    public float diffDistance = 0.001f;
    public float speed = 1.0f;
    public Node target;
    // Start is called before the first frame update
    void Start()
    {
        target = path.Pop();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target.position) < diffDistance)
            {
                // Swap the position of the cylinder.
                target = path.Pop();
            }
        }
    }
}

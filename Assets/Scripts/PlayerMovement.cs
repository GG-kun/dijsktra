using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.GetType() == typeof(SphereCollider))
        {
            string message = "Nope, this is not the treasure";
            if (obj.gameObject.GetComponent<TestNode>().isTreasure)
            {
                message = "You've found the treasure";
            }
            Debug.Log(message);
        }            
    }
}

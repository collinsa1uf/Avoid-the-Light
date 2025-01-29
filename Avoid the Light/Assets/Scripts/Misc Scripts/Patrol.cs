using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
    public Transform pointA;
    public Transform pointB;
    public bool toPosB = true;
    public float speed = 10f;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;
    // Use this for initialization
    void Start()
    {
        pointAPosition = new Vector3(pointA.position.x, pointA.position.y, pointA.position.z);
        pointBPosition = new Vector3(pointB.position.x, pointB.position.y, pointB.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 thisPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (toPosB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (thisPosition.Equals(pointBPosition))
            {
                //Debug.Log ("Position b");
                toPosB = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (thisPosition.Equals(pointAPosition))
            {
                //Debug.Log ("Position a");
                toPosB = true;
            }
        }
    }
}
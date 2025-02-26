using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollisionBoxcast : MonoBehaviour
{
    RaycastHit[] hit;
    BoxCollider lightCollider;
    public float distance = 10f;
    public float scaleX = 1.0f;
    public float scaleY = 1.0f;
    public float scaleZ = 1.0f;
    private bool hitPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        lightCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Boxcast();
    }

    void Boxcast()
    {
        lightCollider.size = new Vector3(scaleX, scaleY, scaleZ);
        hit = Physics.BoxCastAll(lightCollider.bounds.center, new Vector3(scaleX, scaleY, scaleZ) * 0.5f, transform.forward, transform.rotation, distance);

        bool playerInArray = false;
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject.name == "Player")
            {
                playerInArray = true;
            }
        }

        if (playerInArray)
        {
            hitPlayer = true;
            LightCollisionManager.SetSpotlight(gameObject);
        }
        else
        {
            hitPlayer = false;
        }
    }

    public bool GetHitPlayer()
    {
        return hitPlayer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * distance);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.forward * (distance / 2), new Vector3(scaleX, scaleY, scaleZ * distance));
    }
}

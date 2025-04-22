using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    RaycastHit[] hit;
    BoxCollider lightCollider;
    public float distance = 10f;
    public float damage = 10f;
    private bool hitPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        lightCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        UseRaycast();
    }

    void UseRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(lightCollider.transform.position, lightCollider.transform.forward, out hit))
        {
            if (hit.transform.gameObject.layer == 6)
            {
                LightCollisionManager.SetSpotlightHittingCurtain(gameObject);
            }
        }
    }

    public bool GetHitPlayer()
    {
        return hitPlayer;
    }

    public float GetDamage()
    {
        return damage;      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * distance);
    }
}

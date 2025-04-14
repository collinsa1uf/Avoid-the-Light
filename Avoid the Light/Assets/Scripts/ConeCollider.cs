using UnityEngine;

public class ConeCollider : MonoBehaviour
{
    Transform parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = gameObject.transform.parent;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightCollisionManager.SetHitPlayer(true);
            LightCollisionManager.SetParent(parent);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightCollisionManager.SetHitPlayer(false);
        }
    }
}

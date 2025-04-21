using UnityEngine;

public class LightCollisionManager : MonoBehaviour
{
    public GameObject player;
    private static GameObject spotlightHittingPlayer = null;
    private static BoxCollider boxCollider = null;
    private static Transform parent = null;
    private static GameObject spotlightBeingBlocked = null;
    private static bool hitPlayer = false;
    private bool isVisible = false;

    private void Start()
    {
        hitPlayer = false;
        isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckIfHittingPlayer();
        CheckIfVisible();

        Debug.Log("1. " + hitPlayer + "  2. " + isVisible);
        if (hitPlayer && isVisible)
        {
            DraculaController.setDamageNum(parent.gameObject.GetComponent<Raycast>().GetDamage());
            DraculaController.isNearLight = true;
            DraculaController.isInLight = true;
        }
        else if (hitPlayer && !isVisible)
        {
            DraculaController.isInLight = false;
            DraculaController.isNearLight = true;
        }
        else
        {
            DraculaController.isInLight = false;
            DraculaController.isNearLight = false;
        }

        CheckIfBeingBlocked();
    }

    public static void SetSpotlightHittingPlayer(GameObject spotlight)
    {
        spotlightHittingPlayer = spotlight;
        boxCollider = spotlightHittingPlayer.GetComponent<BoxCollider>();
    }

    /*void CheckIfHittingPlayer()
    {
        if (spotlightHittingPlayer != null)
        {
            hitPlayer = spotlightHittingPlayer.GetComponent<Boxcast>().GetHitPlayer();
        }
    }*/

    void CheckIfVisible()
    {
        /*if (boxCollider != null)
        {
            RaycastHit hit;
            if (Physics.Linecast(player.transform.position, boxCollider.transform.position, out hit))
            {
                if (hit.collider.gameObject.tag == "Light")
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                }
            }
        }*/
        if (parent != null)
        {
            RaycastHit hit;
            LayerMask layerMask = Physics.AllLayers & ~(1 << 9);
            if (Physics.Linecast(player.transform.position, parent.GetComponent<BoxCollider>().transform.position, out hit, layerMask, QueryTriggerInteraction.Ignore)) 
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Light")
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                }
            }
        }
    }

    public static void SetSpotlightBeingBlocked(GameObject spotlight)
    {
        spotlightBeingBlocked = spotlight;
    }

    void CheckIfBeingBlocked()
    {
        if (spotlightBeingBlocked != null)
        {
            hitPlayer = false;
            spotlightBeingBlocked.SetActive(false);
            spotlightBeingBlocked = null;
        }
    }

    public static void SetHitPlayer(bool hit)
    {
        hitPlayer = hit;
    }

    public static void SetParent(Transform p)
    {
        parent = p;
    }

    private void OnDrawGizmos()
    {
        /*if (spotlightHittingPlayer != null)
        {
            Debug.DrawLine(player.transform.position, boxCollider.transform.position, Color.red);
        }*/
        if (parent != null)
        {
            Debug.DrawLine(player.transform.position, parent.GetComponent<BoxCollider>().transform.position, Color.red);
        }
    }
}

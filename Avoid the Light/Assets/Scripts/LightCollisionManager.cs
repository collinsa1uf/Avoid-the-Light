using UnityEngine;

public class LightCollisionManager : MonoBehaviour
{
    public GameObject player;
    private static GameObject spotlightHittingPlayer = null;
    private static BoxCollider boxCollider = null;
    private static GameObject spotlightHittingCurtain = null;
    private bool hitPlayer = false;
    private bool isVisible = false;

    // Update is called once per frame
    void Update()
    {
        CheckIfHittingPlayer();
        CheckIfVisible();

        if (hitPlayer && isVisible)
        {
            DraculaController.isInLight = true;
        }
        else
        {
            DraculaController.isInLight = false;
        }

        CheckIfHittingCurtain();
    }

    public static void SetSpotlightHittingPlayer(GameObject spotlight)
    {
        spotlightHittingPlayer = spotlight;
        boxCollider = spotlightHittingPlayer.GetComponent<BoxCollider>();
    }

    void CheckIfHittingPlayer()
    {
        if (spotlightHittingPlayer != null)
        {
            hitPlayer = spotlightHittingPlayer.GetComponent<LightCollisionBoxcast>().GetHitPlayer();
        }
    }

    void CheckIfVisible()
    {
        if (boxCollider != null)
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
        }
    }

    public static void SetSpotlightHittingCurtain(GameObject spotlight)
    {
        spotlightHittingCurtain = spotlight;
    }

    void CheckIfHittingCurtain()
    {
        if (spotlightHittingCurtain != null)
        {
            spotlightHittingCurtain.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (spotlightHittingPlayer != null)
        {
            Debug.DrawLine(player.transform.position, boxCollider.transform.position, Color.red);
        }
    }
}

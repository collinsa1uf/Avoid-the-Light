using UnityEngine;

public class Killbox : MonoBehaviour
{
    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<DraculaController>().Die();
        }
    }
}

using UnityEngine;
using TMPro;

public class GrabKey : MonoBehaviour
{
    public GameObject silverKey;
    public GameObject goldKey;
    public TMP_Text dialogueText;

    private bool isNearKey = false;
    public static bool hasKey = false;
    public static string heldKeyTag = "";

    void Start()
    {
        if (silverKey != null) { silverKey.SetActive(false); }
        if (goldKey != null) { goldKey.SetActive(false); }
        if (dialogueText != null) { dialogueText.gameObject.SetActive(false); }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearKey = true;
            if (dialogueText)
            {
                dialogueText.gameObject.SetActive(true);
                dialogueText.text = "Press E to take the key";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearKey = false;
            if(dialogueText) dialogueText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isNearKey && Input.GetKeyDown(KeyCode.E))
        {
            string keyTag = gameObject.tag;

            // Enable the correct in-hand key
            if (keyTag == "Silver Lock" && silverKey != null)
            {
                silverKey.SetActive(true);
            }
            else if (keyTag == "Gold Lock" && goldKey != null)
            {
                goldKey.SetActive(true);
            }

            heldKeyTag = keyTag;
            hasKey = true;

            if (dialogueText != null) dialogueText.gameObject.SetActive(false);
            gameObject.SetActive(false); // Hide this key in the world
        }
    }
}

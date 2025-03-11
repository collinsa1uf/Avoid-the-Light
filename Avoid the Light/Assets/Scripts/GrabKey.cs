using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class GrabKey : MonoBehaviour
{
    public GameObject inHandKey;
    public GameObject modelKey;
    public TMP_Text dialogueText;
    private bool isNearKey = false;
    public static bool hasKey = false;

    void Start()
    {
        inHandKey.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearKey = true;
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = "Press E to take the key";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearKey = false;
            dialogueText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isNearKey && Input.GetKeyDown(KeyCode.E))
        {
            inHandKey.SetActive(true);
            dialogueText.gameObject.SetActive(false);
            modelKey.SetActive(false);
            hasKey = true;
        }
    }

}

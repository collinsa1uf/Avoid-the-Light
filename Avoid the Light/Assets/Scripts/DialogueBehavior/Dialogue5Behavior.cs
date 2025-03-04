using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FifthDialogueTrigger : MonoBehaviour
{
    public Image FifthDialoguePopup;
    public TextMeshProUGUI StoryDialogue5;

    private bool isPopupActive = false;

    void Start()
    {
        FifthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue5.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPopupActive && Input.GetKeyDown(KeyCode.X))
        {
            ClosePopup();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FifthDialoguePopup.gameObject.SetActive(true);
            StoryDialogue5.gameObject.SetActive(true);
            isPopupActive = true;
        }
    }

    void ClosePopup()
    {
        FifthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue5.gameObject.SetActive(false);
        isPopupActive = false;
    }
}

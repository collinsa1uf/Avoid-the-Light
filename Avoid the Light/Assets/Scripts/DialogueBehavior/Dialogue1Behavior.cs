using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstDialogueTrigger : MonoBehaviour
{
    public Image FirstDialoguePopup;
    public TextMeshProUGUI StoryDialogue1;

    private bool isPopupActive = false;

    void Start()
    {
        FirstDialoguePopup.gameObject.SetActive(false);
        StoryDialogue1.gameObject.SetActive(false);
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
            FirstDialoguePopup.gameObject.SetActive(true);
            StoryDialogue1.gameObject.SetActive(true);
            isPopupActive = true;
        }
    }

    void ClosePopup()
    {
        FirstDialoguePopup.gameObject.SetActive(false);
        StoryDialogue1.gameObject.SetActive(false);
        isPopupActive = false;
    }
}

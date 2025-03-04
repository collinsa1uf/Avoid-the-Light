using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecondDialogueTrigger : MonoBehaviour
{
    public Image SecondDialoguePopup;
    public TextMeshProUGUI StoryDialogue2;

    private bool isPopupActive = false;

    void Start()
    {
        SecondDialoguePopup.gameObject.SetActive(false);
        StoryDialogue2.gameObject.SetActive(false);
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
            SecondDialoguePopup.gameObject.SetActive(true);
            StoryDialogue2.gameObject.SetActive(true);
            isPopupActive = true;
        }
    }

    void ClosePopup()
    {
        SecondDialoguePopup.gameObject.SetActive(false);
        StoryDialogue2.gameObject.SetActive(false);
        isPopupActive = false;
    }
}

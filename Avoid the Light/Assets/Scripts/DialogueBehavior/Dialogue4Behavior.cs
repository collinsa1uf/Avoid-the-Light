using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FourthDialogueTrigger : MonoBehaviour
{
    public Image FourthDialoguePopup;
    public TextMeshProUGUI StoryDialogue4;

    private bool isPopupActive = false;

    void Start()
    {
        FourthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue4.gameObject.SetActive(false);
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
            FourthDialoguePopup.gameObject.SetActive(true);
            StoryDialogue4.gameObject.SetActive(true);
            isPopupActive = true;
        }
    }

    void ClosePopup()
    {
        FourthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue4.gameObject.SetActive(false);
        isPopupActive = false;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThirdtDialogueTrigger : MonoBehaviour
{
    public Image ThirdDialoguePopup;
    public TextMeshProUGUI StoryDialogue3;

    private bool isPopupActive = false;

    void Start()
    {
        ThirdDialoguePopup.gameObject.SetActive(false);
        StoryDialogue3.gameObject.SetActive(false);
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
            ThirdDialoguePopup.gameObject.SetActive(true);
            StoryDialogue3.gameObject.SetActive(true);
            isPopupActive = true;
        }
    }

    void ClosePopup()
    {
        ThirdDialoguePopup.gameObject.SetActive(false);
        StoryDialogue3.gameObject.SetActive(false);
        isPopupActive = false;
    }
}

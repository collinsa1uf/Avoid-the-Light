using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryElementsBehavior : MonoBehaviour
{
    public Image FirstDialoguePopup;
    public TextMeshProUGUI StoryDialogue1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 

        FirstDialoguePopup.gameObject.SetActive(false);
        StoryDialogue1.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            // Debug.Log("Hitting first dialogue");
            FirstDialoguePopup.gameObject.SetActive(true);
            StoryDialogue1.gameObject.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        FirstDialoguePopup.gameObject.SetActive(false);
        StoryDialogue1.gameObject.SetActive(false);
    }

}


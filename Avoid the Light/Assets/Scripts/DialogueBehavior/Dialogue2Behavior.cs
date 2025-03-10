using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class secondDialogueTrigger : MonoBehaviour
{
    public Image SecondDialoguePopup;
    public TextMeshProUGUI StoryDialogue2;
    private bool isPopupActive = false;
    private bool hasTriggeredDialogue = false;  // Flag to check if dialogue has been triggered already
    public float fadeDuration = 1f;  // Time to complete the fade-in effect

    void Start()
    {
        SecondDialoguePopup.gameObject.SetActive(false);
        StoryDialogue2.gameObject.SetActive(false);

        // Set initial alpha to 0 (fully transparent)
        SetAlpha(SecondDialoguePopup, 0f);
        SetAlpha(StoryDialogue2, 0f);
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
        if (other.gameObject.CompareTag("Player") && !hasTriggeredDialogue)
        {
            SecondDialoguePopup.gameObject.SetActive(true);
            StoryDialogue2.gameObject.SetActive(true);
            isPopupActive = true;
            hasTriggeredDialogue = true;

            // Start the fade-in effect
            StartCoroutine(FadeIn());
        }
    }

    void ClosePopup()
    {
        SecondDialoguePopup.gameObject.SetActive(false);
        StoryDialogue2.gameObject.SetActive(false);
        isPopupActive = false;
    }

    // Fade-in coroutine
    private IEnumerator FadeIn()
    {
        float time = 0f;

        // Fade in both the Image and the TextMeshPro
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);  // Gradually increase alpha
            SetAlpha(SecondDialoguePopup, alpha);
            SetAlpha(StoryDialogue2, alpha);
            yield return null;
        }

        // Ensure they are fully visible after the fade
        SetAlpha(SecondDialoguePopup, 1f);
        SetAlpha(StoryDialogue2, 1f);
    }

    // Helper function to set the alpha of a UI element
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FifthDialogueTrigger : MonoBehaviour
{
    public Image FifthDialoguePopup;
    public TextMeshProUGUI StoryDialogue5;
    private bool isPopupActive = false;
    private bool hasTriggeredDialogue = false;  // Flag to check if dialogue has been triggered already
    public float fadeDuration = 1f;  // Time to complete the fade-in effect

    void Start()
    {
        FifthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue5.gameObject.SetActive(false);

        // Set initial alpha to 0 (fully transparent)
        SetAlpha(FifthDialoguePopup, 0f);
        SetAlpha(StoryDialogue5, 0f);
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
            FifthDialoguePopup.gameObject.SetActive(true);
            StoryDialogue5.gameObject.SetActive(true);
            isPopupActive = true;
            hasTriggeredDialogue = true;

            // Start the fade-in effect
            StartCoroutine(FadeIn());
        }
    }

    void ClosePopup()
    {
        FifthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue5.gameObject.SetActive(false);
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
            SetAlpha(FifthDialoguePopup, alpha);
            SetAlpha(StoryDialogue5, alpha);
            yield return null;
        }

        // Ensure they are fully visible after the fade
        SetAlpha(FifthDialoguePopup, 1f);
        SetAlpha(StoryDialogue5, 1f);
    }

    // Helper function to set the alpha of a UI element
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}

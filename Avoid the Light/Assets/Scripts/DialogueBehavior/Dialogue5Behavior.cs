using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FifthDialogueTrigger : MonoBehaviour
{
    public Image FifthDialoguePopup;
    public TextMeshProUGUI StoryDialogue5;
    private bool isPopupActive = false;
    private bool hasTriggeredDialogue = false;  
    public float fadeDuration = 1f;  

    void Start()
    {
        FifthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue5.gameObject.SetActive(false);

        
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

            
            StartCoroutine(FadeIn());
        }
    }

    void ClosePopup()
    {
        FifthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue5.gameObject.SetActive(false);
        isPopupActive = false;
    }

    
    private IEnumerator FadeIn()
    {
        float time = 0f;

        
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);  
            SetAlpha(FifthDialoguePopup, alpha);
            SetAlpha(StoryDialogue5, alpha);
            yield return null;
        }

        
        SetAlpha(FifthDialoguePopup, 1f);
        SetAlpha(StoryDialogue5, 1f);
    }

  
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}

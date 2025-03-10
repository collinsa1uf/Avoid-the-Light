using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FourthDialogueTrigger : MonoBehaviour
{
    public Image FourthDialoguePopup;
    public TextMeshProUGUI StoryDialogue4;
    private bool isPopupActive = false;
    private bool hasTriggeredDialogue = false;  
    public float fadeDuration = 1f; 

    void Start()
    {
        FourthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue4.gameObject.SetActive(false);

        
        SetAlpha(FourthDialoguePopup, 0f);
        SetAlpha(StoryDialogue4, 0f);
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
            FourthDialoguePopup.gameObject.SetActive(true);
            StoryDialogue4.gameObject.SetActive(true);
            isPopupActive = true;
            hasTriggeredDialogue = true;

            
            StartCoroutine(FadeIn());
        }
    }

    void ClosePopup()
    {
        FourthDialoguePopup.gameObject.SetActive(false);
        StoryDialogue4.gameObject.SetActive(false);
        isPopupActive = false;
    }

    
    private IEnumerator FadeIn()
    {
        float time = 0f;

        
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);  
            SetAlpha(FourthDialoguePopup, alpha);
            SetAlpha(StoryDialogue4, alpha);
            yield return null;
        }

        
        SetAlpha(FourthDialoguePopup, 1f);
        SetAlpha(StoryDialogue4, 1f);
    }

    
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}

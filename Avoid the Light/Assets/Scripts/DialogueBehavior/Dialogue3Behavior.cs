using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ThirdDialogueTrigger : MonoBehaviour
{
    public Image ThirdDialoguePopup;
    public TextMeshProUGUI StoryDialogue3;
    private bool isPopupActive = false;
    private bool hasTriggeredDialogue = false;  
    public float fadeDuration = 1f;  
    void Start()
    {
        ThirdDialoguePopup.gameObject.SetActive(false);
        StoryDialogue3.gameObject.SetActive(false);

        
        SetAlpha(ThirdDialoguePopup, 0f);
        SetAlpha(StoryDialogue3, 0f);
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
            ThirdDialoguePopup.gameObject.SetActive(true);
            StoryDialogue3.gameObject.SetActive(true);
            isPopupActive = true;
            hasTriggeredDialogue = true;

            
            StartCoroutine(FadeIn());
        }
    }

    void ClosePopup()
    {
        ThirdDialoguePopup.gameObject.SetActive(false);
        StoryDialogue3.gameObject.SetActive(false);
        isPopupActive = false;
    }

    
    private IEnumerator FadeIn()
    {
        float time = 0f;

        
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);  
            SetAlpha(ThirdDialoguePopup, alpha);
            SetAlpha(StoryDialogue3, alpha);
            yield return null;
        }

        
        SetAlpha(ThirdDialoguePopup, 1f);
        SetAlpha(StoryDialogue3, 1f);
    }

    
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}
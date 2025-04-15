using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FirstDialogueTrigger : MonoBehaviour
{
    public Image FirstDialoguePopup;
    public TextMeshProUGUI StoryDialogue1;
    private bool isPopupActive = false;
    private bool hasTriggeredDialogue = false;  
    public float fadeDuration = 1f;

    private GameObject player;
    private DraculaController draculaController;

    void Start()
    {
        FirstDialoguePopup.gameObject.SetActive(false);
        StoryDialogue1.gameObject.SetActive(false);

       
        SetAlpha(FirstDialoguePopup, 0f);
        SetAlpha(StoryDialogue1, 0f);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            draculaController = player.GetComponent<DraculaController>();
        }
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
            FirstDialoguePopup.gameObject.SetActive(true);
            StoryDialogue1.gameObject.SetActive(true);
            isPopupActive = true;
            hasTriggeredDialogue = true;

            if (draculaController != null)
            {
                draculaController.enabled = false;
            }
         
            StartCoroutine(FadeIn());
        }
    }

    void ClosePopup()
    {
        FirstDialoguePopup.gameObject.SetActive(false);
        StoryDialogue1.gameObject.SetActive(false);
        isPopupActive = false;

        if (draculaController != null)
        {
            draculaController.enabled = true;
        }
    }

    
    private IEnumerator FadeIn()
    {
        float time = 0f;

        
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration); 
            SetAlpha(FirstDialoguePopup, alpha);
            SetAlpha(StoryDialogue1, alpha);
            yield return null;
        }

       
        SetAlpha(FirstDialoguePopup, 1f);
        SetAlpha(StoryDialogue1, 1f);
    }

   
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class secondDialogueTrigger : MonoBehaviour
{
    public Image SecondDialoguePopup;
    public TextMeshProUGUI StoryDialogue2;
    private bool isPopupActive = false;
    private bool hasTriggeredDialogue = false;  
    public float fadeDuration = 1f;

    private GameObject player;
    private DraculaController draculaController;

    void Start()
    {
        SecondDialoguePopup.gameObject.SetActive(false);
        StoryDialogue2.gameObject.SetActive(false);

        
        SetAlpha(SecondDialoguePopup, 0f);
        SetAlpha(StoryDialogue2, 0f);

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
            SecondDialoguePopup.gameObject.SetActive(true);
            StoryDialogue2.gameObject.SetActive(true);
            isPopupActive = true;
            hasTriggeredDialogue = true;

            if (draculaController != null)
            {
                draculaController.InDialogue();
                draculaController.enabled = false;
            }


            StartCoroutine(FadeIn());
        }
    }

    void ClosePopup()
    {
        SecondDialoguePopup.gameObject.SetActive(false);
        StoryDialogue2.gameObject.SetActive(false);
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
            SetAlpha(SecondDialoguePopup, alpha);
            SetAlpha(StoryDialogue2, alpha);
            yield return null;
        }

        
        SetAlpha(SecondDialoguePopup, 1f);
        SetAlpha(StoryDialogue2, 1f);
    }

   
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}

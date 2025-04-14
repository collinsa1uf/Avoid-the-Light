using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class CurtainInteraction : MonoBehaviour
{
    public Animator curtainAnimator; 
    public GameObject promptText; 

    private bool isPlayerNear = false;
    private bool isCurtainClosed = false;

    public AudioClip curtainAudioClip;

    void Start()
    {
        promptText.SetActive(false);
        curtainAnimator.SetBool("isClosed", false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isCurtainClosed)
        {
            CloseCurtain();
        }
        else if (isCurtainClosed)
        {
            promptText.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerNear = true;
            promptText.SetActive(true); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            promptText.SetActive(false); 
        }
    }

    void CloseCurtain()
    {
        curtainAnimator.SetBool("isClosed", true);
        isCurtainClosed = true;
        promptText.SetActive(false);

        StartCoroutine(DisableAnimator());
        SoundFXManager.instance.PlaySoundFXClip(curtainAudioClip, 0.4f);
    }

    IEnumerator DisableAnimator()
    {
        yield return new WaitForSeconds(1.5f);
        curtainAnimator.enabled = false;
    }
}
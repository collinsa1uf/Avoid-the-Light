using System.Collections;
using TMPro;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform doorHinge; // Assign door
    public TextMeshProUGUI lockedText;
    public TextMeshProUGUI openedText;
    public GameObject keyInHand;
    public GameObject interactionIndicator;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    public bool isLocked;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isPlayerNearby = false;

    // ==== Audio ====
    public AudioClip creakAudioClip;
    public AudioClip doorOpenClip;

    private void Start()
    {
        closedRotation = doorHinge.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        if (lockedText)
            lockedText.gameObject.SetActive(false); // Hide text

        if (openedText)
            openedText.gameObject.SetActive(false); // Hide

        if (interactionIndicator) interactionIndicator.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby)
            TryOpenDoor();

        if (isOpen) interactionIndicator.gameObject.SetActive(false);
    }

    void TryOpenDoor()
    {
        // Uses public static bool from GrabKey.cs to see if a key is collected.
        if (GrabKey.hasKey & !isOpen && isLocked)
            StartCoroutine(OpenDoor());
        else if (!GrabKey.hasKey && isLocked)
            StartCoroutine(ShowLockedMessage());
        else if (!isLocked)
            StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        SoundFXManager.instance.PlaySoundFXClip(creakAudioClip, 0.6f);
        SoundFXManager.instance.PlaySoundFXClip(doorOpenClip, 0.6f);

        isOpen = true;
        if (GrabKey.hasKey)
        {
            GrabKey.hasKey = false; // Key is used.
            keyInHand.gameObject.SetActive(false);
            StartCoroutine(ShowOpenedMessage());
        }

        interactionIndicator.gameObject.SetActive(false);

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * openSpeed;
            doorHinge.rotation = Quaternion.Lerp(closedRotation, openRotation, time);
            yield return null;
        }

    }

    IEnumerator ShowOpenedMessage()
    {
        if (interactionIndicator) interactionIndicator.SetActive(false);

        if (openedText)
        {
            openedText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            openedText.gameObject.SetActive(false);
        }

        if (isPlayerNearby && interactionIndicator) interactionIndicator.SetActive(true); // Show it again if player is still nearby
    }

    IEnumerator ShowLockedMessage()
    {
        if (interactionIndicator) interactionIndicator.SetActive(false);

        if (lockedText)
        {
            lockedText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            lockedText.gameObject.SetActive(false);
        }

        if (isPlayerNearby && interactionIndicator) interactionIndicator.SetActive(true); // Show it again if player is still nearby
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpen) return; // if the door is open dont show anything

        if(other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionIndicator) interactionIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionIndicator) interactionIndicator.SetActive(false);
        }
    }
}

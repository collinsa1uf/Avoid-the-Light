using System.Collections;
using UnityEngine;

public class MovableObjectLoop : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float interactionRange = 3f;
    public float indicatorOffset = 1f;
    public GameObject indicatorUI;

    public AudioSource moveSound;
    public Animator playerAnimator;

    private bool isMoving = false;
    private bool movingToB = true; // True = moving to B, False = moving to A
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (indicatorUI) indicatorUI.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Show interaction indicator
        if (indicatorUI)
            indicatorUI.SetActive(distance < interactionRange && !isMoving);

        if(indicatorUI.activeSelf)
        {
            UpdateIndicatorPosition();
        }

        if (distance < interactionRange && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            StartCoroutine(MoveBetweenPoints());
        }
    }

    void UpdateIndicatorPosition()
    {
        if (!indicatorUI || !player) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        Vector3 indicatorPosition = transform.position + directionToPlayer * indicatorOffset;

        indicatorUI.transform.position = indicatorPosition;

        indicatorUI.transform.LookAt(player);
        indicatorUI.transform.Rotate(0, 180, 0);
    }

    IEnumerator MoveBetweenPoints()
    {
        isMoving = true;

        if (indicatorUI) indicatorUI.SetActive(false);

        if (playerAnimator) playerAnimator.SetBool("isPushing", true);
        if (moveSound) moveSound.Play();

        Vector3 target = movingToB ? pointB.position : pointA.position;

        while(Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        movingToB = !movingToB;

        if (playerAnimator) playerAnimator.SetBool("isPushing", false);
        if (moveSound) moveSound.Stop();

        isMoving = false;
    }
}
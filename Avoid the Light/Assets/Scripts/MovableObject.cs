using System.Collections;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float interactionRange = 3f;
    public float indicatorOffset = 1f;
    public GameObject indicatorUI;

    public AudioSource moveSound;
    public Animator playerAnimator;

    private int currentWaypointIndex = 0;
    private bool isMoving = false;
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
            indicatorUI.SetActive(distance < interactionRange);

        if(indicatorUI.activeSelf)
        {
            UpdateIndicatorPosition();
        }

        if (distance < interactionRange && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            StartCoroutine(MoveToNextWaypoint());
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

    IEnumerator MoveToNextWaypoint()
    {
        if(currentWaypointIndex < waypoints.Length)
        {
            isMoving = true;

            if (playerAnimator) playerAnimator.SetBool("isPushing", true);
            if (moveSound) moveSound.Play();

            Vector3 targetPosition = waypoints[currentWaypointIndex].position;

            while(Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPosition;
            currentWaypointIndex++;

            if (playerAnimator) playerAnimator.SetBool("isPushing", false);
            if (moveSound) moveSound.Stop();

            isMoving = false;
        }
    }
}
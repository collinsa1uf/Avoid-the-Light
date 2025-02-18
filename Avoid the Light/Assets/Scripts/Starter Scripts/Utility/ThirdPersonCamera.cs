using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float rotationSpeed = 1f;

    public float mouseX;

    public float mouseY;

    public Transform Player;

    public Transform PlayerReference;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    public void CameraRotation()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -30, 60);
        gameObject.transform.LookAt(PlayerReference);
        if (Input.GetMouseButton(1))//Right Click
        {
            PlayerReference.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
            PlayerReference.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
    }
}

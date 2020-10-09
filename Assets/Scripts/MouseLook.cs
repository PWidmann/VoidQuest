using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 300f;

    public Transform playerBody;
    float mouseX;
    float mouseY;

    float xRotation = 0f;

    private void LateUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity  * Time.smoothDeltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity  * Time.smoothDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

}

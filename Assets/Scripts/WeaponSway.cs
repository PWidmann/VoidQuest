using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    private float amount = 0.05f;
    private float maxAmount = 0.02f;
    private float smoothAmount = 8f;

    Vector3 startPosition;
    Vector3 finalPosition;

    float movementX;
    float movementY;
    float movementZ;

    float walkX;

    float jump = 0;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        movementX = -Input.GetAxis("Mouse X") * amount;
        movementY = -Input.GetAxis("Mouse Y") * amount;
        walkX = -Input.GetAxis("Horizontal") * amount;
        movementZ = -Input.GetAxis("Vertical") * amount;

        

        if (Input.GetKeyDown(KeyCode.Space) && PlayerController.Instance.isGrounded)
        {
            jump = 0.15f;
        }

        if (jump > 0)
        {
            jump -= Time.deltaTime / 2f;
            if (jump < 0)
                jump = 0;
        }

        movementX = Mathf.Clamp(movementX + walkX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY , -maxAmount, maxAmount);
        movementZ = Mathf.Clamp(movementZ, -maxAmount, maxAmount);

        finalPosition = new Vector3(movementX, movementY - jump, movementZ);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + startPosition, Time.deltaTime * smoothAmount);
    }
}

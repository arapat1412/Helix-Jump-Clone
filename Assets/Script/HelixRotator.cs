using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixRotator : MonoBehaviour
{
    public float rotationSpeed = 600f;
    public float rotationSpeedAndroid = 50f;

    private void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(transform.position.x, -mouseX * rotationSpeed * Time.deltaTime, transform.position.z);
        }

        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float xDeltaPos = Input.GetTouch(0).deltaPosition.x;
            transform.Rotate(transform.position.x, -xDeltaPos * rotationSpeedAndroid * Time.deltaTime, transform.position.y);
        }
    }
}

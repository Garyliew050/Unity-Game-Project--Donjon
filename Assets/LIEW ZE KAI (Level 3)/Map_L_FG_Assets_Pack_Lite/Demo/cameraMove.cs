using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {

    private float rSpeed = 3.0f;
    private float X = 0.0f;
    private float Y = 0.0f;

    void Update()
    {
        X += Input.GetAxis("Mouse X") * rSpeed;
        Y += Input.GetAxis("Mouse Y") * rSpeed;

        // Limit the vertical rotation to avoid camera flipping
        Y = Mathf.Clamp(Y, -90f, 90f);

        transform.localRotation = Quaternion.Euler(-Y, X, 0);
    }
}

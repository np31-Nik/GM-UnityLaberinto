using UnityEngine;
using System.Collections;

public class SueloController : MonoBehaviour {

    public float RateOfRotate = 1.0f;
    public float lowLimitX = -30.0f;
    public float highLimitX = 30.0f;
    public float lowLimitZ = -30.0f;
    public float highLimitZ = 30.0f;

    private bool isAndroid;

    private float initialYRotation;

    void Start () {
        #if UNITY_ANDROID
            isAndroid = true;
        #else
            isAndroid = false;
        #endif

        // Store the initial Y rotation
        initialYRotation = transform.rotation.eulerAngles.y;
    }

    void FixedUpdate () {
        if (isAndroid)
        {
            float posH = Input.acceleration.x;
            float posV = Input.acceleration.y;

            RotateObject(posH, posV);
        }
        else
        {
            float posH = Input.GetAxis("Horizontal");
            float posV = Input.GetAxis("Vertical");

            RotateObject(posV, posH);
        }
    }

    void RotateObject(float horizontalInput, float verticalInput) {
        Quaternion rotation = transform.rotation;

        // Calculate rotation only around X and Z axes
        Quaternion deltaRotation = Quaternion.Euler(horizontalInput * RateOfRotate, 0, -verticalInput * RateOfRotate);

        // Apply rotation only to X and Z axes
        Quaternion newRotation = rotation * deltaRotation;
        newRotation = ClampRotation(newRotation);

        // Restore initial Y rotation
        Vector3 euler = newRotation.eulerAngles;
        euler.y = initialYRotation;
        newRotation = Quaternion.Euler(euler);

        transform.rotation = newRotation;
    }

    Quaternion ClampRotation(Quaternion q) {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, lowLimitX, highLimitX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
        angleZ = Mathf.Clamp(angleZ, lowLimitZ, highLimitZ);
        q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

        return q;
    }
}

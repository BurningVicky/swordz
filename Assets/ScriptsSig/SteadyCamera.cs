using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyCamera : MonoBehaviour
{
    public Transform target; // CameraHolder
    public Vector3 desiredOffset = new Vector3(0, 2, -5);
    public float smoothSpeed = 5f;
    public float deadzoneDistance = 0.5f;

    private bool isMoving = false;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + target.rotation * desiredOffset;
        Vector3 difference = desiredPosition - transform.position;

        if (!isMoving && difference.magnitude > deadzoneDistance)
        {
            isMoving = true;
        }
        else if (isMoving && difference.magnitude <= 0.01f)
        {
            isMoving = false;
        }

        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed * Time.deltaTime);
        }
    }
}

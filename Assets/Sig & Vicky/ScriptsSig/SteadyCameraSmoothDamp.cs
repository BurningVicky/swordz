using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyCameraSmoothDamp : MonoBehaviour
{
    public Transform target; // CameraHolder
    public Vector3 desiredOffset = new Vector3(0, 2, -5);
    public float smoothTime = 0.2f; // tempo para suavizar
    public float deadzoneDistance = 0.5f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + target.rotation * desiredOffset;
        Vector3 difference = desiredPosition - transform.position;

        if (difference.magnitude > deadzoneDistance)
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }

        // Sempre suaviza a rotação também
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime / smoothTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyCameraRevamp : MonoBehaviour
{
    public Transform target; // CameraHolder
    public Vector3 desiredOffset = new Vector3(0, 2, -5);
    public float smoothTime = 0.2f;
    public float deadzoneDistance = 0.5f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 smoothedRotatedOffset;

    void Start()
    {
        smoothedRotatedOffset = target.rotation * desiredOffset;
    }

    void LateUpdate()
    {
        Vector3 rotatedOffset = target.rotation * desiredOffset;

        // Suaviza o offset rotacionado
        smoothedRotatedOffset = Vector3.Lerp(smoothedRotatedOffset, rotatedOffset, Time.deltaTime / smoothTime);

        Vector3 desiredPosition = target.position + smoothedRotatedOffset;
        Vector3 difference = desiredPosition - transform.position;

        if (difference.magnitude > deadzoneDistance)
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }

        // Sempre suaviza a rotação da câmera também
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime / smoothTime);
    }
}

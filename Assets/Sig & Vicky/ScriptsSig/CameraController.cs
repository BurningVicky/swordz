using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform cameraTransform; // O "braço" ou a MainCamera
    public Transform player; // Referência ao Player

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotação horizontal (Y) → aplica no holder inteiro
        transform.Rotate(Vector3.up * mouseX);

        // Rotação vertical (X) → aplica só na câmera (localRotation)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // CameraHolder segue o player na posição
        transform.position = player.position;
    }
}

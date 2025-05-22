using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform cameraTransform; // Arm ou MainCamera
    public Transform player; // Referência ao Player

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        // Rotação do holder em Y
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Rotação do braço em X
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // *** Aqui segue a posição do player ***
        transform.position = player.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor en el centro de la pantalla
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        // Limitar la rotación en X para evitar hacer giros completos
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplicar rotación en X (vertical) y Y (horizontal)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}

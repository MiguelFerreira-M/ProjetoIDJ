using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCamera : MonoBehaviour
{
    [HideInInspector]
    public PlayerController playerController;

    private float rotateX = 0f;
    private float currentRotationX = 0f;

    void Start()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    void Update()
    {
        RotateCamera();
    }

    public void OnCameraMovement(InputAction.CallbackContext value)
    {
        rotateX = value.ReadValue<float>();
    }

    private void RotateCamera()
    {
        currentRotationX += rotateX * playerController.activeMovementSet.lookSensitivity * Time.deltaTime;
        currentRotationX = Mathf.Clamp(currentRotationX,-90, 90);
        transform.localEulerAngles = new Vector3(currentRotationX, 0, 0);
        rotateX = 0;
    }
}

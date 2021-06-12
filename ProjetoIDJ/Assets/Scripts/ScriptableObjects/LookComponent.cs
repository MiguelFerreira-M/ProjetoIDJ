using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/LookComponent")]
public class LookComponent : ActionSetComponent
{
    [SerializeField]
    private LookValues[] lookValues;
    private int activeMovementSetIndex;

    private float rotateY;
    private float currentRotationY = 0f;

    private float rotateX = 0f;
    private float currentRotationX = 0f;

    public override void OnStart()
    {
        playerController.characterCamera.transform.localEulerAngles = Vector3.zero;
    }

    public override void OnUpdate()
    {
        activeMovementSetIndex = playerController.activeMovementSetIndex;
        RotateCharacter();
        RotateCamera();
    }


    public void OnCameraMovementY(InputAction.CallbackContext value)
    {
        rotateY = value.ReadValue<float>();
    }

    private void RotateCharacter()
    {
        currentRotationY += rotateY * lookValues[activeMovementSetIndex].lookSensitivity * Time.deltaTime;

        playerController.characterRigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, currentRotationY, 0)));

        rotateY = 0;
    }

    public void OnCameraMovementX(InputAction.CallbackContext value)
    {
        rotateX = value.ReadValue<float>();
    }

    private void RotateCamera()
    {
        currentRotationX += rotateX * lookValues[activeMovementSetIndex].lookSensitivity * Time.deltaTime;
        currentRotationX = Mathf.Clamp(currentRotationX, -90, 90);
        playerController.characterCamera.transform.localEulerAngles = new Vector3(currentRotationX, 0, 0);
        rotateX = 0;
    }
}

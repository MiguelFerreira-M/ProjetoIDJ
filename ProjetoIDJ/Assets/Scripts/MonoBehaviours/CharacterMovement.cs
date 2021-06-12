using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void Del();

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [HideInInspector]
    public PlayerController playerController;

    public Del moveHandler;
   
    public Rigidbody CharacterRigidbody;

    public Vector3 moveVector;
    public Vector3 dirVector;

    public Vector3 inputVector;

    public float currentSpeed = 0f;

    private float rotateY;
    private float currentRotationY = 0f;
    
    public bool actionMove = false;

    void Start()
    {
        CharacterRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RotateCharacter();
    }

    void FixedUpdate()
    {
    }

    public void OnCameraMovement(InputAction.CallbackContext value)
    {
        rotateY = value.ReadValue<float>();
    }

    private void RotateCharacter()
    {
        currentRotationY += rotateY * playerController.activeMovementSet.lookSensitivity * Time.deltaTime;

        CharacterRigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, currentRotationY, 0)));

        rotateY = 0;
    }

}

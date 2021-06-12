using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/JumpComponent")]
public class JumpComponent : ActionSetComponent
{
    [SerializeField]
    private float jumpForce = 200f;

    [SerializeField]
    private float sphereCastRadius = 0.5f;

    [SerializeField]
    private LayerMask layerMask;

    private bool onGround = false;

    public override void OnUpdate()
    {
        Vector3 castSpherePos = playerController.transform.position;
        if (Physics.CheckSphere(castSpherePos, sphereCastRadius, layerMask))
        {
            onGround = true;
        }
        else
            onGround = false;
    }

    public void Jump(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (activeMovementAction)
            {
                if (playerController)
                {
                    if (onGround)
                    {
                        
                        playerController.characterRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    }
                }
                else
                    throw new System.NullReferenceException("NO REFERENCE FOR PLAYER CONTROLLER");
            }
        }
    }
}

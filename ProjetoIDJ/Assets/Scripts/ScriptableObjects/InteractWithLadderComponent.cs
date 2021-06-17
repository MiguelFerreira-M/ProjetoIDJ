using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/InteractWithLadderComponents")]
public class InteractWithLadderComponent : ActionSetComponent
{
    public bool canInteractWithLadder = false;
    public bool onLadder;

    public void OnTriggerEnter(Collider collider)
    {
        if (activeMovementAction)
        {
            if (collider.CompareTag("Ladder"))
            {
                canInteractWithLadder = true;
            }
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (activeMovementAction)
        {
            if (collider.CompareTag("Ladder"))
            {
                canInteractWithLadder = false;
            }
        }
    }

    public void OnInteractWithLadder(InputAction.CallbackContext context)
    {
        if (activeMovementAction)
        {
            if (context.performed)
            {
                if (onLadder)
                {
                    playerController.ChangeActiveActionMapSet("MovementActionMap");
                    playerController.characterRigidbody.useGravity = true;
                    onLadder = false;
                }
                else
                {
                    if (canInteractWithLadder)
                    {
                        playerController.ChangeActiveActionMapSet("LadderActionMap");
                        onLadder = true;
                        playerController.characterRigidbody.useGravity = false;
                    }
                }
            }
        }
    }
}

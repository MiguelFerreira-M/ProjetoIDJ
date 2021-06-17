using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/ChangeToSprintComponent")]
public class ChangeToSprintComponent : ActionSetComponent
{
    public void ChangeToSprint(InputAction.CallbackContext context)
    {
        if (activeMovementAction)
        {
            if (context.performed)
            {
                playerController.activeActionMapSet.ChangeActiveActionSet("Sprint");
            }
            if (context.canceled)
            {
                playerController.activeActionMapSet.ChangeActiveActionSet(playerController.activeActionMapSet.defaultActionSetIndex);
            }
        }
    }
}

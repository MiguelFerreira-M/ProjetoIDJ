using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/ChangeActiveActionSetComponent")]
public class ChangeActiveActionSetComponent : ActionSetComponent
{
    public string actionSetName1;
    public string actionSetName2;

    public void ChangeActiveActionSet(InputAction.CallbackContext context)
    {
        if (activeMovementAction)
        {
            if (context.performed)
            {
                if(playerController.activeActionSetIndex == playerController.defaultActionSetIndex)
                {
                    ChangDefaultActionSetIndex();

                    playerController.ChangeActiveActionSet(playerController.defaultActionSetIndex);
                }
                else
                {
                    ChangDefaultActionSetIndex();
                }
            }
        }
    }

    private void ChangDefaultActionSetIndex()
    {
        if (playerController.GetActionSetName(playerController.defaultActionSetIndex) == actionSetName1)
        {
            playerController.defaultActionSetIndex = playerController.GetActionSetIndex(actionSetName2);
        }
        else
        {
            playerController.defaultActionSetIndex = playerController.GetActionSetIndex(actionSetName1);
        }
    }
}

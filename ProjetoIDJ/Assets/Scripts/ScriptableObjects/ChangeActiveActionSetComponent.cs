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
                if(playerController.activeActionMapSet.activeActionSetIndex == playerController.activeActionMapSet.defaultActionSetIndex)
                {
                    ChangDefaultActionSetIndex();

                    playerController.activeActionMapSet.ChangeActiveActionSet(playerController.activeActionMapSet.defaultActionSetIndex);
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
        if (playerController.activeActionMapSet.GetActionSetName(playerController.activeActionMapSet.defaultActionSetIndex) == actionSetName1)
        {
            playerController.activeActionMapSet.defaultActionSetIndex = playerController.activeActionMapSet.GetActionSetIndex(actionSetName2);
        }
        else
        {
            playerController.activeActionMapSet.defaultActionSetIndex = playerController.activeActionMapSet.GetActionSetIndex(actionSetName1);
        }
    }
}

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
                if(playerController.GetActiveActionSetName() == actionSetName1)
                {
                    playerController.ChangeActiveActionSet(playerController.GetActionSetIndex(actionSetName2));
                    playerController.defaultActionSetIndex = playerController.GetActionSetIndex(actionSetName2);
                }
                else
                {
                    playerController.ChangeActiveActionSet(playerController.GetActionSetIndex(actionSetName1));
                    playerController.defaultActionSetIndex = playerController.GetActionSetIndex(actionSetName1);
                }
                    
            }
        }
    }
}

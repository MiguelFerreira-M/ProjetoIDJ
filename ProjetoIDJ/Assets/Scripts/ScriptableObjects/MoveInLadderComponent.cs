
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/MoveInLadderComponet")]
public class MoveInLadderComponent : ActionSetComponent
{
    private Vector3 inputVector;
    public float speed = 2f;

    public void OnMoveInLadder(InputAction.CallbackContext context )
    {
        inputVector = new Vector3(0f, context.ReadValue<float>(), 0f);
    }

    public override void OnFixedUpdate()
    {
        MoveInLadder();
    }

    private void MoveInLadder()
    {
        playerController.characterRigidbody.MovePosition(playerController.transform.position + inputVector * speed * Time.deltaTime);
    }
}

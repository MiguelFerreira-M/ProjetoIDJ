using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/MoveComponent")]
public class MoveComponent : ActionSetComponent
{
    public List<ActionSetComponent> actionComponents = new List<ActionSetComponent>();


    public override void OnFixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        playerController.characterMovement.dirVector = playerController.transform.TransformDirection(playerController.characterMovement.inputVector);

        foreach(ActionSetComponent actionComponent in actionComponents)
        {
            actionComponent.playerController = playerController;
            actionComponent.OnFixedUpdate();
        }

        playerController.characterMovement.moveVector = playerController.characterMovement.dirVector;

        CalculateMoveSpeed();
        playerController.characterMovement.moveVector *= playerController.characterMovement.currentSpeed;

        playerController.characterMovement.CharacterRigidbody.MovePosition(playerController.transform.position + playerController.characterMovement.moveVector * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            playerController.characterMovement.actionMove = true;
            playerController.characterMovement.inputVector = new Vector3(value.ReadValue<Vector2>().x, 0, value.ReadValue<Vector2>().y);
        }
        if (value.canceled)
        {
            playerController.characterMovement.actionMove = false;
        }
    }

    private void CalculateMoveSpeed()
    {
        if (playerController.characterMovement.actionMove)
        {
            playerController.characterMovement.currentSpeed += ((playerController.activeMovementSet.moveSpeed * FactorOfDirection()) / (playerController.activeMovementSet.timeToMaxSpeed / Time.deltaTime));
        }
        else
        {
            playerController.characterMovement.currentSpeed -= ((playerController.activeMovementSet.moveSpeed * FactorOfDirection()) / (playerController.activeMovementSet.timeToStop / Time.deltaTime));
        }

        playerController.characterMovement.currentSpeed = Mathf.Clamp(playerController.characterMovement.currentSpeed, 0, playerController.activeMovementSet.moveSpeed * FactorOfDirection());
    }

    private float FactorOfDirection()
    {
        if (playerController.characterMovement.inputVector.z < 0)
            return playerController.activeMovementSet.factorOfMoveSpeedBackwards;
        else if (playerController.characterMovement.inputVector.z == 0)
            return playerController.activeMovementSet.factorOfMoveSpeedSideways;
        else
            return playerController.activeMovementSet.factorOfMoveSpeedFoward;
    }
}

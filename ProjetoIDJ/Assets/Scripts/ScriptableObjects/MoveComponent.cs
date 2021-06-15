using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/MoveComponent")]
public class MoveComponent : ActionSetComponent
{
    public List<ActionSetComponent> actionComponents = new List<ActionSetComponent>();

    [SerializeField]
    private MoveValues[] moveValues;
    private int activeMovementSetIndex;

    private Vector3 inputVector;
    public Vector3 dirVector;
    private Vector3 moveVector;

    private float currentSpeed = 0f;

    private bool actionMove = false;

    public override void OnFixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        activeMovementSetIndex = playerController.activeMovementSetIndex;

        dirVector = playerController.transform.TransformDirection(inputVector);

        foreach(ActionSetComponent actionComponent in actionComponents)
        {
            actionComponent.playerController = playerController;
            actionComponent.OnFixedUpdate();
        }

        moveVector = dirVector;

        CalculateMoveSpeed();
        moveVector *= currentSpeed;

        playerController.characterRigidbody.MovePosition(playerController.transform.position + moveVector * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            actionMove = true;
            inputVector = new Vector3(value.ReadValue<Vector2>().x, 0, value.ReadValue<Vector2>().y);
        }
        if (value.canceled)
        {
            actionMove = false;
        }
    }

    private void CalculateMoveSpeed()
    {
        if (actionMove)
        {
            currentSpeed += ((moveValues[activeMovementSetIndex].moveSpeed * FactorOfDirection()) / (moveValues[activeMovementSetIndex].timeToMaxSpeed / Time.deltaTime));
        }
        else
        {
            currentSpeed -= ((moveValues[activeMovementSetIndex].moveSpeed * FactorOfDirection()) / (moveValues[activeMovementSetIndex].timeToStop / Time.deltaTime));
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, moveValues[activeMovementSetIndex].moveSpeed * FactorOfDirection());
    }

    private float FactorOfDirection()
    {
        if (inputVector.z < 0)
            return moveValues[activeMovementSetIndex].factorOfMoveSpeedBackwards;
        else if (inputVector.z == 0)
            return moveValues[activeMovementSetIndex].factorOfMoveSpeedSideways;
        else
            return moveValues[activeMovementSetIndex].factorOfMoveSpeedFoward;
    }
}

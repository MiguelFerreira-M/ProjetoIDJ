using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ActionsSetComponent/MoveComponent")]
public class MoveComponent : ActionSetComponent
{
    public Vector3Variable inputVector;
    public Vector3Variable dirVector;
    public Vector3Variable moveVector;
    public FloatVariable currentSpeed;
    [Space]

    public List<ActionSetComponent> actionComponents = new List<ActionSetComponent>();
    [Space]
    
    [SerializeField]
    private MoveValues[] moveValues;

    private int activeMovementSetIndex;

    private bool actionMove = false;

    public override void OnFixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        activeMovementSetIndex = playerController.activeActionSetIndex;

        dirVector.value = playerController.transform.TransformDirection(inputVector.value);

        foreach(ActionSetComponent actionComponent in actionComponents)
        {
            actionComponent.playerController = playerController;
            actionComponent.OnFixedUpdate();
        }

        moveVector.value = dirVector.value;

        CalculateMoveSpeed();
        moveVector.value *= currentSpeed.value;

        playerController.characterRigidbody.MovePosition(playerController.transform.position + moveVector.value * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            actionMove = true;
            inputVector.value = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
        if (context.canceled)
        {
            actionMove = false;
        }
    }

    private void CalculateMoveSpeed()
    {
        if (actionMove)
        {
            currentSpeed.value += ((moveValues[activeMovementSetIndex].moveSpeed * FactorOfDirection()) / (moveValues[activeMovementSetIndex].timeToMaxSpeed / Time.deltaTime));
        }
        else
        {
            currentSpeed.value -= ((moveValues[activeMovementSetIndex].moveSpeed * FactorOfDirection()) / (moveValues[activeMovementSetIndex].timeToStop / Time.deltaTime));
        }

        currentSpeed.value = Mathf.Clamp(currentSpeed.value, 0, moveValues[activeMovementSetIndex].moveSpeed * FactorOfDirection());
    }

    private float FactorOfDirection()
    {
        if (inputVector.value.z < 0)
            return moveValues[activeMovementSetIndex].factorOfMoveSpeedBackwards;
        else if (inputVector.value.z == 0)
            return moveValues[activeMovementSetIndex].factorOfMoveSpeedSideways;
        else
            return moveValues[activeMovementSetIndex].factorOfMoveSpeedFoward;
    }
}

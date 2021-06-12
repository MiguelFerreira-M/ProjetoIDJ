using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement _characterMovement;
    public CharacterMovement characterMovement { get => _characterMovement;}

    [SerializeField]
    private CharacterCamera _characterCamera;
    public CharacterCamera characterCamera { get => _characterCamera;}

    public ActionSet activeMovementSet { get; set; }

    [SerializeField]
    private ActionSet[] movementSets;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characterMovement.playerController = this;
        characterCamera.playerController = this;

        ChangeActiveMovementSet(movementSets[0]);
    }

    void Update()
    {
        if(activeMovementSet)
            foreach (ActionSetComponent actionComponent in activeMovementSet.actionComponents)
            {
                if(actionComponent)
                    actionComponent.OnUpdate();
                else
                    throw new System.NullReferenceException();
            }
    }

    void FixedUpdate()
    {
        if (activeMovementSet)
            foreach (ActionSetComponent actionComponent in activeMovementSet.actionComponents)
            {
                if (actionComponent)
                    actionComponent.OnFixedUpdate();
                else
                    throw new System.NullReferenceException();
            }
    }

    public void OnChangeActiveMovementSet(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
            foreach (ActionSet movementSet in movementSets)
            {
                if (movementSet.name == value.action.name)
                {
                    ChangeActiveMovementSet(movementSet);
                    return;
                }
            }
        }
        
        if(value.canceled)
        {
            ChangeActiveMovementSet(movementSets[0]);
            return;
        }
    }

    private void ChangeActiveMovementSet(ActionSet newMovementSet)
    {
        if (activeMovementSet != null)
        {
            foreach (ActionSetComponent actionComponent in activeMovementSet.actionComponents)
            {
                actionComponent.playerController = null;
                actionComponent.activeMovementAction = false;
            }
        }
        
        activeMovementSet = newMovementSet;

        foreach (ActionSetComponent actionComponent in activeMovementSet.actionComponents)
        {
            actionComponent.playerController = this;
            actionComponent.activeMovementAction = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionSetComponent : ScriptableObject
{
    private PlayerController _playerController;
    public PlayerController playerController
    {
        get => _playerController;
        set
        {
            if (value == null)
            {
                _playerController = value;
                activeMovementAction = false;
            }
            else
            {
                _playerController = value;
                activeMovementAction = true;
            }    
        }
    }

    public bool activeMovementAction = false;

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
}

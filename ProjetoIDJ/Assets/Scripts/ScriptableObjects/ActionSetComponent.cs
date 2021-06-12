using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionSetComponent : ScriptableObject
{
    public PlayerController playerController;
    public bool activeMovementAction = false;

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
}

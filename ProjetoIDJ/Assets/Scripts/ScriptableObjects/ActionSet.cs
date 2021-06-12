using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ActionsSet", order = 1)]
public class ActionSet : ScriptableObject
{
    public List<ActionSetComponent> actionComponents = new List<ActionSetComponent>();

    public void CallStart()
    {
            foreach (ActionSetComponent actionComponent in actionComponents)
            {
                if (actionComponent)
                    actionComponent.OnStart();
                else
                    throw new System.NullReferenceException();
            }
    }

    public void CallUpdate()
    {
        foreach (ActionSetComponent actionComponent in actionComponents)
        {
            if (actionComponent)
                actionComponent.OnUpdate();
            else
                throw new System.NullReferenceException();
        }
    }

    public void CallFixedUpdate()
    {
        foreach (ActionSetComponent actionComponent in actionComponents)
        {
            if (actionComponent)
                actionComponent.OnFixedUpdate();
            else
                throw new System.NullReferenceException();
        }
    }

    public void SetActive(PlayerController playerController)
    {
        foreach (ActionSetComponent actionComponent in actionComponents)
        {
            if (actionComponent)
            {
                actionComponent.playerController = playerController;
                actionComponent.activeMovementAction = true;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public void SetDisabled()
    {
        foreach (ActionSetComponent actionComponent in actionComponents)
        {
            if (actionComponent)
            {
                actionComponent.playerController = null;
                actionComponent.activeMovementAction = false;
            }
            else
                throw new System.NullReferenceException();
        }
    }
}

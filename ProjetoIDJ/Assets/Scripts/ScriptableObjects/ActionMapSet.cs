using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionMapSet")]
public class ActionMapSet : ScriptableObject
{
    public ActionSet[] actionSets;

    [HideInInspector]
    public PlayerController playerController;

    private ActionSet _activeActionSet;
    public ActionSet activeActionSet { get => _activeActionSet; }

    private int _activeActiontSetIndex;
    public int activeActionSetIndex { get => _activeActiontSetIndex; }

    [HideInInspector]
    public int defaultActionSetIndex;

    public void OnStart()
    {
        ChangeActiveActionSet(0);
        defaultActionSetIndex = 0;

        activeActionSet.CallStart();
    }

    public void OnUpdate()
    {
        activeActionSet.CallUpdate();
    }

    public void OnFixedUpdate()
    {
        activeActionSet.CallFixedUpdate();
    }

    public void ChangeActiveActionSet(string newActionSetName)
    {
        for (int i = 0; i < actionSets.Length; i++)
        {
            if (newActionSetName == actionSets[i].name)
            {
                if (activeActionSet != null)
                {
                    activeActionSet.SetDisabled();
                }

                _activeActionSet = actionSets[i];
                _activeActiontSetIndex = i;
                _activeActionSet.SetActive(playerController);

                return;
            }
        }
    }

    public void ChangeActiveActionSet(int newActionSetIndex)
    {
        if (activeActionSet != null)
        {
            activeActionSet.SetDisabled();
        }

        _activeActionSet = actionSets[newActionSetIndex];

        activeActionSet.SetActive(playerController);

        _activeActiontSetIndex = newActionSetIndex;
    }

    public int GetActionSetIndex(string actionSetName)
    {
        for (int i = 0; i < actionSets.Length; i++)
        {
            if (actionSetName == actionSets[i].name)
            {
                return i;
            }
        }
        Debug.LogError("NO ACTION SETS WITH THAT NAME");
        return 0;
    }

    public string GetActionSetName(int actionSetIndex)
    {
        if (actionSetIndex < actionSets.Length)
        {
            return activeActionSet.name;
        }
        else
            Debug.LogError("NO ACTION SETS WITH THAT NAME");
        return null;
    }

    public void DisableActiveActionSet()
    {
        activeActionSet.SetDisabled();
    }
}

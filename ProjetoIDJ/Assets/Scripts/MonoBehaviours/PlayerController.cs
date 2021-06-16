using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterCamera;
    public GameObject characterCamera { get => _characterCamera;}

    private Rigidbody _characterRigidbody;
    public Rigidbody characterRigidbody { get => _characterRigidbody; }

    private ActionSet _activeActionSet;
    public ActionSet activeActionSet { get => _activeActionSet; }

    private int _activeActiontSetIndex;
    public int activeActionSetIndex { get => _activeActiontSetIndex; }

    [HideInInspector]
    public int defaultActionSetIndex;


    [SerializeField]
    private ActionSet[] actionSets;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _characterRigidbody = GetComponent<Rigidbody>();

        ChangeActiveActionSet(0);
        defaultActionSetIndex = 0;

        activeActionSet.CallStart();
    }

    void Update()
    {
        activeActionSet.CallUpdate();
    }

    void FixedUpdate()
    {
        activeActionSet.CallFixedUpdate();
    }

    public void ChangeActiveActionSet(string newActionSetIndex)
    {
        for (int i = 0; i < actionSets.Length; i++)
        {
            if (newActionSetIndex == actionSets[i].name)
            {
                if (activeActionSet != null)
                {
                    activeActionSet.SetDisabled();
                }

                _activeActionSet = actionSets[i];
                _activeActiontSetIndex = i;
                _activeActionSet.SetActive(this);

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

        activeActionSet.SetActive(this);

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

    public string GetActiveActionSetName()
    {
        return actionSets[activeActionSetIndex].name;
    }
}

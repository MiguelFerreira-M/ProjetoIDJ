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

    private ActionSet _activeMovementSet;
    public ActionSet activeMovementSet { get => _activeMovementSet; }

    private int _activeMovementSetIndex;
    public int activeMovementSetIndex { get => _activeMovementSetIndex; }


    [SerializeField]
    private ActionSet[] movementSets;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _characterRigidbody = GetComponent<Rigidbody>();

        ChangeActiveMovementSet(movementSets[0]);
        _activeMovementSetIndex = 0;

        activeMovementSet.CallStart();
    }

    void Update()
    {
        activeMovementSet.CallUpdate();
    }

    void FixedUpdate()
    {
        activeMovementSet.CallFixedUpdate();
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

    public void ChangeActiveMovementSet(ActionSet newMovementSet)
    {
        if (activeMovementSet != null)
        {
            activeMovementSet.SetDisabled();
        }
        
        _activeMovementSet = newMovementSet;

        activeMovementSet.SetActive(this);

        for(int i = 0; i < movementSets.Length; i++)
        {
            if (newMovementSet.name == movementSets[i].name)
            {
                _activeMovementSetIndex = i;
                return;
            }
        }
    }

    public void ChangeActiveMovementSet(int newMovementSetIndex)
    {
        if (activeMovementSet != null)
        {
            activeMovementSet.SetDisabled();
        }

        _activeMovementSet = movementSets[newMovementSetIndex];

        activeMovementSet.SetActive(this);

        _activeMovementSetIndex = newMovementSetIndex;
    }
}

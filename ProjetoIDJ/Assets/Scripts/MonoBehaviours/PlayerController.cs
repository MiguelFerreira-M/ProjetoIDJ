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

        ChangeActiveMovementSet(0);

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

    public void ChangeActiveMovementSet(string newMovementSet)
    {
        for (int i = 0; i < movementSets.Length; i++)
        {
            if (newMovementSet == movementSets[i].name)
            {
                if (activeMovementSet != null)
                {
                    activeMovementSet.SetDisabled();
                }

                _activeMovementSet = movementSets[i];
                _activeMovementSetIndex = i;
                _activeMovementSet.SetActive(this);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterCamera;
    public GameObject characterCamera { get => _characterCamera;}

    private Rigidbody _characterRigidbody;
    public Rigidbody characterRigidbody { get => _characterRigidbody; }

    [HideInInspector]
    private PlayerInput _playerInput;
    public PlayerInput playerInput { get => _playerInput; }

    [SerializeField]
    private ActionMapSet[] actionMapSets;

    [HideInInspector]
    public ActionMapSet activeActionMapSet;

    [Space]
    public OnTriggerEnterEvent onTriggerEnterEvent;

    [Space]
    public OnTriggerExitEvent OnTriggerExitEvent;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _characterRigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();

        activeActionMapSet = actionMapSets[0];

        activeActionMapSet.playerController = this;

        activeActionMapSet.OnStart();
    }

    void Update()
    {
        activeActionMapSet.OnUpdate();
    }

    void FixedUpdate()
    {
        activeActionMapSet.OnFixedUpdate();
    }

    public void OnDisable()
    {
        activeActionMapSet.activeActionSet.SetDisabled();
    }

    public void ChangeActiveActionMapSet(string ActionMapSetName)
    {
        foreach(ActionMapSet actionMapSet in actionMapSets)
        {
            if(actionMapSet.name == ActionMapSetName)
            {
                activeActionMapSet.DisableActiveActionSet();
                activeActionMapSet = actionMapSet;
                activeActionMapSet.playerController = this;

                playerInput.SwitchCurrentActionMap(ActionMapSetName);

                activeActionMapSet.OnStart();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnterEvent.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent.Invoke(other);
    }
}

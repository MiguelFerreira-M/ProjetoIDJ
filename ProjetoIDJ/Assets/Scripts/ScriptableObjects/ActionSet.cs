using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ActionsSet", order = 1)]
public class ActionSet : ScriptableObject
{
    [SerializeField][Range(0f, 50f)]
    private float _lookSensitivity = 15f;
    public float lookSensitivity { get => _lookSensitivity; set => _lookSensitivity = value; }

    [SerializeField][Range(0.0f, 100f)]
    private float _moveSpeed = 10f;
    public float moveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [SerializeField][Range(0.0f, 10f)]
    private float _timeToMaxSpeed = 1f;
    public float timeToMaxSpeed { get => _timeToMaxSpeed; set => _timeToMaxSpeed = value; }

    [SerializeField][Range(0.0f, 10f)]
    private float _timeToStop = 1f;
    public float timeToStop { get => _timeToStop; set => _timeToStop = value; }

    [SerializeField][Range(0.0f, 1f)]
    private float _factorOfMoveSpeedFoward = 1f;
    public float factorOfMoveSpeedFoward { get => _factorOfMoveSpeedFoward; set => _factorOfMoveSpeedFoward = value; }

    [SerializeField][Range(0.0f, 1f)]
    private float _factorOfMoveSpeedBackwards = 0.5f;
    public float factorOfMoveSpeedBackwards { get => _factorOfMoveSpeedBackwards; set => _factorOfMoveSpeedBackwards = value; }

    [SerializeField][Range(0.0f, 1f)]
    private float _factorOfMoveSpeedSideways = 1f;
    public float factorOfMoveSpeedSideways { get => _factorOfMoveSpeedSideways; set => _factorOfMoveSpeedSideways = value; }


    public List<ActionSetComponent> actionComponents = new List<ActionSetComponent>();

}

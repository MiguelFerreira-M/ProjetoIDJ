using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionsSetComponentValues/MoveValues")]
public class MoveValues : ScriptableObject
{
    [Range(0.0f, 100f)]
    public float moveSpeed = 10f;

    [Range(0.0f, 10f)]
    public float timeToMaxSpeed = 1f;

    [Range(0.0f, 10f)]
    public float timeToStop = 1f;

    [Range(0.0f, 1f)]
    public float factorOfMoveSpeedFoward = 1f;

    [Range(0.0f, 1f)]
    public float factorOfMoveSpeedBackwards = 0.5f;

    [Range(0.0f, 1f)]
    public float factorOfMoveSpeedSideways = 1f;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionsSetComponentValues/LookValues")]
public class LookValues : ScriptableObject
{
    [Range(0f,20f)]
    public float lookSensitivity = 1f;
}

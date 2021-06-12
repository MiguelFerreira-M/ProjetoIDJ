using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionsSetComponent/OnSlopeComponent")]
public class OnSlopeComponent : ActionSetComponent
{
    [SerializeField]
    private MoveComponent moveComponent;

    public override void OnFixedUpdate()
    {
        ProjectToSlope();
        Debug.DrawLine(playerController.transform.position, playerController.transform.position + moveComponent.dirVector);
    }

    private Vector3 CheckPlaneNormal()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerController.transform.position + Vector3.up, Vector3.down * 0.2f, out hit))
        {
            return hit.normal;
        }

        return Vector3.up;
    }

    private void ProjectToSlope()
    {
        moveComponent.dirVector = Vector3.ProjectOnPlane(moveComponent.dirVector, CheckPlaneNormal());
    }
}

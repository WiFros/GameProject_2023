using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 previousTargetPosition;
    private Vector3 positionOffset;
    private Vector3 rotationOffset;
    
    private void Start()
    {
        previousTargetPosition = target.position;
    }
    
    private void Update()
    {
        previousTargetPosition = target.position;

        transform.SetPositionAndRotation(previousTargetPosition + positionOffset, Quaternion.Euler(rotationOffset));
    }

    public void SetCameraOffset(Vector3 pos, Vector3 rot)
    {
        positionOffset = pos;
        rotationOffset = rot;
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
    }
}

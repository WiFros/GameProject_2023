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
        if (target.position != previousTargetPosition)
        {
            previousTargetPosition = target.position;
            //return;
        }

        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, previousTargetPosition + positionOffset, 10.0f*Time.deltaTime), Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotationOffset), 5.0f * Time.deltaTime));
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

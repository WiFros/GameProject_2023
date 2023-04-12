using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCameraFollow : MonoBehaviour
{
    public Transform target;
        private Vector3 previousTargetPosition;
        private Vector3 offset;
    
        private void Start()
        {
            offset = transform.position - target.position;
            previousTargetPosition = target.position;
        }
    
        private void Update()
        {
            if (target.position != previousTargetPosition)
            {
                previousTargetPosition = target.position;
                return;
            }
    
            Vector3 desiredPosition = target.position + offset;
            transform.position = desiredPosition;
        }
}

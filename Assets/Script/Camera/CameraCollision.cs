using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Vector3 cameraOffset; // 카메라와 플레이어 사이의 기본 거리
    public float minDistance = 1.0f; // 카메라와 플레이어 사이의 최소 거리
    public Transform cameraTransform; // 카메라의 Transform

    private Vector3 desiredPosition;

    void Update()
    {
        if (Player.Instance != null) // 플레이어 싱글톤 인스턴스가 있는지 확인
        {
            desiredPosition = Player.Instance.transform.position + cameraOffset;
            RaycastHit hit;

            if (Physics.Raycast(Player.Instance.transform.position, cameraOffset, out hit))
            {
                desiredPosition = Player.Instance.transform.position + (cameraOffset.normalized * Mathf.Max(hit.distance, minDistance));
            }

            cameraTransform.position = desiredPosition;
        }
    }
}

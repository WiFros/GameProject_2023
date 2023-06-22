using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.yellow; // 기즈모의 색상
    public Vector3 gizmoSize = Vector3.one; // 기즈모의 크기

    void OnDrawGizmos()
    {
        // 기즈모의 색상을 설정합니다.
        Gizmos.color = gizmoColor;

        // 이 오브젝트의 위치에 기즈모를 그립니다. 기즈모의 크기는 gizmoSize 변수를 이용합니다.
        Gizmos.DrawCube(transform.position, gizmoSize);
    }
}

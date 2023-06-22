using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpInteraction : Interactable
{
    public Transform jumpStartPoint;  // 점프 시작 지점
    public Transform jumpEndPoint;    // 점프 종료 지점
    public float jumpTime = 1f;       // 점프에 걸리는 시간

    private GameObject player;
    private bool isJumping = false;

    void Start()
    {
        // Find the player GameObject using its tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Interact(GameObject player)
    {
        // 점프를 시작합니다. 
        if(!isJumping)
            StartCoroutine(JumpRoutine(player));
    }

    private IEnumerator JumpRoutine(GameObject player)
    {
        isJumping = true;

        Vector3 startPoint = jumpStartPoint.position;
        Vector3 endPoint = jumpEndPoint.position;

        // 곡선 움직임을 위한 중간 지점 계산
        Vector3 midPoint = (startPoint + endPoint) / 2.0f + Vector3.up;

        float timer = 0.0f;

        while (timer <= jumpTime)
        {
            float t = timer / jumpTime;
            Vector3 nextPos = Vector3.Lerp(startPoint, midPoint, t) * t + Vector3.Lerp(midPoint, endPoint, t) * (1 - t);
            player.transform.position = nextPos;
            timer += Time.deltaTime;
            yield return null;
        }

        player.transform.position = endPoint;
        isJumping = false;
    }
}

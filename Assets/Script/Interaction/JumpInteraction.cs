using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class JumpInteraction : MonoBehaviour
{
    public JumpInteraction targetJumpPoint; // 대상 점프 지점
    public TextMeshProUGUI text_Description;
    public float jumpDuration = 1.0f; // 점프에 걸리는 시간 (초)
    private bool isPlayerNear = false;
    private GameObject player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // 태그를 이용해서 플레이어를 확인
        {
            text_Description.gameObject.SetActive(true);
            text_Description.text = "Press F to jump";
            player = other.gameObject; // 충돌한 오브젝트를 플레이어로 설정
            Debug.Log("Press F to jump");
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text_Description.gameObject.SetActive(false);
            text_Description.text = "";
            isPlayerNear = false;
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            // 점프를 시작합니다.
            StartCoroutine(JumpToTarget());
        }
    }

    private IEnumerator JumpToTarget()
    {
        Vector3 startPosition = player.transform.position; // 점프 시작 위치
        Vector3 targetPosition = targetJumpPoint.transform.position; // 대상 위치는 대상 점프 지점의 위치입니다.
        Vector3 jumpPeak = (startPosition + targetPosition) / 2.0f + Vector3.up * 2.0f; // 점프 곡선의 정점

        float elapsedTime = 0;

        while (elapsedTime < jumpDuration / 2.0f)
        {
            player.transform.position = Vector3.Lerp(startPosition, jumpPeak, elapsedTime / (jumpDuration / 2.0f));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0; // 경과 시간을 다시 0으로 초기화합니다.

        while (elapsedTime < jumpDuration / 2.0f)
        {
            player.transform.position = Vector3.Lerp(jumpPeak, targetPosition, elapsedTime / (jumpDuration / 2.0f));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        player.transform.position = targetPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullInteraction : MonoBehaviour
{
    public float interactionDistance = 1.0f;
        private GameObject player;
        private bool isPlayerNear = false;
    
        private void Start()
        {
            player = GameObject.FindWithTag("Player"); // 플레이어 찾기
        }
    
        private void Update()
        {
            // 플레이어가 물체 주변에 있는지 확인
            if (Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
            {
                isPlayerNear = true;
            }
            else
            {
                isPlayerNear = false;
            }
    
            if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
            {
                // 플레이어가 밀거나 당기는 방향을 계산
                Vector3 direction = transform.position - player.transform.position;
                direction = Vector3.Normalize(new Vector3(Mathf.Round(direction.x), 0, Mathf.Round(direction.z)));
    
                // 플레이어가 방향키를 누르면 방향을 확인
                Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                inputDirection = Vector3.Normalize(new Vector3(Mathf.Round(inputDirection.x), 0, Mathf.Round(inputDirection.z)));
    
                // 물체를 밀거나 당기는 방향을 결정
                if (Vector3.Dot(direction, inputDirection) > 0)
                {
                    StartCoroutine(MoveObject(transform.position + direction)); // 밀기
                }
                else
                {
                    StartCoroutine(MoveObject(transform.position - direction)); // 당기기
                }
            }
        }
    
        private IEnumerator MoveObject(Vector3 targetPosition)
        {
            Vector3 startPosition = transform.position;
            float distance = Vector3.Distance(startPosition, targetPosition);
            float duration = distance; // 이동하는 데 걸리는 시간 (여기서는 1초로 설정)
            float elapsedTime = 0;
    
            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
    
            transform.position = targetPosition;
        }
}

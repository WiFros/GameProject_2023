using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDetecter : MonoBehaviour
{
    public float interactionRange = 3f; // 상호작용 가능한 거리
    public LayerMask npcLayer; // NPC를 감지할 레이어

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // E 키를 눌렀을 때
        {
            InteractWithNPC(); // NPC와 상호작용
        }
    }

    private void InteractWithNPC()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, npcLayer)) // 플레이어 위치에서 정면으로 Raycast를 발사하여 NPC를 감지
        {
            NPC npc = hit.collider.GetComponent<NPC>(); // 충돌한 객체의 NPC 컴포넌트 가져오기
            if (npc != null) // NPC 컴포넌트가 존재하는 경우
            {
                npc.TriggerDialogue(); // 대화 시작
            }
        }
    }
}
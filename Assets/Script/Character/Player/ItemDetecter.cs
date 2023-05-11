using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDetecter : MonoBehaviour
{
    [SerializeField] private float pickupRange = 3f; // 아이템 획득 가능한 거리
    [SerializeField] private LayerMask itemLayer; // 아이템을 감지할 레이어
    [SerializeField] private TextMeshProUGUI actionText; // 행동 안내 텍스트
    [SerializeField] private Inventory inventory; // 인벤토리

    private ItemPickUp currentItem; // 현재 선택된 아이템

    private void Update()
    {
        CheckForItem(); // 아이템 감지 확인
        PickupItem(); // 아이템 획득
    }

    private void CheckForItem()
    {
        RaycastHit hit;
        bool itemInRange = Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, itemLayer); // 플레이어 위치에서 정면으로 Raycast를 발사하여 아이템을 감지
        if (itemInRange)
        {
            ItemPickUp itemPickUp = hit.collider.GetComponent<ItemPickUp>(); // 충돌한 객체의 ItemPickUp 컴포넌트 가져오기
            if (itemPickUp != null) // ItemPickUp 컴포넌트가 존재하는 경우
            {
                currentItem = itemPickUp; // 현재 선택된 아이템으로 설정
                actionText.gameObject.SetActive(true); // 행동 안내 텍스트 활성화
                actionText.text = currentItem.item.itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>"; // 텍스트 업데이트
            }
        }
        else if (currentItem != null) // 아이템이 감지 범위 밖에 있는 경우
        {
            float distance = Vector3.Distance(transform.position, currentItem.transform.position); // 플레이어와 아이템 사이의 거리 계산
            if (distance > pickupRange)
            {
                currentItem = null; // 현재 선택된 아이템 초기화
                actionText.gameObject.SetActive(false); // 행동 안내 텍스트 비활성화
            }
        }
    }

    private void PickupItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentItem != null) // E 키를 누르고 현재 선택된 아이템이 있는 경우
        {
            Debug.Log(currentItem.item.itemName + "을(를) 획득했습니다."); // 획득한 아이템 로그 출력
            inventory.AcquireItem(currentItem.item); // 인벤토리에 아이템 추가
            Destroy(currentItem.gameObject); // 아이템 오브젝트 제거
            actionText.gameObject.SetActive(false); // 행동 안내 텍스트 비활성화
            currentItem = null; // 현재 선택된 아이템 초기화
        }
    }
}

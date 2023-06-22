using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private Interactable currentInteractable;
    private void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E 키를 눌렀습니다.");
            currentInteractable.Interact(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        Debug.Log("OnTriggerEnter");
        if (interactable != null)
        {
            currentInteractable = interactable;
            currentInteractable.ShowUI(); // 플레이어가 상호작용 가능 범위에 들어오면 UI를 보여줍니다.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && currentInteractable == interactable)
        {
            currentInteractable.HideUI(); // 플레이어가 상호작용 가능 범위에서 나가면 UI를 숨깁니다.
            currentInteractable = null;
        }
    }
}

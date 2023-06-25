using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public Renderer objectRenderer;  // 오브젝트의 Renderer 컴포넌트
    private Material originalMaterial;  // 오브젝트의 원래 Material
    public Material highlightedMaterial;  // 오브젝트가 강조될 때의 Material

    private Rigidbody rb; // Rigidbody 컴포넌트

    private void Start()
    {
        // 오브젝트의 원래 Material을 저장합니다.
        originalMaterial = objectRenderer.material;
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
    }
    public void Interact()
    {
        // 플레이어가 오브젝트를 잡으면 오브젝트의 Material을 강조된 Material로 변경합니다.
        objectRenderer.material = highlightedMaterial;
        if (rb != null)
        {
            rb.isKinematic = true; // Rigidbody의 움직임을 멈춤
        }
    }

    public void StopInteract()
    {
        // 플레이어가 오브젝트를 놓으면 오브젝트의 Material을 원래의 Material로 돌려놓습니다.
        objectRenderer.material = originalMaterial;
        if (rb != null)
        {
            rb.isKinematic = false; // Rigidbody의 움직임을 다시 활성화
        }
    }

    public float moveSpeed = 1.0f;
    public float rotateSpeed = 60.0f;  // 초당 60도 회전

    public void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void Rotate(float amount)
    {
        transform.Rotate(0, amount * rotateSpeed * Time.deltaTime, 0);
    }
}

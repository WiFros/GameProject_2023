using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 2; // 데미지 양을 정의합니다. Inspector에서 조절 가능합니다.
    public float knockbackForce = 15f; // 넉백 힘을 정의합니다. Inspector에서 조절 가능합니다.

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        Debug.Log("OnCollisionEnter : " + other.gameObject.name);
        if (player != null) // 플레이어와 충돌했는지 확인합니다.
        {
            Debug.Log("Player Hit");
            player.TakeDamage(damageAmount, knockbackForce, transform.position);
        }
    }
}

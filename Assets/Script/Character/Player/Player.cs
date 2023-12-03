using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isMoving = false;
    public int maxHealth = 10;
    public int currentHealth;
    public float invincibilityDuration = 2.0f;  // 무적 시간을 조절할 수 있는 변수

    private bool isInvincible = false;  // 무적 상태 판단
    private Renderer playerRenderer;  // 플레이어 Renderer
    private Color originalColor;  // 플레이어 원래 색상
    private Rigidbody rb;  // Rigidbody 컴포넌트

    // 이 클래스를 위한 싱글톤 인스턴스
    public static Player Instance { get; private set; }

    void Awake()
    {
        // 싱글톤 인스턴스 체크 및 할당
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);  // 씬 전환시에도 인스턴스 유지
    }

    void Start()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
        UIManager.Instance.UpdateHealthBar(currentHealth, maxHealth); // 체력바 UI 업데이트

        playerRenderer = GetComponentInChildren<Renderer>();
        originalColor = playerRenderer.material.color;
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
    }

    void Update()
    {
        if (Input.anyKey) // 키 입력이 있다면
            isMoving = true;
        else
            isMoving = false;
    }



    IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;  // 무적 상태 설정

        float elapsedTime = 0f;
        float blinkInterval = 0.2f;  // 깜빡이는 간격

        while (elapsedTime < duration)
        {
            playerRenderer.material.color = Color.clear;  // 투명하게 만들기
            yield return new WaitForSeconds(blinkInterval);

            playerRenderer.material.color = originalColor;  // 원래 색상으로
            yield return new WaitForSeconds(blinkInterval);

            elapsedTime += blinkInterval * 2;
        }

        isInvincible = false;  // 무적 상태 해제
        playerRenderer.material.color = originalColor;  // 원래 색상으로 복구
    }

    public void TakeDamage(int damage, float knockbackForce, Vector3 enemyPosition)
    {
        if (!isInvincible) // 무적 상태가 아니라면
        {
            Debug.Log("Player TakeDamage");
            // 체력 감소
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);
            UIManager.Instance.UpdateHealthBar(currentHealth, maxHealth); // 체력바 UI 업데이트

            // 넉백
            Vector3 knockbackDirection = (transform.position - enemyPosition).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            // 무적 시간 부여
            StartCoroutine(InvincibilityRoutine(invincibilityDuration));
        }
    }
}
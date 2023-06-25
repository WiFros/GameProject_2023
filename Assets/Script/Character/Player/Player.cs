using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public Image[] hearts; // 완전한 하트 이미지 배열
    public Sprite fullHeart; // 완전한 하트 스프라이트
    public Sprite halfHeart; // 절반 하트 스프라이트
    public Sprite emptyHeart; // 빈 하트 스프라이트

    // 이 클래스를 위한 싱글톤 인스턴스
    public static Player Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth / 2)
                hearts[i].sprite = fullHeart;
            else if (i < Mathf.Ceil(currentHealth / 2f))
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
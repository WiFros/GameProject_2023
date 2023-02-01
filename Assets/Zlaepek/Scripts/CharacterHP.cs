using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHP : MonoBehaviour
{
    // max ü�� UI
    public static int MAX_HP = 5;
    // �ִ� ü��
    public int totalHP = 10;
    // ���� ü��
    [SerializeField] private int currentHP;

    [SerializeField] private GameObject hp_display;
    [SerializeField] private GameObject[] lifeHeart = new GameObject[MAX_HP];

    // ó�� ü��
    void Start()
    {
        for (int i = 0; i < MAX_HP; i++)
        {
            lifeHeart[i] = hp_display.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject;
        }

        currentHP = totalHP;
        // currentHP = 1;
        HPRender();
    }

    void Update()
    {
        HPRender();
    }

    public void Damaged()
    {
        // ���ó��
        if (currentHP - 1 <= 0)
        {
            // ���ó��
        }
        else
        {
            currentHP--;
        }
        HPRender();
    }

    public void Regened()
    {
        // ���� �� ü�� ����
        HPRender();
    }

    private void HPRender()
    {
        for (int i = 0; i < currentHP / 2; i++)
        {
            lifeHeart[i].GetComponent<Image>().fillAmount = 1.0f;
        }
        if (currentHP % 2 > 0)
        {
            lifeHeart[currentHP / 2].GetComponent<Image>().fillAmount = 0.5f;
        }
        else if (currentHP != 10)
        {
            lifeHeart[currentHP / 2].GetComponent<Image>().fillAmount = 0.0f;
        }
        for (int i = Mathf.CeilToInt(currentHP / 2)+1; i < MAX_HP; i++)
        {
            lifeHeart[i].GetComponent<Image>().fillAmount = 0.0f;
        }
    }
}

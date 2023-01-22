using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHP : MonoBehaviour
{
    // max 체력
    public static int MAX_HP = 2;
    // 최대 체력
    public int totalHP = 3;

    private int currentHP;

    public GameObject[] lifeHeart = new GameObject[MAX_HP];

    private Color HeartColor = new Color(255, 255, 255, 255);
    private Color EmptyHeartColor = new Color(50, 50, 50, 136);

    // 처음 체력
    void Start()
    {
        currentHP = totalHP;
        HPRender();
    }

    public void Damaged()
    {
        // 사망처리
        if (currentHP - 1 <= 0)
        {
            // 사망처리
        }
        else
        {
            currentHP--;
        }
        HPRender();
    }

    public void Regened()
    {
        // 리젠 시 체력 설정
        HPRender();
    }

    private void HPRender()
    {
        for (int i = 1; i < totalHP; i++)
        {
            if (i < currentHP)
            {
                lifeHeart[i-1].GetComponent<Image>().color = HeartColor;
            }
            else
            {
                lifeHeart[i-1].GetComponent<Image>().color = EmptyHeartColor;
            }
        }
    }


}

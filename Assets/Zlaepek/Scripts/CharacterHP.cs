using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHP : MonoBehaviour
{
    // max ü��
    public static int MAX_HP = 2;
    // �ִ� ü��
    public int totalHP = 3;

    private int currentHP;

    public GameObject[] lifeHeart = new GameObject[MAX_HP];

    private Color HeartColor = new Color(255, 255, 255, 255);
    private Color EmptyHeartColor = new Color(50, 50, 50, 136);

    // ó�� ü��
    void Start()
    {
        currentHP = totalHP;
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
